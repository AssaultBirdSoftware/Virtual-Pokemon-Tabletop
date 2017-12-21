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
    /// Interaction logic for Manage_Identitys.xaml
    /// </summary>
    public partial class Manage_Identitys
    {
        public Manage_Identitys()
        {
            InitializeComponent();

            Load();
        }

        public void Load()
        {
            ID_List.Items.Clear();

            foreach (SaveManager.Identity.Identity_Data id in Program.Identitys)
            {
                ID_List.Items.Add(id);
            }
        }
    }
}
