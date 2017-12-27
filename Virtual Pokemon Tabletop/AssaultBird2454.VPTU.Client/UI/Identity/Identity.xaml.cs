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

namespace AssaultBird2454.VPTU.Client.UI.Identity
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public UserIdentity UID;

        public Add(UserIdentity _UID = null)
        {
            InitializeComponent();

            if(_UID == null)
            {
                UID = new UserIdentity();
            }
            else
            {
                UID = _UID;
                Load();
            }
        }

        public void Save()
        {
            UID.AuthKey = AuthKey.Text;
            UID.Campaign_Name = Campaign_Name.Text;
            UID.ICN = ICN.Text;
            UID.Server_Address = Address.Text;
            UID.Server_Port = (int)Port.Value;
        }

        public void Load()
        {
            AuthKey.Text = UID.AuthKey;
            Campaign_Name.Text = UID.Campaign_Name;
            ICN.Text = UID.ICN;
            Address.Text = UID.Server_Address;
            Port.Value = UID.Server_Port;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Save();

            DialogResult = true;
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
