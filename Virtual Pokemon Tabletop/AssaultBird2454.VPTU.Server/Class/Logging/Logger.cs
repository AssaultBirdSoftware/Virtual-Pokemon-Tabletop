using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultBird2454.VPTU.Server.Class.Logging
{
    /// <summary>
    /// Defines what type the log entry is
    /// </summary>
    public enum LoggerLevel { Info, Notice, Warning, Error, Fatil, Debug, Audit }

    public class Logger
    {
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
        public StreamWriter FileStream;

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

        public Logger()
        {

        }

        /// <summary>
        /// Configures the Logger
        /// </summary>
        /// <param name="Dir">The Directory to write the file to</param>
        public void Setup(string Dir)
        {
            //TODO: Add an option to dissable logging to file

            LogFile_Dir = Dir;

            FileStream = new StreamWriter(LogFile_Dir, true);
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
                Console.ForegroundColor = ConsoleColor.Black;
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
            FileStream.WriteLine(Write);
        }
    }
}
