using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types.Events;
using StatsLab.Connection_OBS;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TwitchLib.Api.Helix;


namespace StatsLab
{

    public partial class StatsView : Window
    {

        OBSWebsocket ws;
        private DispatcherTimer timer;
       
        public bool blockTouch = false;
      
        public StatsView()
        {
            InitializeComponent();
            ws = new OBSWebsocket();
            ShowInTaskbar = false;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.05f);
            timer.Tick += rechargedTimer;
            timer.Start();

            DataSaved.Instance.LoadDocObs();
            MyWindowObs.Left = DataSaved.Instance.posXObs;
            MyWindowObs.Top = DataSaved.Instance.posYObs;
          
           

            if (DataSaved.Instance.heightObs> 10 && DataSaved.Instance.widthObs > 10)
            {
                MyWindowObs.Height = DataSaved.Instance.heightObs;
                MyWindowObs.Width = DataSaved.Instance.widthObs;
            }

            Console.WriteLine("Actual X"+ MyWindowObs.Left);
            Console.WriteLine("Actual Y "+ MyWindowObs.Top);

        }

       

        private void rechargedTimer(object sender, EventArgs e)
        {
           
            if (DataSaved.Instance.isConnectedOBS)
            {
                
                UpdateSources();
                UpdateStateLock();
            }
            else {
            }
            ExceptionOBSClose();



        }
        private void TuVentana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11 )
            {
                Close.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.F11)
            {
              
                Close.Visibility = Visibility.Visible;
                UpdateStateLock();

            }

        }
        public void ExceptionOBSClose()
        {
            DataSaved.Instance.isOpenedOBS();
            if (!DataSaved.Instance._isOpenObs && DataSaved.Instance.isConnectedOBS)
            {
                this.Close();

            }

        }

         
        public void UpdateSources()
        {

            try
            {
                if (DataSaved.Instance.sourseAct0)
                {
                    if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.SourceNum0)
            || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.SourceNum0).VolumeMul <= 0)
                    {
                        MuteSource0.Visibility = Visibility.Visible;
                        UnMuteSource0.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MuteSource0.Visibility = Visibility.Collapsed;
                        UnMuteSource0.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MuteSource0.Visibility = Visibility.Collapsed;
                    UnMuteSource0.Visibility = Visibility.Collapsed;
                }
                if (DataSaved.Instance.sourseAct1)
                {
                    if ( OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.SourceNum1)
                || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.SourceNum1).VolumeMul <= 0)
                {
                    MuteSource1.Visibility = Visibility.Visible;
                    UnMuteSource1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MuteSource1.Visibility = Visibility.Collapsed;
                    UnMuteSource1.Visibility = Visibility.Visible;
                }
                }
                else
                {
                    MuteSource1.Visibility = Visibility.Collapsed;
                    UnMuteSource1.Visibility = Visibility.Collapsed;
                }
                if(DataSaved.Instance.sourseAct2)
                {
                    if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.SourceNum2)
                || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.SourceNum2).VolumeMul <= 0)
                    {
                        MuteSource2.Visibility = Visibility.Visible;
                        UnMuteSource2.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MuteSource2.Visibility = Visibility.Collapsed;
                        UnMuteSource2.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MuteSource2.Visibility = Visibility.Collapsed;
                    UnMuteSource2.Visibility = Visibility.Collapsed;
                }
                if (DataSaved.Instance.sourseAct3)
                {
                    if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.SourceNum3)
                || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.SourceNum3).VolumeMul <= 0)
                {
                    MuteSource3.Visibility = Visibility.Visible;
                    UnMuteSource3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MuteSource3.Visibility = Visibility.Collapsed;
                    UnMuteSource3.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MuteSource3.Visibility = Visibility.Collapsed;
                    UnMuteSource3.Visibility = Visibility.Collapsed;
                }
                if (DataSaved.Instance.sourseAct4)
                {
                    if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.SourceNum4)
                || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.SourceNum4).VolumeMul <= 0)
                    {
                        MuteSource4.Visibility = Visibility.Visible;
                        UnMuteSource4.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MuteSource4.Visibility = Visibility.Collapsed;
                        UnMuteSource4.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MuteSource4.Visibility = Visibility.Collapsed;
                    UnMuteSource4.Visibility = Visibility.Collapsed;
                }
                if (DataSaved.Instance.sourseAct5)
                {
                    if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.SourceNum5)
                     || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.SourceNum5).VolumeMul <= 0)
                    {
                        MuteSource5.Visibility = Visibility.Visible;
                        UnMuteSource5.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MuteSource5.Visibility = Visibility.Collapsed;
                        UnMuteSource5.Visibility = Visibility.Visible;
                    }
                }

                else
                {
                    MuteSource5.Visibility = Visibility.Collapsed;
                    UnMuteSource5.Visibility = Visibility.Collapsed;
                }
                if (DataSaved.Instance.sourseAct6)
                {
                    if (OBSConnector.Instance.obs.GetInputMute(DataSaved.Instance.SourceNum6)
                     || OBSConnector.Instance.obs.GetInputVolume(DataSaved.Instance.SourceNum6).VolumeMul <= 0)
                     {
                         MuteSource6.Visibility = Visibility.Visible;
                         UnMuteSource6.Visibility = Visibility.Collapsed;
                     }
                     else
                     {
                         MuteSource6.Visibility = Visibility.Collapsed;
                         UnMuteSource6.Visibility = Visibility.Visible;
                     }
                }
                else
                {
                    MuteSource6.Visibility = Visibility.Collapsed;
                    UnMuteSource6.Visibility = Visibility.Collapsed;
                }
            





                }catch (Exception ex) {
                Console.WriteLine("fallo");

            }



        }

       

        private void ClosedButton(object sender, RoutedEventArgs e)
        {
            if (!DataSaved.Instance.blockTouchobs)
            {
                DataSaved.Instance.posXObs = MyWindowObs.Left;
                DataSaved.Instance.posYObs= MyWindowObs.Top;
                DataSaved.Instance.heightObs= MyWindowObs.Height;
                DataSaved.Instance.widthObs= MyWindowObs.Width;
                DataSaved.Instance.SaveDocObs(DataSaved.Instance.posXObs, DataSaved.Instance.posYObs, DataSaved.Instance.heightObs, DataSaved.Instance.widthObs);
                Console.WriteLine("SeGuardo");
                this.Hide();
            }
                
        }

        private void BlockButton(object sender, RoutedEventArgs e)
        {
            if (DataSaved.Instance.blockTouchobs)
            { DataSaved.Instance.blockTouchobs = false; }
            else
            { DataSaved.Instance.blockTouchobs = true; }

        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !DataSaved.Instance.blockTouchobs)
            {
                this.DragMove();
            }
        }

        public void UpdateStateLock()
        {
            if (DataSaved.Instance.isBlockObs)
            {
                CandadoC.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed; 
                ButtonBlock.Visibility = Visibility.Collapsed;
            }
            else if (!DataSaved.Instance.isBlockObs && DataSaved.Instance.blockTouchobs == false)
            {
                CandadoA.Visibility = Visibility.Visible;
                CandadoC.Visibility = Visibility.Collapsed;
                ButtonBlock.Visibility = Visibility.Visible;
            }
            else if (!DataSaved.Instance.isBlockObs && DataSaved.Instance.blockTouchobs)
            {
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoC.Visibility = Visibility.Visible;
                ButtonBlock.Visibility = Visibility.Visible;
            }
            if (DataSaved.Instance.blockTouchobs == false)
            {
                
                MyWindowObs.ResizeMode = ResizeMode.CanResizeWithGrip;
                Close.Visibility = Visibility.Visible;
                MyWindowObs.BorderBrush = Brushes.Black;
                MyWindowObs.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19E4E3E3"));
            }
            else if (DataSaved.Instance.blockTouchobs == true)
            {
               
                Close.Visibility = Visibility.Collapsed;

                MyWindowObs.BorderBrush = Brushes.Transparent; 
                MyWindowObs.Background = Brushes.Transparent;
                MyWindowObs.ResizeMode = ResizeMode.NoResize;
            }
        }



   
    }
    } 





    

