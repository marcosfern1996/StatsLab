using StatsLab.Connection_OBS;
using StatsLab.Connection_Twitch;
using System;
using System.Windows;
using System.Windows.Input;

namespace StatsLab
{

    public partial class SettingWindows : Window
    {
        TwitchConnection _twitchConnection;
        Bandera bandera;
        

       // public bool monitoreando;
        public string portTxt, passwordTxt, micro, souseAudio1;
        
        //public bool sussesConectedBool;
        
        public SettingWindows()
        {
           
           bandera = new Bandera();
            _twitchConnection = new TwitchConnection();
            
            InitializeComponent();
        }

        
        private void Datos_Click(object sender, RoutedEventArgs e)
        {

            bandera.ActualizarBarraMicro();
            /*
            this._twitchConnection.ConnectTwitchAccount();
            DataSaved.Instance.idTwitch = IdTwitch.Text;
            DataSaved.Instance.channelName= ChanelName.Text;
            bandera.ConectingTwitch();
            */
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataSaved.Instance.PortOBS = PortTXT.Text;
            DataSaved.Instance.PasswordOBS = PasswordTXT.Password;
            DataSaved.Instance.sourseName = NameSourse.Text;
            DataSaved.Instance.microName = NameMicrophone.Text;


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

        private void Monitoreo_Click(object sender, RoutedEventArgs e)
        {
            DataSaved.Instance.isOpenedOBS();
            if (DataSaved.Instance.isOpenObs && DataSaved.Instance.isConnectedOBS)
            {
                bandera.ActualizarMicro();
                bandera.ActualizarSourse();
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
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed )
            {
                this.DragMove();
            }
        }
        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            // Abre el enlace en el navegador predeterminado
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;  // Para evitar que el navegador predeterminado se abra también
        }
    }
}
