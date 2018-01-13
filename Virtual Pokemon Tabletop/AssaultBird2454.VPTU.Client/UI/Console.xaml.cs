using System;
using System.Windows.Controls;
using System.Windows.Media;
using AssaultBird2454.VPTU.Server.Class.Logging;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    ///     Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : UserControl, I_Logger
    {
        public Console()
        {
            InitializeComponent();
        }

        public Console(bool Debug)
        {
            InitializeComponent();
            LogDebug = Debug;
        }

        /// <summary>
        ///     Gets if the logger is logging Debug Entries
        /// </summary>
        public bool LogDebug { get; set; }

        /// <summary>
        ///     Gets and Sets the servers name
        /// </summary>
        public string ServerName { get; set; } = "Internal Server";

        public void Log(string Data, LoggerLevel Level)
        {
            var Fcolor = Colors.White;
            //Color Bcolor = Colors.Black;

            if (Level == LoggerLevel.Info)
            {
                Fcolor = Colors.Green;
                //Bcolor = Colors.Black;
            }
            else if (Level == LoggerLevel.Notice)
            {
                Fcolor = Colors.Blue;
                //Bcolor = Colors.Black;
            }
            else if (Level == LoggerLevel.Warning)
            {
                Fcolor = Colors.Yellow;
                //Bcolor = Colors.Black;
            }
            else if (Level == LoggerLevel.Error)
            {
                Fcolor = Colors.DarkOrange;
                //Bcolor = Colors.Black;
            }
            else if (Level == LoggerLevel.Fatil)
            {
                Fcolor = Colors.DarkRed;
                //Bcolor = Colors.DarkRed;
            }
            else if (Level == LoggerLevel.Debug)
            {
                if (!LogDebug)
                    return; // Dont log debug if dissabled

                Fcolor = Colors.Gray;
                //Bcolor = Colors.Black;
            }
            else if (Level == LoggerLevel.Audit)
            {
                Fcolor = Colors.DarkMagenta;
                //Bcolor = Colors.Black;
            }

            var Write = DateTime.Now + " [" + Level + "] <" + ServerName + "> -> " + Data + "\n";
            Console_Display.WriteOutput(Write, Fcolor);
        }

        public void Setup(Logger_Type Type)
        {
        }
    }
}