using System.Diagnostics;
using System;
using System.Windows;

namespace StatsLab
{

    public partial class MainWindow : Window
    {
       

        SettingWindows _settingWindows;
        public MainWindow()
        {
            InitializeComponent(); _settingWindows = new SettingWindows();
            _settingWindows.Show();
            this.Close();
           
            
            
        }
        

    }
}
