using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using TwitchLib.Api;
using TwitchLib.Api.Auth;
using TwitchLib.Api.Helix;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Client;

namespace StatsLab.Connection_Twitch
{
    
        
    public  class TwitchConnection
    {
        public static readonly string TwitchClientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";
        public static readonly string TwitchRedirectUri = "http://localhost:3000/redirect/";
        public static readonly string TwitchClientSecret = "9zy5tcaks9wz8d3zzcekr4xgchkm9f";

      
        private readonly TwitchAPI api;

      
        public async void ConnectTwitch()
        {
            try
            {
               
                var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

                var api = new TwitchAPI();
                api.Settings.ClientId = TwitchClientId;
                api.Settings.Secret = TwitchClientSecret;

                var streams = await api.Helix.Streams.GetStreamsAsync(userLogins: userLogins);
                
                if (streams.Streams.Length > 0)
                {
                   
                    Console.WriteLine($"Espectadores actuales: {streams.Streams[0].ViewerCount}");
                    DataSaved.Instance.countViewers = streams.Streams[0].ViewerCount.ToString();

                    DataSaved.Instance.isTwitchConnected = true;
                }
                else
                {
                    Console.WriteLine("Canal no encontrado o sin transmisión en vivo");
                }

            } catch (Exception ex) {

                DataSaved.Instance.isTwitchConnected = false;
                MessageBox.Show("ingrese un canal valido");
            
            }
           

        }

        public async void ConsultViewers()
        {
          
                string clientId = TwitchClientId;
                string clientSecret = TwitchClientSecret;
                var api = new TwitchAPI();
                api.Settings.ClientId = clientId;
                api.Settings.Secret = clientSecret;


                var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

                try
                {
                    var streams = await api.Helix.Streams.GetStreamsAsync(userLogins: userLogins);

                    if (streams.Streams.Length > 0)
                    {

                        DataSaved.Instance.countViewers = streams.Streams[0].ViewerCount.ToString();
                        Console.WriteLine(streams.Streams[0].ViewerCount.ToString());

                    }
                    else
                    {
                        Console.WriteLine("Canal no encontrado o sin transmisión en vivo");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al consultar el número de espectadores: {ex.Message}");
                }
            


        }


    }


    
}
