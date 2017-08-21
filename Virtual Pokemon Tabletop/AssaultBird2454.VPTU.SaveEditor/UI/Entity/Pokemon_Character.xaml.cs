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
using AssaultBird2454.VPTU.EntityManager.Data;

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
            Stats_Test();
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
            Reload_Stats();
        }
        /// <summary>
        /// Loads the pokemon from the species list (Used when creating a new pokemon)
        /// </summary>
        public void LoadFromSpecies()
        {
            Reload_Stats();
        }

        /// <summary>
        /// Saves the data
        /// </summary>
        public void Save()
        {
            
        }

        public void Reload_Stats()
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
            LoadFromSpecies();
        }

        private void Stats_Test()
        {
            EntityManager.Data.Stats HP = new EntityManager.Data.Stats();
            EntityManager.Data.Stats Attack = new EntityManager.Data.Stats();
            EntityManager.Data.Stats Defence = new EntityManager.Data.Stats();
            EntityManager.Data.Stats SpAttack = new EntityManager.Data.Stats();
            EntityManager.Data.Stats SpDefence = new EntityManager.Data.Stats();
            EntityManager.Data.Stats Speed = new EntityManager.Data.Stats();

            HP.Species = 5;
            HP.StatsChanged += StatsChanged;
            HP.Name = "HP";

            Attack.Species = 8;
            Attack.StatsChanged += StatsChanged;
            Attack.Name = "Attack";

            Defence.Species = 7;
            Defence.StatsChanged += StatsChanged;
            Defence.Name = "Defence";

            SpAttack.Species = 10;
            SpAttack.StatsChanged += StatsChanged;
            SpAttack.Name = "Special Attack";

            SpDefence.Species = 15;
            SpDefence.StatsChanged += StatsChanged;
            SpDefence.Name = "Special Defence";

            Speed.Species = 3;
            Speed.StatsChanged += StatsChanged;
            Speed.Name = "Speed";

            Stats_Editor.Items.Add(HP);
            Stats_Editor.Items.Add(Attack);
            Stats_Editor.Items.Add(Defence);
            Stats_Editor.Items.Add(SpAttack);
            Stats_Editor.Items.Add(SpDefence);
            Stats_Editor.Items.Add(Speed);
        }

        private void StatsChanged()
        {
            Stats_Editor.Items.Refresh();
        }
    }
}
