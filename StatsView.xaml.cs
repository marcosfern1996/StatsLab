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
        public bool blockTouch = false;

        public StatsView()
        {
            InitializeComponent();
            twitchConnection = new TwitchConnection();
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
                UpdateStateMicro();
                UpdateStateSource();
                UpdateStateLock();
            }
            else {
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
            //I need Continuae this part.
            //that class need get the dB of the Sourse and show it in a progressBar.
            JObject data = new JObject
            {
                { "inputName", DataSaved.Instance.sourceName},
                { "inputVolumeDb", DataSaved.Instance.sourcedB }

            };
            string jsonString = data.ToString();
            
            
        }

        public void UpdateStateMicro()
        {
            DataMicro();
            if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.microName[1]))
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

        public void UpdateStateSource()
        {
            DataAudio();
            if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.sourceName))
            {
                MuteSource.Visibility = Visibility.Visible;
                UnMuteSource.Visibility = Visibility.Collapsed;
            }
            else
            {
                MuteSource.Visibility = Visibility.Collapsed;
                UnMuteSource.Visibility = Visibility.Visible;
            }
        }

        public void DataMicro()
        {
            // I want to have access to all audio sourse an make a list, which the user will can choose her source audio
            for(int i = 0;DataSaved.Instance.microName.Length > 0;i++)
            {
                DataSaved.Instance.microOn = OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.microName[1]);

            }

        }

        public void DataAudio()
        {            
                DataSaved.Instance.sourceOn = OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.sourceName);
        }

        private void ClosedButton(object sender, RoutedEventArgs e)
        {
            if (!blockTouch)
            Application.Current.Shutdown();
        }

        private void BlockButton(object sender, RoutedEventArgs e)
        {
            if (blockTouch)
            { blockTouch=false;}
            else
            { blockTouch = true;}
            
        }
       
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && !blockTouch)
            {
                this.DragMove();
            }
        }

        public void UpdateStateLock()
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





    

