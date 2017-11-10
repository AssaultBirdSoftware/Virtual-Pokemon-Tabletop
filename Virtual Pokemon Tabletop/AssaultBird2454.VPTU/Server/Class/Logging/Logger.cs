using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Class.Logging
{
    public enum Logger_Type { Client, Server, Other }
    /// <summary>
    /// Defines what type the log entry is
    /// </summary>
    public enum LoggerLevel { Info, Notice, Warning, Error, Fatil, Debug, Audit }

    public class Console_Logger : I_Logger
    {
        public Console_Logger()
        {

        }
        public Console_Logger(bool Debug)
        {
            LogDebug = Debug;
        }

        #region Variables, Properties & Events
        private string _LogFile_Dir = "";
        /// <summary>
        /// Gets the directory for the log file
        /// </summary>
        public string LogFile_Dir
        {
            get
            {
                return _LogFile_Dir;
            }
            set
            {
                _LogFile_Dir = value;
                // Invoke Event
                // Load the file
            }
        }
        private StreamWriter SR;
        private FileStream FS;


        private bool _LogDebug = false;
        /// <summary>
        /// Gets if the logger is logging Debug Entries
        /// </summary>
        public bool LogDebug
        {
            get
            {
                return _LogDebug;
            }
            set
            {
                _LogDebug = value;
                // Change Event
            }
        }
        #endregion

        /// <summary>
        /// Configures the Logger
        /// </summary>
        /// <param name="Dir">The Directory to write the file to</param>
        public void Setup(Logger_Type Type)
        {
            //TODO: Add an option to dissable logging to file

            LogFile_Dir = Main.AssemblyDirectory + @"\Logs\" + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString().Replace(':',' ');

            if (!Directory.Exists(Path.GetDirectoryName(LogFile_Dir)))
                Directory.CreateDirectory(Path.GetDirectoryName(LogFile_Dir));

            FS = new FileStream(LogFile_Dir, FileMode.OpenOrCreate);
            SR = new StreamWriter(FS);
        }

        public void Log(string Data, LoggerLevel Level)
        {
            if (Level == LoggerLevel.Info)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (Level == LoggerLevel.Notice)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (Level == LoggerLevel.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (Level == LoggerLevel.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (Level == LoggerLevel.Fatil)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            else if (Level == LoggerLevel.Debug)
            {
                if (!LogDebug)
                    return;// Dont log debug if dissabled

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (Level == LoggerLevel.Audit)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            string Write = DateTime.Now.ToString() + " [" + Level.ToString() + "] -> " + Data;

            Console.WriteLine(Write);
            try
            {
                SR.WriteLine(Write);
                SR.Flush();
            }
            catch { /* File Logging not configured */ }
        }
    }

    public interface I_Logger
    {
        /// <summary>
        /// Configures the logging class
        /// </summary>
        /// <param name="Dir">A optional path to define where the log fil will be saved</param>
        void Setup(Logger_Type Type);
        /// <summary>
        /// Sends a log entry to the logger
        /// </summary>
        /// <param name="Data">The Log Message</param>
        /// <param name="Level">Defines log type</param>
        void Log(string Data, LoggerLevel Level);
    }

    public class Invalid_Logger_Class_Exception : Exception
    {
        public Invalid_Logger_Class_Exception()
        {

        }
    }
}
