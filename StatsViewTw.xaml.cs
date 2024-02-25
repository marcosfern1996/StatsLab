using Newtonsoft.Json;
using OBSWebsocketDotNet;
using StatsLab.Connection_OBS;
using StatsLab.Connection_Twitch;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TwitchLib.Api;
using TwitchLib.Api.Helix;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.PubSub;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Models;
using TwitchLib.PubSub.Events;
using System.Runtime.InteropServices;
using TwitchLib.Communication.Interfaces;

namespace StatsLab
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {

            Config cfg = new Config(DataSaved.Instance.channelName, "oauth:49pdvza2g1mkc4mnyp18wu81g3f6iv");
            MyTwitchApi api = new MyTwitchApi(cfg);
            api.Connect();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanges(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MyTwitchApi
    {
        public TextBlock textBlock;

        
        Config cfg;
        TwitchClient _client = new TwitchClient();
        JoinedChannel _channel;
        StatsViewTw stats;
        Helix helix;
      //  TwitchPubSub _pubSub= new TwitchPubSub();

        internal MyTwitchApi(Config cfg)
        {
            this.cfg = cfg;

        }

        internal void Connect()
        {
            if (cfg != null)
            {

                ConnectionCredentials credentials = new ConnectionCredentials(cfg.GetName(), cfg.GetToken());
                var clientOptions = new ClientOptions
                {
                    MessagesAllowedInPeriod = 700,
                    ThrottlingPeriod = TimeSpan.FromSeconds(50),
                };
                WebSocketClient customClient = new WebSocketClient(clientOptions);

                _client = new TwitchClient(customClient);
                _client.Initialize(credentials, DataSaved.Instance.channelName);
                _client.Connect();

               


                _client.OnConnected += OnConectedClient;
                _client.OnDisconnected += OndisConectedClient;
                _client.OnMessageReceived += MessageReceived;
                

               // _pubSub = new TwitchPubSub();
              //  _pubSub.OnPubSubServiceConnected += onPubSubServiceConnected;
               // _pubSub.OnListenResponse += onListenResponse;
               // _pubSub.OnStreamUp += StartStream;
                //_pubSub.OnStreamDown += OutStream;
               
                


              //  _pubSub.ListenToFollows(DataSaved.Instance.channelName);

               // _pubSub.Connect();
                // var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

            }
        }

        

        private void onListenResponse(object sender, OnListenResponseArgs e)
        {
            if (!e.Successful)
                throw new Exception($"Failed to listen! Response: {e.Response}");
        }
        private void onPubSubServiceConnected(object sender, EventArgs e)
        {
            // SendTopics accepts an oauth optionally, which is necessary for some topics
            //_pubSub.SendTopics();
        }
        private void OutStream(object sender, OnStreamDownArgs e)
        {
            Console.WriteLine("ya no estoy en directo");
            DataSaved.Instance.isTwitchConnected = true;
        }

        private void StartStream(object sender, OnStreamUpArgs e)
        {
            Console.WriteLine("estoy en directo");
            DataSaved.Instance.isTwitchConnected = true;
        }

        private void OndisConectedClient(object sender, OnDisconnectedEventArgs e)
        {
            DataSaved.Instance.isClienConnected = false;
        }

        public void MessageReceived(object sender, OnMessageReceivedArgs e)
        {
            
          
            DataSaved.Instance.messageTw = e.ChatMessage.Message.ToString();
            DataSaved.Instance.userMesstw = e.ChatMessage.Username.ToString();
            DataSaved.Instance.userColor = e.ChatMessage.ColorHex.ToString();
            
            DataSaved.Instance.newMessage = true;

            Console.WriteLine(e.ChatMessage.ColorHex.ToString() + e.ChatMessage.Username.ToString() + " : " + e.ChatMessage.Message.ToString());

          
        }

        private void OnConectedClient(object sender, OnConnectedArgs e)
        {
            
            _channel = _client.JoinedChannels.FirstOrDefault();
            DataSaved.Instance.isClienConnected = true;
            _client.SendMessage(_channel, "Hola");

        }
    }

    public class Config
    {

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string token;
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        public Config()
        {

        }

        public Config(string username , string userToken)
        {
            //string content = File.ReadAllText(filepath);
           // var c = JsonConvert.DeserializeObject<Config>(content);
            this.name = username;
            this.token = userToken;

        }
        internal string GetName()
        {
            return name;
        }
        internal string GetToken()
        {
            return token;
        }




        public static readonly string TwitchClientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";
        public static readonly string TwitchRedirectUri = "http://localhost:3000/";
        public static readonly string TwitchClientSecret = "i0x8f56wv6h46iwqxo6w2xvwnkiyev";
    }


    public partial class StatsViewTw : Window
    {
        string message;

        private DispatcherTimer timer;

        private DispatcherTimer timerChat;
       
        public bool blockTouch = false;

        public StatsViewTw()
        {

            InitializeComponent();
            //_twitchConnection = new TwitchConnection();
            ShowInTaskbar = false;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1f);
            timer.Tick += rechargedTimer;
            timer.Start();

            timerChat = new DispatcherTimer();
            timerChat.Interval = TimeSpan.FromSeconds(5);
            timerChat.Tick += Timer_Tick;
            timerChat.Start();

            DataSaved.Instance.LoadDocTwitch();
            MyWindowTwitch.Left = DataSaved.Instance.posXTwitch;
            MyWindowTwitch.Top = DataSaved.Instance.posYTwitch;

            if( DataSaved.Instance.heightTwitch > 10 && DataSaved.Instance.widthTwitch> 10){
                MyWindowTwitch.Height = DataSaved.Instance.heightTwitch;
                MyWindowTwitch.Width = DataSaved.Instance.widthTwitch;

            }


        }

        public async void ConsultViewers()
        {  
            if (DataSaved.Instance.isClienConnected)
            {
                var api = new TwitchAPI();
                api.Settings.ClientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";
                api.Settings.Secret = "i0x8f56wv6h46iwqxo6w2xvwnkiyev";
                try
                {
                    var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

                    var streams = await api.Helix.Streams.GetStreamsAsync(userLogins: userLogins);

                    if (streams.Streams.Length > 0)
                    {

                        DataSaved.Instance.countViewers = streams.Streams[0].ViewerCount.ToString();
                        //Console.WriteLine(streams.Streams[0].ViewerCount.ToString());

                    }
                    else
                    {
                        // Console.WriteLine("Canal no encontrado o sin transmisión en vivo");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al consultar el número de espectadores: {ex.Message}");
                }
            }

        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ChatPanel.Children.Count > 100)
            {
                Console.WriteLine("Testchar");
                ChatPanel.Children.RemoveAt(0);
            }
           
        }

        public void MakeNewChat( )
        {

            try
            {
                if (DataSaved.Instance.newMessage)
                {
                    TextBlock textBlock = new TextBlock()
                    {
                        Margin = new Thickness(2),
                        TextWrapping= TextWrapping.Wrap,
                        FontSize = 25,
                        

                    };

                    
                   

                    textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DataSaved.Instance.userColor));
                    textBlock.Text = DataSaved.Instance.userMesstw + " : " + DataSaved.Instance.messageTw;
                  

                    ChatPanel.Children.Add(textBlock);
                    ScrolChat.ScrollToEnd();
                    DataSaved.Instance.newMessage = false;
                }

            }
            catch (Exception e)
            {


            }
           

        }


        private void rechargedTimer(object sender, EventArgs e)
        {
            ConsultViewers();
            UpdateStateLock();
            ShowGrids();



            if (DataSaved.Instance.isClienConnected)
            {

                MakeNewChat();
                UpdateStateViewers();


                OBSWebsocket obs = OBSConnector.Instance.obs;

                //Obtén el estado de la transmisión
                if (DataSaved.Instance.isConnectedOBS)
                {
                    bool isStreaming = obs.GetStreamStatus().IsActive;
                    if (isStreaming)
                    {
                        // OBS está transmitiendo
                        InDirect.Visibility = Visibility.Visible;
                    }
                    else if (obs.GetStreamStatus().IsActive)
                    {
                        //  OBS está reconectándose
                        InDirect.Visibility = Visibility.Visible;
                    }
                    else if (DataSaved.Instance.isConnectedOBS)
                    {
                        //   OBS no está transmitiendo ni reconectándose
                        InDirect.Visibility = Visibility.Collapsed;
                    }


                    // Verifica si OBS está actualmente transmitiendo


                }

            }
        }

        private void ShowGrids()
        {
            if (DataSaved.Instance.Viewerstw)
            {
                GridViewers.Visibility = Visibility.Visible;
            }
            else
            {
                GridViewers.Visibility = Visibility.Collapsed;
            }
            if (DataSaved.Instance.ChatTw)
            {
                GridChatTwitch.Visibility = Visibility.Visible;
            }
            else
            {
                GridChatTwitch.Visibility = Visibility.Collapsed;
            }

        }

        public void UpdateStateViewers() {
            
            ConsultViewers();
            NumViewers.Content = DataSaved.Instance.countViewers;

        }

        private void TuVentana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11  )
            {
                Close.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoA.Visibility= Visibility.Collapsed;
            }
            else if(e.Key == Key.F11 )
            {
               
                Close.Visibility = Visibility.Visible;
                UpdateStateLock();
                
            }

        }

        private void ClosedButton(object sender, RoutedEventArgs e)
        {
            if (!blockTouch)
            {
                double posX = MyWindowTwitch.Left;
                double posY = MyWindowTwitch.Top;
                double winHeigh = MyWindowTwitch.Height;
                double winWidth = MyWindowTwitch.Width;
                DataSaved.Instance.SaveDocTwitch(posX, posY, winHeigh, winWidth);
                Console.WriteLine("SeGuardo");
                this.Hide();
            }
                

        }

        private void BlockButton(object sender, RoutedEventArgs e)
        {
            if (blockTouch)
            {
                blockTouch = false;
               
            }
            else
            { blockTouch = true;

               
            }
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !blockTouch)
            {
                this.DragMove();
            }
        }

        public void UpdateStateLock()
        {
            if(DataSaved.Instance.isBlockTwitch) {
                CandadoC.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
                ButtonBlock.Visibility = Visibility.Collapsed;
            }else if(!DataSaved.Instance.isBlockTwitch && blockTouch == false)
            {
                CandadoA.Visibility = Visibility.Visible;
                CandadoC.Visibility = Visibility.Collapsed;
                ButtonBlock.Visibility = Visibility.Visible;
            }
            else if(!DataSaved.Instance.isBlockTwitch && blockTouch)
            {
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoC.Visibility = Visibility.Visible; 
                ButtonBlock.Visibility = Visibility.Visible;

            }
            if (blockTouch == false)
            {
                
                Close.Visibility = Visibility.Visible;
                MyWindowTwitch.ResizeMode = ResizeMode.CanResizeWithGrip;
                MyWindowTwitch.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19E4E3E3"));
                MyWindowTwitch.BorderBrush = Brushes.Black;
              
            }
            else if (blockTouch == true)
            {
              
                Close.Visibility = Visibility.Collapsed;
                MyWindowTwitch.ResizeMode = ResizeMode.NoResize;
                MyWindowTwitch.BorderBrush = Brushes.Transparent;
                MyWindowTwitch.Background = Brushes.Transparent;
            }
        }
    }
}
