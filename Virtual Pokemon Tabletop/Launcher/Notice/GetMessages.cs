using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Launcher.Notice
{
    internal class MessageHandeler
    {
        private Data MessageData { get; set; }
        private MainWindow MainWindow { get; set; }

        public void GetMessages(MainWindow _MainWindow)
        {
            try
            {
                string url = "http://vptu.assaultbirdsoftware.me/Updater/Messages.json";
                MessageData = Newtonsoft.Json.JsonConvert.DeserializeObject<Data>((new WebClient()).DownloadString(url));
                MainWindow = _MainWindow;

                LoadMessages();
            }
            catch { /* Broken JSON, Dont Care */}
        }

        private void LoadMessages()
        {
            Notice notice = new Notice();
            notice.SetContent(MessageData.Message, GetColor(MessageData.Back_Color));
            MainWindow.Messages.Children.Add(notice);
            notice.Visibility = System.Windows.Visibility.Visible;
        }

        private System.Windows.Media.Brush GetColor(NoticeColors Color)
        {
            if (Color == NoticeColors.Red)
            {
                return System.Windows.Media.Brushes.Red;
            }
            else if (Color == NoticeColors.Yellow)
            {
                return System.Windows.Media.Brushes.Yellow;
            }
            else if (Color == NoticeColors.Green)
            {
                return System.Windows.Media.Brushes.Green;
            }
            else if (Color == NoticeColors.Blue)
            {
                return System.Windows.Media.Brushes.Blue;
            }
            else if (Color == NoticeColors.Orange)
            {
                return System.Windows.Media.Brushes.Orange;
            }
            else if (Color == NoticeColors.Purple)
            {
                return System.Windows.Media.Brushes.Purple;
            }
            else if (Color == NoticeColors.Black)
            {
                return System.Windows.Media.Brushes.Black;
            }
            else if (Color == NoticeColors.White)
            {
                return System.Windows.Media.Brushes.White;
            }
            else if (Color == NoticeColors.Gray)
            {
                return System.Windows.Media.Brushes.Gray;
            }
            else
            {
                return System.Windows.Media.Brushes.White;
            }
        }
    }
}
