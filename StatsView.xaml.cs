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
using OBSWebsocketDotNet.Types.Events;
using OBSWebsocketDotNet;


namespace StatsLab
{

    public partial class StatsView : Window
    {
        OBSWebsocket ws;
        private DispatcherTimer timer;
        Helix helix;
        public bool blockTouch = false;

        public StatsView()
        {
            InitializeComponent();
            helix = new Helix();
            ws =new OBSWebsocket();
            //KeyDown += TuVentana_KeyDown;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1f);
            timer.Tick += rechargedTimer;
            timer.Start();
        }

        private void rechargedTimer(object sender, EventArgs e)
        {
            ViewStates();
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
        private void TuVentana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11 && Block.Visibility == Visibility.Visible)
            {
                Block.Visibility = Visibility.Collapsed;
                Close.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.F11 && Block.Visibility == Visibility.Collapsed)
            {
                Block.Visibility = Visibility.Visible;
                Close.Visibility = Visibility.Visible;
                UpdateStateLock();

            }

        }
        public void ExceptionOBSClose()
        {
            DataSaved.Instance.isOpenedOBS();
            if (!DataSaved.Instance.isOpenObs && DataSaved.Instance.isConnectedOBS)
            {
                this.Hide();

            }

        }

        public void UpdateProgressBarMicro()
        {


        }

        public void UpdateStateMicro()
        {
            
            if (DataSaved.Instance.microName != null )
            {
                DataMicro();
                if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.microName) || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.microName).VolumeMul <= 0)
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
        }

        public void UpdateStateSource()
        {
            if (DataSaved.Instance.sourceName != null)
            {
                DataAudio();
                if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.sourceName) || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.sourceName).VolumeMul <= 0)
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
        }

        public void DataMicro()
        {
            if (DataSaved.Instance.microName != null)
            {
                DataSaved.Instance.microOn = OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.microName);
            }

        }

        public void DataAudio()
        {
            DataSaved.Instance.sourceOn = OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.sourceName);
        }

        private void ClosedButton(object sender, RoutedEventArgs e)
        {
            if (!blockTouch)
                this.Hide();
        }

        private void BlockButton(object sender, RoutedEventArgs e)
        {
            if (blockTouch)
            { blockTouch = false; }
            else
            { blockTouch = true; }

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

       
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Console.WriteLine(OBSConnector.Instance.obs.GetMediaInputStatus("Musica"));
        }
        private void Obs_InputVolumeMeters(object sender, InputVolumeMetersEventArgs e)
        {
            /*JObject sourses = new JObject()
            {
                { "PropertyName", " "}, 
            }*/

            // Aquí puedes trabajar con los datos de volumen recibidos en e.inputs
           /* foreach (var input in e.inputs)
            {
                var nombreInput = input["Musica"].;
                Console.WriteLine(nombreInput);
                
                // Accede a las propiedades de cada input, por ejemplo:
                // var nombreInput = input["nombrePropiedad"].ToString();
            }
           */
            Console.WriteLine("Funcionó");
        }

        public void ViewStates()
        {
            if (DataSaved.Instance.microName == null)
            {
                Micro.Visibility = Visibility.Collapsed;
                MuteMicro.Visibility = Visibility.Collapsed;
                UnMuteMicro.Visibility = Visibility.Collapsed;
            }
            else
            {
                Micro.Visibility = Visibility.Visible;
                MuteMicro.Visibility = Visibility.Visible;
                UnMuteMicro.Visibility = Visibility.Visible;
            }
            if (DataSaved.Instance.sourceName == null)
            {
                Source.Visibility = Visibility.Collapsed;
                MuteSource.Visibility = Visibility.Collapsed;
                UnMuteSource.Visibility = Visibility.Collapsed;
            }
            else
            {
                Source.Visibility = Visibility.Visible;
                MuteSource.Visibility = Visibility.Visible;
                UnMuteSource.Visibility = Visibility.Visible;
            }
        }
    }
    } 





    

