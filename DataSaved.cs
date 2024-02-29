using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Windows;

namespace StatsLab
{
    public class DataSaved
    {
        private static DataSaved _instance;

        public bool blockTouchobs {  get; set; }
        public bool blockTouchtwitch{  get; set; }

        public bool isOpenObs;
        public bool _isOpenObs
        {
            get { return isOpenObs; }
            set { isOpenObs = value; }
        }
        public bool isBlockObs { get; set; }
        public bool isBlockTwitch { get; set; }
        public double letterSize { get; set; }
        
        public bool ChatTw { get; set; }
        public bool Viewerstw { get; set; }

        public string PortOBS { get; set; }
        public string PasswordOBS { get; set; }
        public bool isConnectedOBS { get; set; }

      
        public double posXObs { get; set; }
        public double posYObs { get; set; }
        public double heightObs { get; set; }
        public double widthObs { get; set; }
        public int[] sourcesInput {  get; set; }
        public int[] sourcesCheck {  get; set; }

        public string SourceNum0 { get; set; }
        public string SourceNum1 { get; set; }
        public string SourceNum2 { get; set; }
        public string SourceNum3 { get; set; }
        public string SourceNum4 { get; set; }
        public string SourceNum5 { get; set; }
        public string SourceNum6 { get; set; }

        public double sourceMul0 { get; set; }
        public double sourceMul1 { get; set; }
        public double sourceMul2 { get; set; }
        public double sourceMul3 { get; set; }
        public double sourceMul4 { get; set; }
        public double sourceMul5 { get; set; }
        public double sourceMul6 { get; set; }

        public bool sourseAct0 { get; set; }
        public bool sourseAct1 { get; set; }
        public bool sourseAct2 { get; set; }
        public bool sourseAct3 { get; set; }
        public bool sourseAct4 { get; set; }
        public bool sourseAct5 { get; set; }
        public bool sourseAct6 { get; set; }

        public string countViewers { get; set; }
        public bool isTwitchConnected { get; set; }
        public bool isClienConnected { get; set; }
       public bool isStreaming {  get; set; }

        public double posXTwitch {  get; set; }
        public double posYTwitch {  get; set; }
        public double heightTwitch { get; set; }
        public double widthTwitch { get; set; }


        public string channelName { get; set; }
        public string messageTw{ get; set; }
        public string userMesstw{ get; set; }
        public string userColor{ get; set; }
        public bool newMessage {  get; set; }

        private DataSaved()
        {
        }

        public static DataSaved Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataSaved();
                }
                return _instance;
            }
        }

        
        public void isOpenedOBS()
        {
            _isOpenObs = IsProcessRunning("obs64");
           
        }

        public static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        public void SaveUserData(string channelName, string portOBS, double fontSize)
        {
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;
            string rutaArchivo = carpetaProyecto + "UserData.txt";

            try
            {
                using(StreamWriter userData = new StreamWriter(rutaArchivo))
                {
                    userData.WriteLine(channelName);
                    userData.WriteLine(portOBS);
                    userData.WriteLine(fontSize);
                }
            }catch(Exception ex)
            {

            }
        } 
        public void LoadUserData()
        {
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;
            string rutaArchivo = Path.Combine(carpetaProyecto, "UserData.txt");
            try
            {
                using(StreamReader lector = new StreamReader(rutaArchivo))
                {
                    channelName = lector.ReadLine();
                    PortOBS = lector.ReadLine();
                    letterSize = double.Parse(lector.ReadLine());
                }
            }
            catch
            {

            }
        }

        public void SaveDocTwitch(double posX ,double posY , double windheigh, double windwidth)
        {
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;
            string rutaArchivo = carpetaProyecto + "TwitchData.txt";

           
            try
            {
              
                using (StreamWriter siteTwichWIndow = new StreamWriter(rutaArchivo))
                {
                  
                    siteTwichWIndow.WriteLine($"{posX}");
                    siteTwichWIndow.WriteLine($"{posY}");
                    siteTwichWIndow.WriteLine($"{windheigh}");
                    siteTwichWIndow.WriteLine($"{windwidth}");
                }

                Console.WriteLine("Datos guardados correctamente en el archivo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar en el archivo: {ex.Message}");
            }
        }

        public void LoadDocTwitch()
        {
            
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;

            string rutaArchivo = Path.Combine(carpetaProyecto, "TwitchData.txt");

         
            try
            {
              
                using (StreamReader lector = new StreamReader(rutaArchivo))
                {
                 
                   
                    posXTwitch = double.Parse(lector.ReadLine());
                    posYTwitch = double.Parse(lector.ReadLine());
                    heightTwitch = double.Parse(lector.ReadLine());
                    widthTwitch= double.Parse(lector.ReadLine());
                }

              
                Console.WriteLine($"posicion X: {posXTwitch}");
                Console.WriteLine($"posicion Y: {posYTwitch}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer desde el archivo: {ex.Message}");
            }
        }
        
        public void SaveDocObs(double posX ,double posY, double windheigh, double windwidth)
        {
           
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;
            string rutaArchivo = carpetaProyecto + "ObsData.txt";

         
            try
            {
               
                using (StreamWriter siteTwichWIndow = new StreamWriter(rutaArchivo))
                {

                    siteTwichWIndow.WriteLine($"{posX}");
                    siteTwichWIndow.WriteLine($"{posY}");
                    siteTwichWIndow.WriteLine($"{windheigh}");
                    siteTwichWIndow.WriteLine($"{windwidth}");
                }

                Console.WriteLine("Datos guardados correctamente en el archivo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar en el archivo: {ex.Message}");
            }
        }


        public void LoadDocObs()
        {

        
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;

         
            string rutaArchivo = Path.Combine(carpetaProyecto, "ObsData.txt");

          
            try
            {
                using (StreamReader lector = new StreamReader(rutaArchivo))
                {
                  
                    posXObs = double.Parse(lector.ReadLine());
                    posYObs = double.Parse(lector.ReadLine());
                    heightObs = double.Parse(lector.ReadLine());
                    widthObs = double.Parse(lector.ReadLine());
                }

             
                Console.WriteLine($"posicion X: {posXObs}");
                Console.WriteLine($"posicion Y: {posYObs}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer desde el archivo: {ex.Message}");
            }
        }

    }
}
