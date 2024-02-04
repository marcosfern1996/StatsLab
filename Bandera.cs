using StatsLab.Connection_OBS;
using System;

namespace StatsLab
{
    public class Bandera
    {       
        StatsView statsView = new StatsView() ;      

        public void ActualizarMicro()
        {
            statsView.UpdateStateMicro();           
        }

        public void ActualizarSource()
        {
            statsView.UpdateStateSource();          
        }

        public void ActualizarBarraMicro()
        {
            statsView.UpdateProgressBarMicro();
        }

        public void AbrirMonitoreo()
        {
            statsView.Show();           
        }

        public void ConectingTwitch()
        {
            statsView.GetStateTWitch();
        }     

    }

   
}
