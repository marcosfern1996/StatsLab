using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Communication;
using OBSWebsocketDotNet.Types.Events;

using System;
using System.Windows.Forms;

namespace StatsLab.Connection_OBS
{
    public class OBSConnector
    {
        public bool micro;
        public bool musica;
        public OBSWebsocket obs;
        public event EventHandler<InputVolumeMetersEventArgs> InputVolumeMeters;
        public event EventHandler Connected;
        private static OBSConnector _instance;
        
        private OBSConnector()
        {
            
            
            obs = new OBSWebsocket();
            obs.Connected += OnConnected;
            
        }
/*
        public void UpdateTracks()
        {
            
            obs.InputVolumeMeters += OBSConnector_InputVolumeMeters;
            Console.WriteLine("hasta aca llego1");
        }

        private void OBSConnector_InputVolumeMeters(object sender, EventArgs e)
        {
           
            Console.WriteLine("hasta aca llego2");
            Console.WriteLine(e.ToString()); 
        }
*/
        public static OBSConnector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OBSConnector();
                }
                return _instance;
            }
        }       

        

        public void Connect(string port, string password)
        {
            obs.ConnectAsync("ws://localhost:" + port, password);
        }

        public void Disconnect()
        {
            obs.Disconnect();
        }

        private void OnConnected(object sender, EventArgs e)
        {
            if (DataSaved.Instance.isConnectedOBS)
            {
                MessageBox.Show("Actualizado");
            }
            else
            {
                MessageBox.Show("Estoy conectado");

            }
            
            Connected?.Invoke(this, EventArgs.Empty);
            DataSaved.Instance.isConnectedOBS = true;

           
        }
       
    }
}
