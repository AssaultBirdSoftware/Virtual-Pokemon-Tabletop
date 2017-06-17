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

namespace Launcher.Notice
{
    /// <summary>
    /// Interaction logic for Notice.xaml
    /// </summary>
    public partial class Notice : UserControl
    {
        public Notice()
        {
            InitializeComponent();
        }
        public void SetContent(string Message, Brush Brush_Color)
        {
            Notice_Message.Content = Message;// Sets the message
            Background = Brush_Color;// Sets the color
        }
    }
}
