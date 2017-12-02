using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace AssaultBird2454.VPTU.Client.UI.Entity
{
    /// <summary>
    /// Interaction logic for EntityList.xaml
    /// </summary>
    public partial class EntityList : UserControl
    {
        public EntityList()
        {
            InitializeComponent();

            Test();
        }

        public TreeViewItem Create_Dir(string Name, TreeViewItem Parent = null)
        {
            TreeViewItem Child = new TreeViewItem() { Header = Name };

            if (Parent == null)
            {
                Tree.Items.Add(Child);
            }
            else
            {
                Parent.Items.Add(Child);
            }

            return Child;
        }
        public void Create_Entity(TreeViewItem Parent, Bitmap Image, string Name, List<KeyValuePair<System.Windows.Media.Color, string>> Viewers)
        {
            EntityListItem ELI = new EntityListItem();
            ELI.Update(Image, Name, Viewers);

            Parent.Items.Add(new TreeViewItem() { Header = ELI });
        }

        private void Test()
        {
            List<KeyValuePair<System.Windows.Media.Color, string>> PIs = new List<KeyValuePair<System.Windows.Media.Color, string>>();
            PIs.Add(new KeyValuePair<System.Windows.Media.Color, string>(new System.Windows.Media.Color() { A = 255, R = 255, G = 0, B = 0 }, "Gill Bates"));
            PIs.Add(new KeyValuePair<System.Windows.Media.Color, string>(new System.Windows.Media.Color() { A = 255, R = 0, G = 255, B = 20 }, "Tessa Marlow"));

            TreeViewItem Players = Create_Dir("Players");

            TreeViewItem GB = Create_Dir("Gill Bates", Players);
            TreeViewItem GB_Party = Create_Dir("Party", GB);

            Create_Entity(GB, new Bitmap(@"C:\Users\Tasman\Desktop\PTU\Pokemon Tiles\Pokemon\Sprites (large)\000.png"), "Gill Bates", PIs);
            Create_Entity(GB_Party, new Bitmap(@"C:\Users\Tasman\Desktop\PTU\Pokemon Tiles\Pokemon\Tokens\448.png"), "Lucario", PIs);
            Create_Entity(GB_Party, new Bitmap(@"C:\Users\Tasman\Desktop\PTU\Pokemon Tiles\Pokemon\Tokens\133.png"), "Eevee", PIs);


            TreeViewItem TM = Create_Dir("Tessa Marlow", Players);
            TreeViewItem TM_Party = Create_Dir("Party", TM);

            Create_Entity(TM, new Bitmap(@"C:\Users\Tasman\Desktop\PTU\Pokemon Tiles\Pokemon\Sprites (large)\000.png"), "Tessa Marlow", PIs);
            Create_Entity(TM_Party, new Bitmap(@"C:\Users\Tasman\Desktop\PTU\Pokemon Tiles\Pokemon\Tokens\149.png"), "Dragonite", PIs);
            Create_Entity(TM_Party, new Bitmap(@"C:\Users\Tasman\Desktop\PTU\Pokemon Tiles\Pokemon\Tokens\686_Inkay.png"), "Inkay", PIs);
        }
    }
}
