using Microsoft.Win32;
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
using System.ComponentModel;
using System.IO;

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

            foreach (Authentication_Manager.Data.ClientIdentity id in Program.Identities)
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
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Save Virtual PTU Identity File";
            OFD.DefaultExt = ".ptuif";
            OFD.Filter = "Pokemon Tabletop User Identity File (*.ptuif)|*.ptuif";
            OFD.CheckPathExists = true;
            OFD.CheckFileExists = true;
            OFD.FileOk += OFD_FileOk;

            OFD.ShowDialog();
        }

        private void OFD_FileOk(object sender, CancelEventArgs e)
        {
            Authentication_Manager.Data.ClientIdentity ID;

            using (StreamReader SR = new StreamReader(new FileStream(((OpenFileDialog)sender).FileName, FileMode.OpenOrCreate)))
            {
                ID = Newtonsoft.Json.JsonConvert.DeserializeObject<Authentication_Manager.Data.ClientIdentity>(SR.ReadToEnd());
            }

            Program.Identities.Add(ID);
            ID_List.Items.Add(ID);
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Identity.Add edit = new Identity.Add((Authentication_Manager.Data.ClientIdentity)ID_List.SelectedItems[0]);
            edit.ShowDialog();

            Load();
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            Authentication_Manager.Data.ClientIdentity id = (Authentication_Manager.Data.ClientIdentity)ID_List.SelectedItems[0];

            Program.Identities.Remove(id);
            ID_List.Items.Remove(id);
        }
    }
}
