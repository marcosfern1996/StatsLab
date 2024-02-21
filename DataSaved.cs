using System.Diagnostics;
using System.Net.Sockets;

using System.IO;
using OBSWebsocketDotNet.Types.Events;
using System;

namespace StatsLab
{
    public class DataSaved
    {
        private static DataSaved _instance;

        public bool isOpenObs;
        public bool _isOpenObs
        {
            get { return isOpenObs; }
            set { isOpenObs = value; }
        }

        public string PortOBS { get; set; }
        public string PasswordOBS { get; set; }
        public bool isConnectedOBS {  get; set; }
       
        public double[] microdB {  get; set; }
        public double sourcedB{  get; set; }


        public double posXObs { get; set; }
        public double posYObs { get; set; }


        public string SourceNum0 { get; set; }
        public string SourceNum1 { get; set; }
        public string SourceNum2 { get; set; }
        public string SourceNum3 { get; set; }
        public string SourceNum4 { get; set; }
        public string SourceNum5 { get; set; }
        public string SourceNum6 { get; set; } 

        public double sourceMul0 { get; set; }
        public double sourceMul1{  get; set; }
        public double sourceMul2{  get; set; }
        public double sourceMul3{  get; set; }
        public double sourceMul4{  get; set; }
        public double sourceMul5{  get; set; }
        public double sourceMul6{  get; set; }

        public bool sourseAct0 {  get; set; }
        public bool sourseAct1 {  get; set; }
        public bool sourseAct2 {  get; set; }
        public bool sourseAct3 {  get; set; }
        public bool sourseAct4 {  get; set; }
        public bool sourseAct5 {  get; set; }
        public bool sourseAct6 {  get; set; }

        public string countViewers{  get; set; }
        public bool isTwitchConnected{  get; set; }

        public double posXTwitch {  get; set; }
        public double posYTwitch {  get; set; }

        public TcpClient TwitchCtls { get; set; }
        public StreamReader reader { get; set; }
        public StreamWriter writer { get; set; }

        public string channelName { get; set; }

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
            isOpenObs = IsProcessRunning("obs64");
           
        }

        public static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }


        public void SaveDocTwitch(double posX ,double posY)
        {
            // Variables que deseas guardar

            // Ruta del archivo de texto
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;
            string rutaArchivo = carpetaProyecto + "TwitchData.txt";

            // Escribir en el archivo de texto
            try
            {
                // Abre el archivo en modo de escritura o lo crea si no existe
                using (StreamWriter siteTwichWIndow = new StreamWriter(rutaArchivo))
                {
                    // Escribe las variables en el archivo
                    siteTwichWIndow.WriteLine($"{posX}");
                    siteTwichWIndow.WriteLine($"{posY}");
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
            // Variables para almacenar los datos cargados
          
            // Obtiene la carpeta base del dominio de aplicación
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;

            // Ruta del archivo de texto en la ubicación del proyecto
            string rutaArchivo = Path.Combine(carpetaProyecto, "TwitchData.txt");

            // Leer desde el archivo de texto
            try
            {
                // Abre el archivo en modo de lectura
                using (StreamReader lector = new StreamReader(rutaArchivo))
                {
                    // Lee los datos desde el archivo
                   
                    posXTwitch = double.Parse(lector.ReadLine());
                    posYTwitch = double.Parse(lector.ReadLine());
                }

                // Muestra los datos cargados
                Console.WriteLine($"posicion X: {posXTwitch}");
                Console.WriteLine($"posicion Y: {posYTwitch}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer desde el archivo: {ex.Message}");
            }
        }
        
        public void SaveDocObs(double posX ,double posY)
        {
            // Variables que deseas guardar

            // Ruta del archivo de texto
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;
            string rutaArchivo = carpetaProyecto + "ObsData.txt";

            // Escribir en el archivo de texto
            try
            {
                // Abre el archivo en modo de escritura o lo crea si no existe
                using (StreamWriter siteTwichWIndow = new StreamWriter(rutaArchivo))
                {
                    // Escribe las variables en el archivo
                    siteTwichWIndow.WriteLine($"{posX}");
                    siteTwichWIndow.WriteLine($"{posY}");
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

            // Variables para almacenar los datos cargados
          
            // Obtiene la carpeta base del dominio de aplicación
            string carpetaProyecto = AppDomain.CurrentDomain.BaseDirectory;

            // Ruta del archivo de texto en la ubicación del proyecto
            string rutaArchivo = Path.Combine(carpetaProyecto, "ObsData.txt");

            // Leer desde el archivo de texto
            try
            {
                // Abre el archivo en modo de lectura
                using (StreamReader lector = new StreamReader(rutaArchivo))
                {
                    // Lee los datos desde el archivo

                    posXObs = double.Parse(lector.ReadLine());
                    posYObs = double.Parse(lector.ReadLine());
                }

                // Muestra los datos cargados
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
