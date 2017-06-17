using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Console
{
    public enum LoggerLevel { Info, Warning, Error, Critical, Debug_Low, Debug_Full }
    public static class Logger
    {
        private static bool Debug_Low = true;
        public static bool Get_Debug_Low {
            get
            {
                return Debug_Low;
            }
        }

        private static bool Debug_Full = true;
        public static bool Get_Debug_Full
        {
            get
            {
                return Debug_Full;
            }
        }

        private static string LogFile_Path = "";
        public static string Get_LogFile_Path
        {
            get
            {
                return LogFile_Path;
            }
        }

        private static string LogFile_Name = "";
        public static string Get_LogFile_Name
        {
            get
            {
                return LogFile_Name;
            }
        }

        public static void Setup(string _LogFile_Path, string _LogFile_Name, bool _Debug_Low = false, bool _Debug_Full = false)
        {
            LogFile_Path = _LogFile_Path;
            LogFile_Name = _LogFile_Name.Replace("%Date%", DateTime.Now.ToShortTimeString());
            Debug_Low = _Debug_Low;
            Debug_Full = _Debug_Full;

            File.Create(LogFile_Path);
        }

        public static void Log(string Data, LoggerLevel Level)
        {

        }
    }
}
