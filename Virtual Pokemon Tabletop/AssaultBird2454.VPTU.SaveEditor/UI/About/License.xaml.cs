using System.Windows;

namespace AssaultBird2454.VPTU.SaveEditor.UI.About
{
    /// <summary>
    ///     Interaction logic for Licence.xaml
    /// </summary>
    public partial class License : Window
    {
        public License(string Data, string TitleText)
        {
            InitializeComponent();

            Title = TitleText;
            License_Text.Text = Data;
        }
    }
}