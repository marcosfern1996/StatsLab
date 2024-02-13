using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TwitchLib.Api;
using TwitchLib.Api.Helix;

namespace StatsLab.Connection_Twitch
{
   

    public  class TwitchConnection
    {
        Helix hel = new Helix();

        // Configura tu clientId y clientSecret obtenidos al registrar tu aplicación en el panel de desarrolladores de Twitch
        
        private readonly TwitchAPI api;

        // Crea una instancia del cliente de la API de Twitch


        
        public async void ConnectTwitch()
        {
            string clientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";
            string clientSecret = "cttt17udurkflc3a338yvhl6dklq5z";
            // Realiza la autenticación

            // Cambia el array de strings a una lista
            var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

            var api = new TwitchAPI();
            api.Settings.ClientId = clientId;
            api.Settings.Secret = clientSecret;

            var streams = await api.Helix.Streams.GetStreamsAsync(userLogins :userLogins);

            if (streams.Streams.Length > 0)
            {
                Console.WriteLine($"Espectadores actuales: {streams.Streams[0].ViewerCount}");
                DataSaved.Instance.countViewers = streams.Streams[0].ViewerCount.ToString();
            }
            else
            {
                Console.WriteLine("Canal no encontrado o sin transmisión en vivo");
            }

        }

        public async void ConsultViewers()
        {
            string clientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";
            string clientSecret = "cttt17udurkflc3a338yvhl6dklq5z";
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
