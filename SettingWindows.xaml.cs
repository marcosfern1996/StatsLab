using StatsLab.Connection_OBS;
using StatsLab.Connection_Twitch;
using System;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;

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

            bandera.ActualizarBarraMicro();
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
            DataSaved.Instance.sourceName = NameSource.Text;
            DataSaved.Instance.microName[1] = NameMicrophone.Text;


            Conectar.Content = "Conectar OBS"; 
            OBSConnector.Instance.Connect(DataSaved.Instance.PortOBS, DataSaved.Instance.PasswordOBS);
            DataSaved.Instance.isOpenedOBS();
           if (DataSaved.Instance.isOpenObs)
           {
                Conectar.Content = "Refrescar";
            }
           else if (!DataSaved.Instance.isOpenObs)
           {
               MessageBox.Show("Abra OBS antes de continuar");
               Conectar.Content = "Conectar OBS";
           }
                       
        }

        private void Monitoring(object sender, RoutedEventArgs e)
        {
            DataSaved.Instance.isOpenedOBS();
            if (DataSaved.Instance.isOpenObs && DataSaved.Instance.isConnectedOBS)
            {
                bandera.ActualizarMicro();
                bandera.ActualizarSource();
                bandera.AbrirMonitoreo();

                //monitoreando = true;
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
