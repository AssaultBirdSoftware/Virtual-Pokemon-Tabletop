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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex.Link
{
    /// <summary>
    /// Interaction logic for Move_Link.xaml
    /// </summary>
    public partial class Link_Evolution : Window
    {
        public VPTU.Pokedex.Pokemon.Link_Evolutions LinkData;
        private SaveManager.SaveManager Manager;

        public Link_Evolution(SaveManager.SaveManager _Manager, VPTU.Pokedex.Pokemon.Link_Evolutions _LinkData = null)
        {
            Manager = _Manager;

            InitializeComponent();

            if(_LinkData != null)
            {
                LinkData = _LinkData;
                Load();
            }
            else { LinkData = new VPTU.Pokedex.Pokemon.Link_Evolutions(); }
        }

        public void Save()
        {
            LinkData.Pokemon_Evo = MainWindow.SaveManager.SaveData.PokedexData.Pokemon.Find(x => x.Species_Name == Pokemon_Name.Text).Species_DexID;
            //LinkData.Evo_Type = VPTU.Pokedex.Pokemon.Evolution_Type.Normal;
        }
        public void Load()
        {
            Pokemon_Name.Text = MainWindow.SaveManager.SaveData.PokedexData.Pokemon.Find(x => x.Species_DexID == LinkData.Pokemon_Evo).Species_Name;
        }

        private void Link_Button_Click(object sender, RoutedEventArgs e)
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

        #region Search For Move
        private void Move_Name_Select_Click(object sender, RoutedEventArgs e)
        {
            Select.Select_Pokemon sp = new Select.Select_Pokemon(Manager);
            bool? pass = sp.ShowDialog();

            if(pass == true && sp.Selected_Pokemon != null)
            {
                Pokemon_Name.Text = sp.Selected_Pokemon.Species_Name;
            }
        }
        #endregion
    }
}
