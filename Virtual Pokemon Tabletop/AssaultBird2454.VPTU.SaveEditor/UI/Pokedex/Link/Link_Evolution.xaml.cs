using System.Windows;
using AssaultBird2454.VPTU.Pokedex.Pokemon;
using AssaultBird2454.VPTU.SaveEditor.UI.Pokedex.Select;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex.Link
{
    /// <summary>
    ///     Interaction logic for Move_Link.xaml
    /// </summary>
    public partial class Link_Evolution : Window
    {
        public Link_Evolutions LinkData;

        public Link_Evolution(Link_Evolutions _LinkData = null)
        {
            InitializeComponent();

            if (_LinkData != null)
            {
                LinkData = _LinkData;
                Load();
            }
            else
            {
                LinkData = new Link_Evolutions();
            }
        }

        public void Save()
        {
            LinkData.Pokemon_Evo = MainWindow.SaveManager.SaveData.PokedexData.Pokemon
                .Find(x => x.Species_Name == Pokemon_Name.Text).Species_DexID;
            //LinkData.Evo_Type = VPTU.Pokedex.Pokemon.Evolution_Type.Normal;
        }

        public void Load()
        {
            Pokemon_Name.Text = MainWindow.SaveManager.SaveData.PokedexData.Pokemon
                .Find(x => x.Species_DexID == LinkData.Pokemon_Evo).Species_Name;
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
            var sp = new Select_Pokemon();
            var pass = sp.ShowDialog();

            if (pass == true && sp.Selected_Pokemon != null)
                Pokemon_Name.Text = sp.Selected_Pokemon.Species_Name;
        }

        #endregion
    }
}