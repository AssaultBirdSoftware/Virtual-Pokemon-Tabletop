using System.Net;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

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
                var url = "http://vptu.assaultbirdsoftware.me/Updater/Messages.json";
                MessageData = JsonConvert.DeserializeObject<Data>(new WebClient().DownloadString(url));
                MainWindow = _MainWindow;

                LoadMessages();
            }
            catch
            {
                /* Broken JSON, Dont Care */
            }
        }

        private void LoadMessages()
        {
            var notice = new Notice();
            notice.SetContent(MessageData.Message, GetColor(MessageData.Back_Color));
            MainWindow.Messages.Children.Add(notice);
            notice.Visibility = Visibility.Visible;
        }

        private Brush GetColor(NoticeColors Color)
        {
            if (Color == NoticeColors.Red)
                return Brushes.Red;
            if (Color == NoticeColors.Yellow)
                return Brushes.Yellow;
            if (Color == NoticeColors.Green)
                return Brushes.Green;
            if (Color == NoticeColors.Blue)
                return Brushes.Blue;
            if (Color == NoticeColors.Orange)
                return Brushes.Orange;
            if (Color == NoticeColors.Purple)
                return Brushes.Purple;
            if (Color == NoticeColors.Black)
                return Brushes.Black;
            if (Color == NoticeColors.White)
                return Brushes.White;
            if (Color == NoticeColors.Gray)
                return Brushes.Gray;
            return Brushes.White;
        }
    }
}