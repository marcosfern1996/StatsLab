﻿using StatsLab.Connection_OBS;
using System;

namespace StatsLab
{
    public class Flag
    {       
        StatsView statsView = new StatsView() ;  
        StatsViewTw statsViewTw = new StatsViewTw() ;


        public void AbrirMonitoreoObs()
        {
            statsView.Show();           
        }
        public void AbrirMonitoreoTwitch()
        {
            statsViewTw.Show();
        }
       
    }

   
}
