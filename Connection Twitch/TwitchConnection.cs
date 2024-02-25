using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using TwitchLib.Api;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using TwitchLib.Client.Events;
using System.Linq;
using TwitchLib.Client.Extensions;
using static StatsLab.Connection_Twitch.TwitchConnection;


namespace StatsLab.Connection_Twitch
{

    

    public class TwitchConnection
    {
       
        public string userName = "marcosfedez96";
        public string password = "Mar-39488962";
        public string ChanelName = "marcosfedez96";

        //private TcpClient twitchClient;
        private StreamReader reader;
        private StreamWriter writer;
        

        private static List<string> scopes = new List<string> { "chat:read", "whispers:read", "whispers:edit", "chat:edit", "channel:moderate" };
        private static bool descaonectado;

        public async void ConnectTwitch()
        {

            try
            {

                var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

                var api = new TwitchAPI();
                api.Settings.ClientId = Configu.TwitchClientId;
                api.Settings.Secret = Configu.TwitchClientSecret;

                var streams = await api.Helix.Streams.GetStreamsAsync(userLogins: userLogins);

                if (streams.Streams.Length > 0)
                {

                    Console.WriteLine($"Espectadores actuales: {streams.Streams[0].ViewerCount}");
                    DataSaved.Instance.countViewers = streams.Streams[0].ViewerCount.ToString();

                   // DataSaved.Instance.isTwitchConnected = true;
                }
                else
                {
                    Console.WriteLine("Canal no encontrado o sin transmisión en vivo");
                }

            }
            catch (Exception ex)
            {

                //DataSaved.Instance.isTwitchConnected = false;
                MessageBox.Show("ingrese un canal valido");

            }



            try
            {
                // string deviceCode = await StartDeviceFlowAsync();
                //if (deviceCode != null)
                //{
                //   await WaitForAuthorizationAsync(deviceCode);
                ConsultViewers();
                DataSaved.Instance.isTwitchConnected = true;
                // }
            }
            catch (Exception ex)
            {
                DataSaved.Instance.isTwitchConnected = false;
                MessageBox.Show($"Error al conectar a Twitch: {ex.Message}");
            }
        }

        


        public async void ConsultViewers()
        {
            /*
            string clientId = Configu.TwitchClientId;
            string clientSecret = Configu.TwitchClientSecret;
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
                    //Console.WriteLine(streams.Streams[0].ViewerCount.ToString());

                }
                else
                {
                    // Console.WriteLine("Canal no encontrado o sin transmisión en vivo");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar el número de espectadores: {ex.Message}");
            }


            */
        }
    }
    public class Configu
    {

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string token;
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        public Configu()
        {

        }

        public Configu(string filepath)
        {
            string content = File.ReadAllText(filepath);
            var c = JsonConvert.DeserializeObject<Configu>(content);
            this.name = c.name;
            this.token = c.token;

        }
        internal string GetName()
        {
            return name;
        }
        internal string GetToken()
        {
            return token;
        }




        public static readonly string TwitchClientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";
        public static readonly string TwitchRedirectUri = "http://localhost:3000/";
        public static readonly string TwitchClientSecret = "i0x8f56wv6h46iwqxo6w2xvwnkiyev";
    }
}
