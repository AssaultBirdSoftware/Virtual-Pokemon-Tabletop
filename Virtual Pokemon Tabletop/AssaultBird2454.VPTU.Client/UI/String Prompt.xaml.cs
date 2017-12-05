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
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    /// Interaction logic for String_Prompt.xaml
    /// </summary>
    public partial class String_Prompt : Window
    {
        public string Input { get; private set; }

        public String_Prompt(string Window_Title)
        {
            InitializeComponent();
            Title = Window_Title;
        }

        private void Sumbit_Button_Click(object sender, RoutedEventArgs e)
        {
            Input = Value.Text;
            DialogResult = true;
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Input = "";
            DialogResult = false;
            Close();
        }
    }
}
