using StatsLab.Connection_Twitch;
using System;
using System.Windows;
using System.Windows.Input;
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
            //KeyDown += TuVentana_KeyDown;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10f);
            timer.Tick += rechargedTimer;
            timer.Start();
        }
        private void rechargedTimer(object sender, EventArgs e)
        {

           
            if (DataSaved.Instance.isTwitchConnected)
            {
                UpdateStateViewers();
                UpdateStateLock();
            }
            
        }

        public void UpdateStateViewers() {
            
            _twitchConnection.ConsultViewers();
            NumViewers.Content = DataSaved.Instance.countViewers;

        }

        private void TuVentana_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11 && Block.Visibility == Visibility.Visible)
            {
                Block.Visibility = Visibility.Collapsed;
                Close.Visibility = Visibility.Collapsed;
                CandadoA.Visibility = Visibility.Collapsed;
                CandadoA.Visibility= Visibility.Collapsed;
            }
            else if(e.Key == Key.F11 && Block.Visibility == Visibility.Collapsed)
            {
                Block.Visibility = Visibility.Visible;
                Close.Visibility = Visibility.Visible;
                UpdateStateLock();
                
            }

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
    }
}
