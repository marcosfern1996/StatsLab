using OBSWebsocketDotNet.Types;
using StatsLab.Connection_OBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using TwitchLib.Api;
using TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage;
using WebSocket4Net.Command;

namespace StatsLab
{
    public enum ChatTypes
    {
        KapChat = 0,
        TwitchPopout = 1,
        CustomURL = 2,
        jChat = 3
    }

    public partial class SettingWindows : Window
    {
        TwitchAPI api = new TwitchAPI();
        List<string> scopes = new List<string>{ "chat:read", "user:read:chat",
                    "channel:read:subscriptions","channel:read:stream_key","channel:read:editors",
                    "user:read:follows", "channel:manage:polls"};
        string host = "https://marcosfern1996.github.io/";

        private bool isButtonPressed = false;

        private const int WM_USER = 0x0400;
        private const int WM_MAXIMIZE_WINDOW = WM_USER + 1;

        public string[] sourcesNames;
        public int i =0;
        private DispatcherTimer timer;
        private readonly NotifyIcon notifyIcon;
        MainViewModel mainViewModel;

        System.Drawing.Icon icon = new System.Drawing.Icon("Images/HUB-Blanco.ico");
        
        
        
       
        Flag flag; 
        public string portTxt, passwordTxt, micro, souseAudio1;
        public int a = 1;


        public SettingWindows()
        {
            api.Settings.ClientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";

            api.Settings.Secret = "z74luhth5mt9q2186fgebmc5rcqtsi";

            api.Settings.AccessToken = api.Settings.Secret;


            notifyIcon = new NotifyIcon();
            flag = new Flag();
          
            InitializeComponent(); 
            OBSGrid.Visibility = Visibility.Visible;
            ShowOBSImg.Width = 60;
            ShowTwitchImg.Width = 35;
            TwitchGrid.Visibility = Visibility.Collapsed;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1f);
            timer.Tick += rechargedTimer;
            timer.Start();



            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = icon;
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += (sender, e) => ShowMainWindow();


            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Abrir Ventana", null, ShouMainWindow);
            notifyIcon.ContextMenuStrip.Items.Add("Activar Candados", null, ActivateLock );
            notifyIcon.ContextMenuStrip.Items.Add("Desactivar Candados", null, DescativateLock );
            notifyIcon.ContextMenuStrip.Items.Add("Cerrar Aplicacion", null, ClosedButton );
            App.Current.Exit += OnAppExit;

          
           
            DataSaved.Instance.LoadUserData();

           
            ChanelName.Text= DataSaved.Instance.channelName;
            sizeLetter.Text = DataSaved.Instance.letterSize.ToString();
            PortTXT.Text = DataSaved.Instance.PortOBS;
            if (DataSaved.Instance.letterSize == 0)
            {
                DataSaved.Instance.letterSize = 20;
                 sizeLetter.Text = DataSaved.Instance.letterSize.ToString();
            }

        }


        private void OnAppExit(object sender, ExitEventArgs e)
        {
           
            notifyIcon.Dispose();
        }

        private void ActivateLock(object sender, EventArgs e)
        {
            DataSaved.Instance.isBlockObs = false;
            DataSaved.Instance.isBlockTwitch= false;
        }
        private void DescativateLock(object sender, EventArgs e)
        {
            DataSaved.Instance.isBlockObs = true;
            DataSaved.Instance.isBlockTwitch= true;
        }

