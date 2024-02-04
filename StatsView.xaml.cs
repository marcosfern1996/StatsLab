using Newtonsoft.Json.Linq;
using StatsLab.Connection_OBS;
using StatsLab.Connection_Twitch;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TwitchLib.Api.Helix;
using System.Diagnostics;
using System.Threading;


namespace StatsLab
{

    public partial class StatsView : Window
    {
        private DispatcherTimer timer;
        TwitchConnection twitchConnection;
        Helix helix;
       // OBSWebsocket obs;
        public bool blockTouch = false;

        public StatsView()
        {
            InitializeComponent();
            twitchConnection = new TwitchConnection();
            //obs = new OBSWebsocket();
            helix = new Helix();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1f);
            timer.Tick += rechargedTimer;
            timer.Start();
            
        }


        private void rechargedTimer(object sender, EventArgs e)
        {
            if (DataSaved.Instance.isConnectedOBS)
            {
                ActualizarMuteMicro();
                ActualizarMuteAudio();
                ActualizationLock();
                // Console.WriteLine(DataSaved.Instance.isConnectedOBS);
            }
            else {// Console.WriteLine(DataSaved.Instance.isConnectedOBS);
            }
            ExceptionOBSClose();
        }

        public void ExceptionOBSClose()
        {
            DataSaved.Instance.isOpenedOBS();
            if (!DataSaved.Instance.isOpenObs)
            {
                this.Close();

            }

        }
        public void UpdateProgressBarMicro()
        {
            JObject data = new JObject
    {
        { "inputName", DataSaved.Instance.sourseName},
        { "inputVolumeDb", DataSaved.Instance.soursedB }
        // Agrega otros campos según sea necesario
    };
            string jsonString = data.ToString();

            //OBSConnector.Instance.obs.SendRequest(requestType: "GetInputAudioSyncOffset", data);

        }



        public void ActualizarMuteMicro()
        {
            DataMicro();
            if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.microName))
            {
                UnMuteMicro.Visibility = Visibility.Collapsed;
                MuteMicro.Visibility = Visibility.Visible;
            }
            else
            {
                UnMuteMicro.Visibility = Visibility.Visible;
                MuteMicro.Visibility = Visibility.Collapsed;
            }
        }

        public void ActualizarMuteAudio()
        {
            DataAudio();
            if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.sourseName))
            {
                MuteSourse.Visibility = Visibility.Visible;
                UnMuteSourse.Visibility = Visibility.Collapsed;
            }
            else
            {
                MuteSourse.Visibility = Visibility.Collapsed;
                UnMuteSourse.Visibility = Visibility.Visible;
            }
        }
        public void DataMicro()
        {
               DataSaved.Instance.microOn = OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.microName);
                                  
        }

        public void DataAudio()
        {            
                DataSaved.Instance.sourseOn = OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.sourseName);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (!blockTouch)
            Application.Current.Shutdown();
        }

        private void Block_Click(object sender, RoutedEventArgs e)
        {
            if (blockTouch)
            { blockTouch=false;}
            else
            { blockTouch = true;}
            
        }
       
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && !blockTouch)
            {
                this.DragMove();
            }
        }

        public void ActualizationLock()
        {
            if (blockTouch == false)
            {
                CandadoA.Visibility = Visibility.Visible;
                CandadoC.Visibility = Visibility.Collapsed;
            }
            else if (blockTouch == true)
            {
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoC.Visibility = Visibility.Visible;
            }
        }

        public async void GetStateTWitch()
        {
            Console.WriteLine("mi ID"+ DataSaved.Instance.idTwitch);
            Console.WriteLine("MI Channel"+ DataSaved.Instance.channelName);


            string clientId = DataSaved.Instance.idTwitch; // Reemplaza con tu ID de cliente de Twitch
            string accessToken = "u6yyjddzcj72s7dfyrmuar8n6cvjwq"; // Reemplaza con tu token de acceso

            string apiUrl = $"https://api.twitch.tv/helix/channels?broadcaster_id={DataSaved.Instance.idTwitch}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                client.DefaultRequestHeaders.Add("Client-Id", clientId);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        // Aquí puedes procesar la respuesta JSON para obtener la información que necesitas
                        Console.WriteLine(jsonResponse);
                    }
                    else
                    {
                        Console.WriteLine($"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la solicitud: {ex.Message}");
                }
            }
        }



    }
}





    

