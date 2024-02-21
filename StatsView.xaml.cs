using OBSWebsocketDotNet;
using StatsLab.Connection_OBS;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TwitchLib.Api.Helix;


namespace StatsLab
{

    public partial class StatsView : Window
    {

        OBSWebsocket ws;
        private DispatcherTimer timer;
        Helix helix;
        public bool blockTouch = false;
        string[] nombres;
        public StatsView()
        {
            InitializeComponent();
            helix = new Helix();
            ws = new OBSWebsocket();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.05f);
            timer.Tick += rechargedTimer;
            timer.Start();

            DataSaved.Instance.LoadDocObs();
            MyVentanaObs.Left = DataSaved.Instance.posXObs;
            MyVentanaObs.Top = DataSaved.Instance.posYObs;

            Console.WriteLine("Actual X"+MyVentanaObs.Left);
            Console.WriteLine("Actual Y "+MyVentanaObs.Top);

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
            if (!DataSaved.Instance.isOpenObs && DataSaved.Instance.isConnectedOBS)
            {
                this.Hide();

            }

        }

        public void UpdateProgressBarMicro()
        {


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
            if (!blockTouch)
            {
                double posX = MyVentanaObs.Left;
                double posY = MyVentanaObs.Top;
                DataSaved.Instance.SaveDocObs(posX, posY);
                Console.WriteLine("SeGuardo");
                this.Hide();
            }
                
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
                MyVentanaObs.ResizeMode = ResizeMode.CanResizeWithGrip;

                Close.Visibility = Visibility.Visible;
            }
            else if (blockTouch == true)
            {
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoC.Visibility = Visibility.Visible;
                Close.Visibility = Visibility.Collapsed;
                MyVentanaObs.ResizeMode = ResizeMode.NoResize;
            }
        }



   
    }
    } 





    

