using StatsLab.Connection_OBS;
using System;

namespace StatsLab
{
    public class Bandera
    {       
        StatsView statsView = new StatsView() ;  
        StatsViewTw statsViewTw = new StatsViewTw() ;

        public void ActualizarMicro()
        {
///statsView.UpdateStateMicro();           
        }

        public void ActualizarSource()
        {
          //  statsView.UpdateStateSource();          
        }

        public void ActualizarBarraMicro()
        {
           // statsView.UpdateProgressBarMicro();
        }

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
