using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Settings
{
    public delegate void Setting_Button_Pressed();

    /// <summary>
    /// Interaction logic for Setting_Value_Button.xaml
    /// </summary>
    public partial class Setting_Value_Button : UserControl
    {
        public Setting_Value_Button()
        {
            InitializeComponent();
        }

        [Description("Invoked when the button of the control is clicked")]
        public event Setting_Button_Pressed Button_Pressed;

        [Category("Settings Display"), Description("Changes the text on the left that represent what the button is for")]
        public string Name_Label
        {
            get
            {
                return (string)Node_Name.Content;
            }
            set
            {
                Node_Name.Content = value;
            }
        }
        [Category("Settings Display"), Description("Changes the text in the button")]
        public string Button_Label
        {
            get
            {
                return (string)Node_Button.Content;
            }
            set
            {
                Node_Button.Content = value;
            }
        }

        private void Node_Button_Click(object sender, RoutedEventArgs e)
        {
            Button_Pressed?.Invoke();
        }
    }
}
