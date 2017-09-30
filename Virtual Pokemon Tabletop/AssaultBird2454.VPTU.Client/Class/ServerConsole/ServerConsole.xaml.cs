using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AssaultBird2454.VPTU.Server.Class.Logging;
using System.IO;

namespace AssaultBird2454.VPTU.Client.Class.ServerConsole
{
    /// <summary>
    /// Interaction logic for ServerConsole.xaml
    /// </summary>
    public partial class ServerConsole : UserControl, I_Logger
    {
        /// <summary>
        /// Current position that input starts at.
        /// </summary>
        private TextPointer inputStartPos;

        public ServerConsole()
        {
            InitializeComponent();
        }

        #region Variables
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

        public void Log(string Data, LoggerLevel Level)
        {
            if (Level == LoggerLevel.Debug && !LogDebug)
                return;

            string Write = DateTime.Now.ToString() + " [" + Level.ToString() + "] -> " + Data;

            //  Write the output.
            var range = new TextRange(Console.Document.ContentEnd, Console.Document.ContentEnd);
            range.Text = Write + "\n";

            if (Level == LoggerLevel.Info)
            {
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));
                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Black));
            }
            else if (Level == LoggerLevel.Notice)
            {
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Black));
            }
            else if (Level == LoggerLevel.Warning)
            {
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Yellow));
                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Black));
            }
            else if (Level == LoggerLevel.Error)
            {
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Black));
            }
            else if (Level == LoggerLevel.Fatil)
            {
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.White));
                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.DarkRed));
            }
            else if (Level == LoggerLevel.Debug)
            {
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Gray));
                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Black));
            }
            else if (Level == LoggerLevel.Audit)
            {
                range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.DarkMagenta));
                range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Black));
            }

            try
            {
                SR.WriteLine(Write);
                SR.Flush();
            }
            catch { /* File Logging not configured */ }

            //  Record the new input start.
            inputStartPos = Console.Document.ContentEnd.GetPositionAtOffset(0);
        }

        public void Setup(Logger_Type Type)
        {
            LogFile_Dir = MainWindow.AssemblyDirectory + @"\Logs\" + Type.ToString() + @"\" + DateTime.Now.ToLongDateString();

            if (!Directory.Exists(System.IO.Path.GetDirectoryName(LogFile_Dir)))
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(LogFile_Dir));

            FS = new FileStream(LogFile_Dir, FileMode.OpenOrCreate);
            SR = new StreamWriter(FS);
        }
    }
}