        private void rechargedTimer(object sender, EventArgs e)
        {
            if (CheckChat.IsChecked == true)
            {
                DataSaved.Instance.ChatTw = true;
            }
            else
            {
                DataSaved.Instance.ChatTw = false;
            }
            if (CheckViewers.IsChecked == true)
            {
                DataSaved.Instance.Viewerstw = true;
            }
            else
            {
                DataSaved.Instance.Viewerstw = false;
            }
            if (DataSaved.Instance.isClienConnected)
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
                System.Windows.MessageBox.Show("Ingrese el nombre de un canal");
                
            }
            else
            {
                DataSaved.Instance.channelName = ChanelName.Text;
               mainViewModel = new MainViewModel();
                this.DataContext = mainViewModel;
          
            }

        }
        private void ChangeFonrsize(object sender , System.Windows.Input.KeyEventArgs e)

        {
            if (e.Key == Key.Enter)
            {
                DataSaved.Instance.letterSize = double.Parse(sizeLetter.Text);
            }
            
        }
        
        private void ChangeFonrsize(object sender , KeyboardFocusChangedEventArgs e)
        {
           
                DataSaved.Instance.letterSize = double.Parse(sizeLetter.Text);
           
            
        }

        private void ClosedButton(object sender, RoutedEventArgs e)
        {
            notifyIcon.Dispose();
            DataSaved.Instance.SaveUserData(DataSaved.Instance.channelName,DataSaved.Instance.PortOBS ,DataSaved.Instance.letterSize);
            System.Windows.Application.Current.Shutdown();
        }
        private void ClosedButton(object sender, EventArgs e)
        {
            notifyIcon.Dispose(); 
            DataSaved.Instance.SaveUserData(DataSaved.Instance.channelName, DataSaved.Instance.PortOBS, DataSaved.Instance.letterSize);

            System.Windows.Application.Current.Shutdown();
            
        }

        private void MinimizedHiddenButton(object sender, RoutedEventArgs e)
        {
            DataSaved.Instance.isBlockTwitch = true;
            DataSaved.Instance.isBlockObs = true;
            DataSaved.Instance.blockTouchtwitch = true;
            DataSaved.Instance.blockTouchobs=true;
            this.WindowState = WindowState.Minimized;

            if (this.WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }
        private void MinimizedButton(object sender, RoutedEventArgs e)
        {
           // DataSaved.Instance.isBlockTwitch = true;
            //DataSaved.Instance.isBlockObs= true;
            this.WindowState = WindowState.Minimized;

           
        }

        private void ShouMainWindow(object sender, EventArgs args)
        {
           
            ShowMainWindow();
        }
        private void ShowMainWindow()
        {
            this.Topmost = true;
            DataSaved.Instance.isBlockTwitch= false;
            DataSaved.Instance.isBlockObs= false; 
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Topmost = false;
        }

        private  void ObsConnect(object sender, RoutedEventArgs e)
        {
            

            DataSaved.Instance.PortOBS = PortTXT.Text;

            DataSaved.Instance.PasswordOBS = PasswordTXT.Password;

 
            Conectar.Content = "Conectar OBS";
            DataSaved.Instance.isOpenedOBS();
            OBSConnector.Instance.Connect(DataSaved.Instance.PortOBS, DataSaved.Instance.PasswordOBS);
            // WantImput();
            isButtonPressed = true;

            if (DataSaved.Instance._isOpenObs && DataSaved.Instance.isConnectedOBS)
           {
                Conectar.Content = "Refrescar";
               

            }
           else if (!DataSaved.Instance._isOpenObs && !DataSaved.Instance.isConnectedOBS)
           {
                System.Windows.MessageBox.Show("Abra OBS antes de continuar");
               Conectar.Content = "Conectar OBS";
           }
           
        }
        // i need to think how make to want any sourses and show it here, but i need to acces to this input.
        /*
        void  WantImput()
        {
            if (isButtonPressed)
            {

                List<InputBasicInfo> sceneItems = OBSConnector.Instance.obs.GetInputList();

                var filteredSources = sceneItems
                 .Where(item => item.InputName == "" || item.InputKind == "wasapi_output_capture" || item.InputKind == "wasapi_input_capture" || item.InputKind == "ffmpeg_source")
                 .OrderBy(item => item.InputName)
                 .ToArray();

           

                sourcesNames = filteredSources.Select(item => item.InputName.ToString()).ToArray();

                for (int i = 0; i < sourcesNames.Length; i++)
                {
                    WrapPanel wrapPanel = new WrapPanel()
                    {
                        Margin = new Thickness(2),
                        Orientation = System.Windows.Controls.Orientation.Vertical,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    WrapSourses.Children.Add(wrapPanel);
                  
                    TextBlock sourse = new TextBlock()
                    {
                        Text = sourcesNames[i].ToString(),
                        
                    };
                    
                    System.Windows.Controls.CheckBox checkSourse = new System.Windows.Controls.CheckBox()
                    {

                    };

                    DataSaved.Instance.sourcesInput[i] = i;
                    DataSaved.Instance.sourcesCheck[i] = i;

                    wrapPanel.Children.Add(checkSourse);
                    wrapPanel.Children.Add(sourse);
                };

                for (int i = 0; i < sourcesNames.Length; i++)
                {
                    if (i == 0 && sourcesNames[i] != null)
                    {
                        DataSaved.Instance.sourcesInput[i] = i;
                    }
                }
                    isButtonPressed = false;
            }


        }
        */

        public void Aut(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo($"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id={api.Settings.ClientId}&redirect_uri={host}&scope={String.Join("+", scopes)}"));
           var authUrl = $"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id={api.Settings.ClientId}&redirect_uri={host}&scope={String.Join("+", scopes)}";
            Console.WriteLine(authUrl);

        }
        void getNameImput()
        {
            try
            {
                List<InputBasicInfo> sceneItems = OBSConnector.Instance.obs.GetInputList();



                var filteredSources = sceneItems
                 .Where(item => item.InputName == "" || item.InputKind == "wasapi_output_capture" || item.InputKind == "wasapi_input_capture" || item.InputKind == "ffmpeg_source")
                 .OrderBy(item => item.InputName)

                 .ToArray();

                if (a < 2)
                {
                    Console.WriteLine("lista de inputs " + filteredSources);
                    a++;
                }

                sourcesNames = filteredSources.Select(item => item.InputName.ToString()).ToArray();

      

                

                for (int i = 0; i < sourcesNames.Length; i++)
                {
                    if (i == 0 && sourcesNames[0] != null)
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
                    if (i == 3 && sourcesNames[3] != null)
                    {
                        DataSaved.Instance.SourceNum3 = sourcesNames[3];
                    }
                    if (i == 4 && sourcesNames[4] != null)
                    {
                        DataSaved.Instance.SourceNum4 = sourcesNames[4];
                    }
                    if (i == 5 && sourcesNames[5] != null)
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
                else
                {
                    DataSaved.Instance.sourseAct1 = false;
                }
                if (check2.IsChecked == true)
                {
                    DataSaved.Instance.sourseAct2 = true;
                }
                else
                {
                    DataSaved.Instance.sourseAct2 = false;
                }
                if (check3.IsChecked == true)
                {
                    DataSaved.Instance.sourseAct3 = true;
                }
                else
                {
                    DataSaved.Instance.sourseAct3 = false;
                }
                if (check4.IsChecked == true)
                {
                    DataSaved.Instance.sourseAct4 = true;
                }
                else
                {
                    DataSaved.Instance.sourseAct4 = false;
                }
                if (check5.IsChecked == true)
                {
                    DataSaved.Instance.sourseAct5 = true;
                }
                else
                {
                    DataSaved.Instance.sourseAct5 = false;
                }
                if (check6.IsChecked == true)
                {
                    DataSaved.Instance.sourseAct6 = true;
                }
                else
                {
                    DataSaved.Instance.sourseAct6 = false;
                }






                for (int i = 0; i < sourcesNames.Length; i++)
                {


                    switch (i)
                    {
                        case 0:
                            source01.Text = sourcesNames[i].ToString();
                            source01.Visibility = Visibility.Visible;
                            check0.Visibility = Visibility.Visible;
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




            }catch(Exception ex) { 
                DataSaved.Instance.isConnectedOBS=false;
            }


        }
        
        private void MonitoringTwitch(object sender, RoutedEventArgs e)
        {
            flag.AbrirMonitoreoTwitch();
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
            if (DataSaved.Instance._isOpenObs && DataSaved.Instance.isConnectedOBS)
            {
               
                flag.AbrirMonitoreoObs();

               
            }
            if(!DataSaved.Instance._isOpenObs)
            {
                System.Windows.MessageBox.Show("Abra OBS antes de continuar");
            }
            if (!DataSaved.Instance.isConnectedOBS && DataSaved.Instance._isOpenObs)
            {
                System.Windows.MessageBox.Show("Faltan datos de obs");
            }


        }

        private void Datos_Copiar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed )
            {
                this.DragMove();
            }
        }

        private void Webs(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true; 
        }

       private void cafe(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://cafecito.app/MarcosFedez"));
            e.Handled = true; 
        }
       
       
    }
}
