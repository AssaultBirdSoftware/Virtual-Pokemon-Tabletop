using System.Windows;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    ///     Interaction logic for String_Prompt.xaml
    /// </summary>
    public partial class String_Prompt : Window
    {
        public String_Prompt(string Window_Title)
        {
            InitializeComponent();
            Title = Window_Title;
        }

        public string Input { get; private set; }

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