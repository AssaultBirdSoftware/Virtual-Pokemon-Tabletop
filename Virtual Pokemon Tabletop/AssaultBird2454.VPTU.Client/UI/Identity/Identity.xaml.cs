using System.Windows;
using AssaultBird2454.VPTU.Authentication_Manager.Data;

namespace AssaultBird2454.VPTU.Client.UI.Identity
{
    /// <summary>
    ///     Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public ClientIdentity UID;

        public Add(ClientIdentity _UID = null)
        {
            InitializeComponent();

            if (_UID == null)
            {
                UID = new ClientIdentity();
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
            UID.Server_Port = (int) Port.Value;
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