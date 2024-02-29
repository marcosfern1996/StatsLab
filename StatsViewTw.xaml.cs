using OBSWebsocketDotNet;
using StatsLab.Connection_OBS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Models;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;
using TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage;

namespace StatsLab
{ 

    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {

            Config cfg = new Config(DataSaved.Instance.channelName, "oauth:d0qz61r8j8fiztw6f06ujg1stvma3u");
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
       
        internal MyTwitchApi(Config cfg)
        {
            this.cfg = cfg;

        }

        internal async void Connect()
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

                _channel = new JoinedChannel(DataSaved.Instance.channelName);

                _client.OnConnected += OnConectedClient;
                _client.OnDisconnected += OndisConectedClient;
                _client.OnMessageReceived += MessageReceived;
               
             
                // var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

            }
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


   
        }

        private void OnConectedClient(object sender, OnConnectedArgs e)
        {
           
          //  _channel = _client.JoinedChannels.FirstOrDefault();
            DataSaved.Instance.isClienConnected = true;
          //  _client.SendMessage(_channel, "Hola");

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
        private void rechargedTimer(object sender, EventArgs e)
        {
           
            UpdateStateLock();
            ShowGrids();
            ConsultViewers();
            if (!DataSaved.Instance.Viewerstw)
            {
                GridChatTwitch.SetValue(Grid.RowProperty, 1);
                GridChatTwitch.SetValue(Grid.RowSpanProperty, 5);
            }
            else if(DataSaved.Instance.Viewerstw)
            {
                GridChatTwitch.SetValue(Grid.RowProperty, 2);
                GridChatTwitch.SetValue(Grid.RowSpanProperty, 4);
            }


            if (DataSaved.Instance.isClienConnected)
            {
               
                MakeNewChat();
                UpdateStateViewers();
               

                OBSWebsocket obs = OBSConnector.Instance.obs;

                if (DataSaved.Instance.isConnectedOBS)
                {
                    DataSaved.Instance.isStreaming = obs.GetStreamStatus().IsActive;
                    if (obs.GetStreamStatus().IsActive)
                    {
                        InDirect.Visibility = Visibility.Visible;
                    }
                    else if (!obs.GetStreamStatus().IsActive)
                    {
                        InDirect.Visibility = Visibility.Collapsed;
                    }

                }

            }
        }
       
        public async void ConsultViewers()
        {  
            if ( DataSaved.Instance.isStreaming)
            {
                

                var api = new TwitchAPI();
                api.Settings.ClientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";
                api.Settings.Secret = "z74luhth5mt9q2186fgebmc5rcqtsi";


                try

                {
                   

                    var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

                    var streams = await api.Helix.Streams.GetStreamsAsync(userLogins: userLogins);

                    api.Settings.AccessToken = api.Settings.Secret;

                    try
                    {
                        var emotes = await api.Helix.Chat.GetGlobalEmotesAsync();

                        foreach (var emote in emotes.GlobalEmotes)
                        {
                            Console.WriteLine($"Emote ID: {emote.Id}, Code: {emote.Name}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine (ex.ToString());
                    }
                    
                    

                    if (streams.Streams.Length > 0)
                    {
                        try
                        {
                            DataSaved.Instance.countViewers = streams.Streams[0].ViewerCount.ToString();
                        }
                        catch
                        {

                        }
                      
                    }
                    else
                    {
                      //  Console.WriteLine("Canal no encontrado o sin transmisión en vivo");
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
            


          
                if (DataSaved.Instance.newMessage)
                {
                    
                    
                    WrapPanel stackPanel = new WrapPanel()
                    {
                        Margin = new Thickness(2),
                        Orientation = Orientation.Horizontal,
                        VerticalAlignment = VerticalAlignment.Center,
                        
                    };
                    ChatPanel.Children.Add( stackPanel );

                    TextBlock userChat = new TextBlock()
                    {
                        Margin = new Thickness(2),
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = DataSaved.Instance.letterSize


                    };
                    TextBlock messageChat = new TextBlock()
                    {
                        Margin = new Thickness(2),
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = DataSaved.Instance.letterSize

                    };
                    userChat.Text = DataSaved.Instance.userMesstw + " : " ;
                     messageChat.Text = DataSaved.Instance.messageTw ;

                    
                        messageChat.Foreground = Brushes.White;
                        try
                        {
                            userChat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DataSaved.Instance.userColor));
                        }
                        catch (Exception exception )
                        {
                            
                            userChat.Foreground = Brushes.Red; 
                        }

                stackPanel.Children.Add(userChat);
                   stackPanel.Children.Add(messageChat);
                    ScrolChat.ScrollToEnd();
                    DataSaved.Instance.newMessage = false;
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
       
            NumViewers.Content = DataSaved.Instance.countViewers;

        }

       

        private void ClosedButton(object sender, RoutedEventArgs e)
        {
            if (!DataSaved.Instance.blockTouchtwitch)
            {
                DataSaved.Instance.posXObs = MyWindowTwitch.Left;
                DataSaved.Instance.posYObs = MyWindowTwitch.Top;
                DataSaved.Instance.heightTwitch = MyWindowTwitch.Height;
                DataSaved.Instance.widthTwitch= MyWindowTwitch.Width;
                DataSaved.Instance.SaveDocTwitch(DataSaved.Instance.posXObs, DataSaved.Instance.posYObs, DataSaved.Instance.heightTwitch, DataSaved.Instance.widthTwitch);
                Console.WriteLine("SeGuardo");
                this.Hide();
            }
                

        }

      

        private void BlockButton(object sender, RoutedEventArgs e)
        {
            if (DataSaved.Instance.blockTouchtwitch)
            {
                DataSaved.Instance.blockTouchtwitch = false;
               
            }
            else
            {
                DataSaved.Instance.blockTouchtwitch = true;

               
            }
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !DataSaved.Instance.blockTouchtwitch)
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
            }else if(!DataSaved.Instance.isBlockTwitch && DataSaved.Instance.blockTouchtwitch == false)
            {
                CandadoA.Visibility = Visibility.Visible;
                CandadoC.Visibility = Visibility.Collapsed;
                ButtonBlock.Visibility = Visibility.Visible;
            }
            else if(!DataSaved.Instance.isBlockTwitch && DataSaved.Instance.blockTouchtwitch)
            {
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoC.Visibility = Visibility.Visible; 
                ButtonBlock.Visibility = Visibility.Visible;

            }
            if (DataSaved.Instance.blockTouchtwitch == false)
            {
                
                Close.Visibility = Visibility.Visible;
                MyWindowTwitch.ResizeMode = ResizeMode.CanResizeWithGrip;
                MyWindowTwitch.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19E4E3E3"));
                MyWindowTwitch.BorderBrush = Brushes.Black;
              
            }
            else if (DataSaved.Instance.blockTouchtwitch == true)
            {
              
                Close.Visibility = Visibility.Collapsed;
                MyWindowTwitch.ResizeMode = ResizeMode.NoResize;
                MyWindowTwitch.BorderBrush = Brushes.Transparent;
                MyWindowTwitch.Background = Brushes.Transparent;
            }
        }
    }
}
