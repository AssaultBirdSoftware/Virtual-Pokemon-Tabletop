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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Entity
{
    /// <summary>
    /// Interaction logic for Pokemon_Character.xaml
    /// </summary>
    public partial class Pokemon_Character : Window
    {
        private SaveManager.SaveManager Manager;
        private EntityManager.Pokemon.PokemonCharacter PokemonData;

        public Pokemon_Character(SaveManager.SaveManager _Mgr, EntityManager.Pokemon.PokemonCharacter _PokemonData = null)
        {
            Manager = _Mgr;
            PokemonData = _PokemonData;

            InitializeComponent();

            LoadSpecies();
        }

        private void LoadSpecies()
        {
            foreach (VPTU.Pokedex.Pokemon.PokemonData data in Manager.SaveData.PokedexData.Pokemon)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = data.Species_DexID + " (" + data.Species_Name + ")";
                cbi.Tag = data;

                Basic_Species.Items.Add(cbi);
            }
        }

        /// <summary>
        /// Load the pokemon from the data
        /// </summary>
        public void Load()
        {

        }
        /// <summary>
        /// Loads the pokemon from the species list (Used when creating a new pokemon)
        /// </summary>
        public void LoadFromSpecies()
        {

        }

        /// <summary>
        /// Saves the data
        /// </summary>
        public void Save()
        {
            
        }

        /// <summary>
        /// Invokes a check to make sure that the species to to change because it will reset everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Basic_Species_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Updating pokemon with species data", "Selected Species", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
