using System.Diagnostics;

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
        public bool microOn {  get; set; }
        public bool sourceOn{  get; set; }
        public string[] microName {  get; set; }
        public string sourceName {  get; set; }
        public double microdB {  get; set; }
        public double sourcedB{  get; set; }
        public double sourceMul{  get; set; }
        public string idTwitch {  get; set; }
        public string channelName {  get; set; }

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

    }
}
