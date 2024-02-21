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
using System.Windows.Threading;
using System.Windows.Media;
using MaterialDesignColors.Recommended;
using OBSWebsocketDotNet.Types;
using System.Linq;

namespace StatsLab
{

    public partial class SettingWindows : Window
    {
        public string[] sourcesNames;
        public int i =0;
        private DispatcherTimer timer;

        TwitchConnection _twitchConnection;
        Bandera bandera; 
        public string portTxt, passwordTxt, micro, souseAudio1;

        public SettingWindows()
        {
            bandera = new Bandera();
            _twitchConnection = new TwitchConnection(); 
            InitializeComponent(); 
            OBSGrid.Visibility = Visibility.Visible;
            ShowOBSImg.Width = 60;
            ShowTwitchImg.Width = 35;
            TwitchGrid.Visibility = Visibility.Collapsed;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1f);
            timer.Tick += rechargedTimer;
            timer.Start();
        }
        private void rechargedTimer(object sender, EventArgs e)
        {
            if (DataSaved.Instance.isTwitchConnected)
            {
                
                GridTwitchConnected.Visibility = Visibility.Visible;
                GridTwitchNotConnected.Visibility = Visibility.Collapsed;
            }
            else
            {
                GridTwitchNotConnected.Visibility= Visibility.Visible;
                GridTwitchConnected.Visibility= Visibility.Collapsed;
            }
            if (DataSaved.Instance.isConnectedOBS)
            {
                getNameImput();
            }
        }
        private void TwitchConnect(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ChanelName.Text))
            {
                MessageBox.Show("Ingrese el nombre de un canal");
                
            }
            else
            {
                DataSaved.Instance.channelName = ChanelName.Text;
                
                _twitchConnection.ConnectTwitch();

            }

        }

        private void ClosedButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizedButton(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private  void ObsConnect(object sender, RoutedEventArgs e)
        {
            

            DataSaved.Instance.PortOBS = PortTXT.Text;

            DataSaved.Instance.PasswordOBS = PasswordTXT.Password;

 
            Conectar.Content = "Conectar OBS";
            DataSaved.Instance.isOpenedOBS();
            OBSConnector.Instance.Connect(DataSaved.Instance.PortOBS, DataSaved.Instance.PasswordOBS);
             

            if (DataSaved.Instance.isOpenObs && DataSaved.Instance.isConnectedOBS)
           {
                Conectar.Content = "Refrescar";
               

            }
           else if (!DataSaved.Instance.isOpenObs && !DataSaved.Instance.isConnectedOBS)
           {
               MessageBox.Show("Abra OBS antes de continuar");
               Conectar.Content = "Conectar OBS";
           }
           
        }

        
        void getNameImput()
        {
            List<InputBasicInfo> sceneItems = OBSConnector.Instance.obs.GetInputList();

            var filteredSources = sceneItems
             .Where(item => item.InputKind == "wasapi_output_capture" || item.InputKind == "wasapi_input_capture")
             .ToArray();

            sourcesNames = filteredSources.Select(item => item.InputName.ToString()).ToArray();
            for ( int i = 0; i < sourcesNames.Length; i++)
            {
                if ( i == 0 && sourcesNames[0] != null)
                {
                    DataSaved.Instance.SourceNum0 = sourcesNames[0];
                }
                if (i == 1 && sourcesNames[1] != null)
                {
                    DataSaved.Instance.SourceNum1 = sourcesNames[1];
                }
                if (i == 2 && sourcesNames[2] != null)
                {
                    DataSaved.Instance.SourceNum2 = sourcesNames[2];
                }
                if (i == 3 && sourcesNames[3]!= null)
                 {
                     DataSaved.Instance.SourceNum3 = sourcesNames[3];
                 }   
                 if (i == 4 && sourcesNames[4] != null)
                 {
                     DataSaved.Instance.SourceNum4 = sourcesNames[4];
                 }  
                 if (   i == 5 && sourcesNames[5] != null)
                 {
                     DataSaved.Instance.SourceNum5 = sourcesNames[5];
                 }  
                 if (i == 6 && sourcesNames[6] != null)
                 {
                     DataSaved.Instance.SourceNum6 = sourcesNames[6];
                 }
                 }
            if (check0.IsChecked == true)
            {
                DataSaved.Instance.sourseAct0 = true;
            }
            else
            {
                DataSaved.Instance.sourseAct0 = false;
            }
            if (check1.IsChecked == true)
            {
                DataSaved.Instance.sourseAct1 = true;
            }
            else { DataSaved.Instance.sourseAct1 = false;
            }
            if (check2.IsChecked == true)
            {
                DataSaved.Instance.sourseAct2 = true;
            }
            else { DataSaved.Instance.sourseAct2 = false;
            }
            if (check3.IsChecked == true)
            {
                DataSaved.Instance.sourseAct3 = true;
            }
            else { DataSaved.Instance.sourseAct3 = false;
            }
            if (check4.IsChecked == true)
            {
                DataSaved.Instance.sourseAct4 = true;
            }
            else { DataSaved.Instance.sourseAct4 = false;
            }
            if (check5.IsChecked == true)
            {
                DataSaved.Instance.sourseAct5 = true;
            }
            else { DataSaved.Instance.sourseAct5 = false;
            }
            if (check6.IsChecked == true)
            {
                DataSaved.Instance.sourseAct6 = true;
            }
            else { DataSaved.Instance.sourseAct6 = false;
            }






                for (int i = 0; i < sourcesNames.Length; i++)
            {

               
                    switch (i)
                    {
                        case 0:
                            source01.Text = sourcesNames[i].ToString();
                            source01.Visibility = Visibility.Visible;
                            check0.Visibility= Visibility.Visible;
                            break;
                        case 1:
                            source02.Text = sourcesNames[i].ToString();
                        source02.Visibility = Visibility.Visible;

                        check1.Visibility = Visibility.Visible;
                        break;
                        case 2:
                            source03.Text = sourcesNames[i].ToString();
                        source03.Visibility = Visibility.Visible;

                        check2.Visibility = Visibility.Visible;
                        break;
                        case 3:
                            source04.Text = sourcesNames[i].ToString();
                        source04.Visibility = Visibility.Visible;

                        check3.Visibility = Visibility.Visible;
                        break;
                        case 4:
                            source05.Text = sourcesNames[i].ToString();
                        source05.Visibility = Visibility.Visible;
                        check4.Visibility = Visibility.Visible;
                        break;
                        case 5:
                            source06.Text = sourcesNames[i].ToString();
                        source06.Visibility = Visibility.Visible;
                        check5.Visibility = Visibility.Visible;
                        break;
                        case 6:
                            source07.Text = sourcesNames[i].ToString();
                            source07.Visibility = Visibility.Visible;
                        check6.Visibility = Visibility.Visible;
                        break;
                        default: break;

                    }

                
               
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
            
        }

        private void ShowOBS_Click(object sender, RoutedEventArgs e)
        {
            OBSGrid.Visibility = Visibility.Visible;
            ShowOBSImg.Width = 60;
            ShowTwitchImg.Width = 35;
            TwitchGrid.Visibility = Visibility.Collapsed;
        }

        private void ShowTwitch_Click(object sender, RoutedEventArgs e)
        {

            OBSGrid.Visibility = Visibility.Collapsed;
            TwitchGrid.Visibility = Visibility.Visible;
            ShowOBSImg.Width = 20;
            ShowTwitchImg.Width = 80;
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
