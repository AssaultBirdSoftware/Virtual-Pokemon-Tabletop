using System.Windows.Controls;
using System.Windows.Media;

namespace Launcher.Notice
{
    /// <summary>
    ///     Interaction logic for Notice.xaml
    /// </summary>
    public partial class Notice : UserControl
    {
        public Notice()
        {
            InitializeComponent();
        }

        public void SetContent(string Message, Brush Brush_Color)
        {
            Notice_Message.Content = Message; // Sets the message
            Background = Brush_Color; // Sets the color
        }
    }
}