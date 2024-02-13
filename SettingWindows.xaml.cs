using StatsLab.Connection_OBS;
using StatsLab.Connection_Twitch;
using System;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using TwitchLib.Api; 
using TwitchLib.Api.Helix.Models.Streams;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Interfaces;

namespace StatsLab
{

    public partial class SettingWindows : Window
    {

        TwitchConnection _twitchConnection;
        Bandera bandera;        
        public string portTxt, passwordTxt, micro, souseAudio1;

        public SettingWindows()
        {
           
            bandera = new Bandera();
            _twitchConnection = new TwitchConnection();
            InitializeComponent();
        }
        
        private void TwitchConnect(object sender, RoutedEventArgs e)
        {
            DataSaved.Instance.channelName = ChanelName.Text;

            _twitchConnection.ConnectTwitch();
            DataSaved.Instance.isTwitchConnected = true;
            /*
            this._twitchConnection.ConnectTwitchAccount();
            DataSaved.Instance.idTwitch = IdTwitch.Text;
            DataSaved.Instance.channelName= ChanelName.Text;
            bandera.ConectingTwitch();
            */
        }

        private void ClosedButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizedButton(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ObsConnect(object sender, RoutedEventArgs e)
        {
            
            DataSaved.Instance.PortOBS = PortTXT.Text;
            DataSaved.Instance.PasswordOBS = PasswordTXT.Password;

            if (string.IsNullOrWhiteSpace(NameSource.Text))
            {
                DataSaved.Instance.sourceName = null;
            }
            else
            {
                DataSaved.Instance.sourceName = NameSource.Text;
            }
            
            if(string.IsNullOrWhiteSpace(NameMicrophone.Text))
            {
                DataSaved.Instance.microName = null;
            }
            else
            {
                DataSaved.Instance.microName = NameMicrophone.Text;
            }
            
            Conectar.Content = "Conectar OBS";
            DataSaved.Instance.isOpenedOBS();
            OBSConnector.Instance.Connect(DataSaved.Instance.PortOBS, DataSaved.Instance.PasswordOBS);

            if (DataSaved.Instance.isOpenObs )
           {
                Conectar.Content = "Refrescar"; 
                
            }
           else if (!DataSaved.Instance.isOpenObs)
           {
               MessageBox.Show("Abra OBS antes de continuar");
               Conectar.Content = "Conectar OBS";
           }
                       
        }

        private void MonitoringTwitch(object sender, RoutedEventArgs e)
        {
            bandera.AbrirMonitoreoTwitch();
        }

        private void listBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //in this function  i need to detect the source list of OBS and show it here.
        }

        private void NameSource_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            /*List<string> listaDeElementos = new List<string>
            {
                "Elemento 1",
                "Elemento 2",
                "Elemento 3"
            };
            foreach (string elemento in listaDeElementos)
            {
                NameSource.Text += elemento + Environment.NewLine;
            }*/
        }

        private void Monitoring(object sender, RoutedEventArgs e)
        {
            DataSaved.Instance.isOpenedOBS();
            if (DataSaved.Instance.isOpenObs && DataSaved.Instance.isConnectedOBS)
            {
                bandera.ActualizarMicro();
                bandera.ActualizarSource();
                bandera.AbrirMonitoreoObs();

               
            }
            if(!DataSaved.Instance.isOpenObs)
            {
                MessageBox.Show("Abra OBS antes de continuar");
            }
            if (!DataSaved.Instance.isConnectedOBS && DataSaved.Instance.isOpenObs)
            {
                MessageBox.Show("Faltan datos de obs");
            }


        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed )
            {
                this.DragMove();
            }
        }

        private void Donations(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true; 
        }
    }
}
