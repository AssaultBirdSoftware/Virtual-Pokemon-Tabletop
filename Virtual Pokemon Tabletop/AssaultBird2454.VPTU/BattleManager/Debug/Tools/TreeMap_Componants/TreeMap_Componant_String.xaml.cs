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

namespace AssaultBird2454.VPTU.BattleManager.Debug.Tools.TreeMap_Componants
{
    /// <summary>
    /// Interaction logic for TreeMap_Componant_String.xaml
    /// </summary>
    public partial class TreeMap_Componant_String : UserControl
    {
        [Description("Specifies the text that the label should siaplay")]
        public string Label
        {
            get { return (string)Display_Label.Content; }
            set { Display_Label.Content = value; }
        }
        [Description("The text in the Textbox")]
        public string Value
        {
            get { return Display_Value.Text; }
            set { Display_Value.Text = value; }
        }

        public TreeMap_Componant_String()
        {
            InitializeComponent();
        }

        private void Display_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Value = Display_Value.Text;
        }
    }
}
