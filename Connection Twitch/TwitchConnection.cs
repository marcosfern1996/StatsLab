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
using System.Net.Http;
using TwitchLib.PubSub;
using TwitchLib.PubSub.Events;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Interop;

namespace StatsLab.Connection_Twitch
{
    
    public static class Config
    {
        public static readonly string TwitchClientId = "30y1o0f4aisqenvpgnm3duwa8q77cl";
        public static readonly string TwitchRedirectUri = "http://localhost:3000/";
        public static readonly string TwitchClientSecret = "i0x8f56wv6h46iwqxo6w2xvwnkiyev";
    }



    public class TwitchConnection
    {
        public string userName = "marcosfedez96";
        public string password = "Mar-39488962";
        public string ChanelName = "marcosfedez96";

        private TcpClient twitchClient;
        private StreamReader reader;
        private StreamWriter writer;

        private static List<string> scopes = new List<string> { "chat:read", "whispers:read", "whispers:edit", "chat:edit", "channel:moderate" };

     

        private readonly TwitchAPI api;

        static async Task MainAsync()
        {
            Console.WriteLine("Twitch user access token example.");

            // ensure client id, secret, and redrect url are set
            validateCreds();

            // create twitch api instance
            var api = new TwitchLib.Api.TwitchAPI();
            api.Settings.ClientId = Config.TwitchClientId;

            // start local web server
            var server = new WebServer(Config.TwitchRedirectUri);

            // print out auth url
            Console.WriteLine($"Please authorize here:\n{getAuthorizationCodeUrl(Config.TwitchClientId, Config.TwitchRedirectUri, scopes)}");

            // listen for incoming requests
            var auth = await server.Listen();

            // exchange auth code for oauth access/refresh
            var resp = await api.Auth.GetAccessTokenFromCodeAsync(auth.Code, Config.TwitchClientSecret, Config.TwitchRedirectUri);

            // update TwitchLib's api with the recently acquired access token
            api.Settings.AccessToken = resp.AccessToken;

            // get the auth'd user
            var user = (await api.Helix.Users.GetUsersAsync()).Users[0];

            // print out all the data we've got
            Console.WriteLine($"Authorization success!\n\nUser: {user.DisplayName} (id: {user.Id})\nAccess token: {resp.AccessToken}\nRefresh token: {resp.RefreshToken}\nExpires in: {resp.ExpiresIn}\nScopes: {string.Join(", ", resp.Scopes)}");

            // refresh token
            var refresh = await api.Auth.RefreshAuthTokenAsync(resp.RefreshToken, Config.TwitchClientSecret);
            api.Settings.AccessToken = refresh.AccessToken;

            // confirm new token works
            user = (await api.Helix.Users.GetUsersAsync()).Users[0];

            // print out all the data we've got
            Console.WriteLine($"Authorization success!\n\nUser: {user.DisplayName} (id: {user.Id})\nAccess token: {refresh.AccessToken}\nRefresh token: {refresh.RefreshToken}\nExpires in: {refresh.ExpiresIn}\nScopes: {string.Join(", ", refresh.Scopes)}");

            // prevent console from closing
            Console.ReadLine();
        }

        private static string getAuthorizationCodeUrl(string clientId, string redirectUri, List<string> scopes)
        {
            var scopesStr = String.Join("+", scopes);

            return "https://id.twitch.tv/oauth2/authorize?" +
                   $"client_id={ clientId }&" +
                   $"redirect_uri={System.Net.WebUtility.UrlEncode(redirectUri)}&" +
                   "response_type=code&" +
                   $"scope={scopesStr}";
        }

        private static void validateCreds()
        {
            if (String.IsNullOrEmpty(Config.TwitchClientId))
                throw new Exception("client id cannot be null or empty");
            if (String.IsNullOrEmpty(Config.TwitchClientSecret))
                throw new Exception("client secret cannot be null or empty");
            if (String.IsNullOrEmpty(Config.TwitchRedirectUri))
                throw new Exception("redirect uri cannot be null or empty");
            Console.WriteLine($"Using client id '{Config.TwitchClientId}', secret '{Config.TwitchClientSecret}' and redirect url '{Config.TwitchRedirectUri}'.");
        }



