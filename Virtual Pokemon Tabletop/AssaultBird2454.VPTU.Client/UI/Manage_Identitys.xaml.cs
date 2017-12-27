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

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    /// Interaction logic for Manage_Identities.xaml
    /// </summary>
    public partial class Manage_Identities
    {
        public Manage_Identities()
        {
            InitializeComponent();

            Load();
        }

        public void Load()
        {
            ID_List.Items.Clear();

            foreach (UserIdentity id in Program.Identities)
            {
                ID_List.Items.Add(id);
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Identity.Add add = new Identity.Add();

            bool? Pass = add.ShowDialog();
            if(Pass == true)
            {
                Program.Identities.Add(add.UID);
                ID_List.Items.Add(add.UID);
            }
        }

        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature Comming Soon", "Not Implemented");
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Identity.Add edit = new Identity.Add((UserIdentity)ID_List.SelectedItems[0]);
            edit.ShowDialog();

            Load();
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            UserIdentity id = (UserIdentity)ID_List.SelectedItems[0];

            Program.Identities.Remove(id);
            ID_List.Items.Remove(id);
        }
    }
}
