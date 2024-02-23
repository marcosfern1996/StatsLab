using OBSWebsocketDotNet;
using StatsLab.Connection_OBS;
using StatsLab.Connection_Twitch;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace StatsLab
{

    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class StatsViewTw : Window
    {
        private DispatcherTimer timer;
        TwitchConnection _twitchConnection;

        public bool blockTouch = false;
        public StatsViewTw()
        {
            InitializeComponent(); 
            _twitchConnection = new TwitchConnection();
            ShowInTaskbar = false;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5f);
            timer.Tick += rechargedTimer;
            timer.Start();

            DataSaved.Instance.LoadDocTwitch();
            MyWindowTwitch.Left = DataSaved.Instance.posXTwitch;
            MyWindowTwitch.Top = DataSaved.Instance.posYTwitch;

            if( DataSaved.Instance.heightTwitch > 10 && DataSaved.Instance.widthTwitch> 10){
                MyWindowTwitch.Height = DataSaved.Instance.heightTwitch;
                MyWindowTwitch.Width = DataSaved.Instance.widthTwitch;

            }


        }
        private void rechargedTimer(object sender, EventArgs e)
        {

            UpdateStateLock();
            if (DataSaved.Instance.isTwitchConnected)
            {
                

                    _twitchConnection.readChat();
                
                // Console.WriteLine(OBSConnector.Instance.obs.GetStreamStatus().IsActive);

                UpdateStateViewers();
                

                OBSWebsocket obs = OBSConnector.Instance.obs;

                // Obtén el estado de la transmisión
                if (DataSaved.Instance.isConnectedOBS)
                {
                    bool isStreaming = obs.GetStreamStatus().IsActive;
                    if (isStreaming)
                    {
                        // OBS está transmitiendo
                        InDirect.Visibility = Visibility.Visible;
                    }
                    else if (obs.GetStreamStatus().IsActive)
                    {
                        // OBS está reconectándose
                        InDirect.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // OBS no está transmitiendo ni reconectándose
                        InDirect.Visibility = Visibility.Collapsed;
                    }
                }

                // Verifica si OBS está actualmente transmitiendo
                
                
            }
            
        }

        public void UpdateStateViewers() {
            
            _twitchConnection.ConsultViewers();
            NumViewers.Content = DataSaved.Instance.countViewers;

        }

        private void TuVentana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11  )
            {
                Close.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoA.Visibility= Visibility.Collapsed;
            }
            else if(e.Key == Key.F11 )
            {
               
                Close.Visibility = Visibility.Visible;
                UpdateStateLock();
                
            }

        }

            private void ClosedButton(object sender, RoutedEventArgs e)
        {
            if (!blockTouch)
            {
                double posX = MyWindowTwitch.Left;
                double posY = MyWindowTwitch.Top;
                double winHeigh = MyWindowTwitch.Height;
                double winWidth = MyWindowTwitch.Width;
                DataSaved.Instance.SaveDocTwitch(posX, posY, winHeigh, winWidth);
                Console.WriteLine("SeGuardo");
                this.Hide();
            }
                

        }

        private void BlockButton(object sender, RoutedEventArgs e)
        {
            if (blockTouch)
            {
                blockTouch = false;
               
            }
            else
            { blockTouch = true;

               
            }
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
            if(DataSaved.Instance.isBlockTwitch) {
                CandadoC.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
            }else if(!DataSaved.Instance.isBlockTwitch && blockTouch == false)
            {
                CandadoA.Visibility = Visibility.Visible;
                CandadoC.Visibility = Visibility.Collapsed;
            }else if(!DataSaved.Instance.isBlockTwitch && blockTouch)
            {
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoC.Visibility = Visibility.Visible;
            }
            if (blockTouch == false)
            {
                
                Close.Visibility = Visibility.Visible;
                MyWindowTwitch.ResizeMode = ResizeMode.CanResizeWithGrip;
                MyWindowTwitch.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19E4E3E3"));
                MyWindowTwitch.BorderBrush = Brushes.Black;
            }
            else if (blockTouch == true)
            {
                
                Close.Visibility = Visibility.Collapsed;
                MyWindowTwitch.ResizeMode = ResizeMode.NoResize;
                MyWindowTwitch.BorderBrush = Brushes.Transparent;
                MyWindowTwitch.Background = Brushes.Transparent;
            }
        }
    }
}