        public async void ConnectTwitch()
        {
            try
            {
                await MainAsync();
                var userLogins = new System.Collections.Generic.List<string> { DataSaved.Instance.channelName };

                var api = new TwitchAPI();
                api.Settings.ClientId = Config.TwitchClientId;
                api.Settings.Secret = Config.TwitchClientSecret;

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
        public class WebServer
        {
            private HttpListener listener;

            public WebServer(string uri)
            {
                listener = new HttpListener();
                listener.Prefixes.Add(uri);
            }

            public async Task<Authorization> Listen()
            {
                listener.Start();
                return await onRequest();
            }

            private async Task<Authorization> onRequest()
            {
                while (listener.IsListening)
                {
                    var ctx = await listener.GetContextAsync();
                    var req = ctx.Request;
                    var resp = ctx.Response;

                    using (var writer = new StreamWriter(resp.OutputStream))
                    {
                        if (req.QueryString.AllKeys.Any("code".Contains))
                        {
                            writer.WriteLine("Authorization started! Check your application!");
                            writer.Flush();
                            return new Authorization(req.QueryString["code"]);
                        }
                        else
                        {
                            writer.WriteLine("No code found in query string!");
                            writer.Flush();
                        }
                    }
                }
                return null;
            }
        }

        public class Authorization
        {
            public string Code { get; }

            public Authorization(string code)
            {
                Code = code;
            }
        }

        public async void ConsultViewers()
        {

            string clientId = Config.TwitchClientId;
            string clientSecret = Config.TwitchClientSecret;
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
                    Console.WriteLine("Canal no encontrado o sin transmisión en vivo");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar el número de espectadores: {ex.Message}");
            }



        }
      
          //  TwitchConnection _TwitConnection = new TwitchConnection();

            //public TcpClient TwitchCtls;
            //public StreamReader reader;
            //public StreamWriter writer;

            

        public void connectChat()
            {
            twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
            reader = new StreamReader(twitchClient.GetStream());
            writer = new StreamWriter(twitchClient.GetStream());

            writer.WriteLine("PASS " + password);
            writer.WriteLine("NICK " + userName);
            writer.WriteLine("USER " + userName + " 8 * :" + userName);
            writer.WriteLine("JOIN #" + ChanelName);
            writer.Flush();

            Console.WriteLine("Connected to Twitch IRC");
        }

            public void readChat()
            {
            if(twitchClient != null)
            {
                if (twitchClient.Available > 0)
                {
                    string message = reader.ReadLine();
                    if (message.Contains("PRIVMSG"))
                    {
                        int splitPoint = message.IndexOf("!", 1);
                        string chatName = message.Substring(1, splitPoint - 1);

                        splitPoint = message.IndexOf(":", 1);
                        string chatMessage = message.Substring(splitPoint + 1);

                        RunFunction(chatName, chatMessage);
                    }
                }
            }
            

        }

            public void RunFunction(string username, string Chatmessage)
        {
                Console.WriteLine($"{username} said \"{Chatmessage}\"");
          
             }
        /*
        private async Task<string> StartDeviceFlowAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var deviceCodeResponse = await httpClient.PostAsync(TwitchDeviceEndpoint, new StringContent($"client_id={TwitchClientId}&scopes=viewing_activity_read", Encoding.UTF8, "application/x-www-form-urlencoded"));
                var deviceCodeJson = await deviceCodeResponse.Content.ReadAsStringAsync();

                if (!deviceCodeResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al iniciar el flujo de autorización de dispositivo: {deviceCodeJson}");
                    return null;
                }

                var deviceCode = GetJsonValue(deviceCodeJson, "device_code");
                var userCode = GetJsonValue(deviceCodeJson, "user_code");
                var verificationUri = GetJsonValue(deviceCodeJson, "verification_uri");

                Console.WriteLine($"Por favor, vaya a {verificationUri} e ingrese el código: {userCode}");

                return deviceCode;
            }
        }*/
        /*
        private async Task WaitForAuthorizationAsync(string deviceCode)
        {
            using (var httpClient = new HttpClient())
            {
                var interval = TimeSpan.FromSeconds(5);

                while (true)
                {
                    await Task.Delay(interval);

                    var tokenResponse = await httpClient.PostAsync(TwitchTokenEndpoint, new StringContent($"client_id={Config.TwitchClientId}&scope=viewing_activity_read&device_code={deviceCode}&grant_type=urn:ietf:params:oauth:grant-type:device_code", Encoding.UTF8, "application/x-www-form-urlencoded"));
                    var tokenJson = await tokenResponse.Content.ReadAsStringAsync();

                    if (tokenResponse.IsSuccessStatusCode)
                    {
                        var accessToken = GetJsonValue(tokenJson, "access_token");
                        Console.WriteLine($"Token de acceso obtenido: {accessToken}");
                        return;
                    }
                    else
                    {
                        var error = GetJsonValue(tokenJson, "error");
                        if (error == "authorization_pending")
                        {
                            Console.WriteLine("Autorización pendiente. Esperando al usuario...");
                        }
                        else if (error == "invalid_device_code")
                        {
                            Console.WriteLine("Código de dispositivo no válido. Reinicie el proceso.");
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"Error: {error}");
                            return;
                        }
                    }
                }
            }
        }
        *//*
        private static string GetJsonValue(string json, string key)
        {
            var startIdx = json.IndexOf($"\"{key}\":", StringComparison.Ordinal) + key.Length + 3;
            var endIdx = json.IndexOf("\"", startIdx, StringComparison.Ordinal);
            return json.Substring(startIdx, endIdx - startIdx);
        }*/



    }
   
}
