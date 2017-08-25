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
            Init();
            LoadSpecies();

            //Stats_Test();
        }

        private void Init()
        {
            Dictionary<string, object> itemSource = new Dictionary<string, object>();
            foreach (BattleManager.Data.Type effect in Enum.GetValues(typeof(BattleManager.Data.Type)))
            {
                itemSource.Add(effect.ToString(), effect);
            }
            Basic_Types.ItemsSource = itemSource;

            Basic_Weight.ItemsSource = Enum.GetValues(typeof(VPTU.Pokedex.Entity.WeightClass));
            Basic_Size.ItemsSource = Enum.GetValues(typeof(VPTU.Pokedex.Entity.SizeClass));
            Basic_Nature.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.Nature));

            #region Defaulting
            Basic_Weight.SelectedIndex = 0;
            Basic_Size.SelectedIndex = 0;
            Basic_Nature.SelectedIndex = 0;
            #endregion
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
            PokemonData.Name = Basic_Name.Text;
            PokemonData.Species_DexID = (decimal)((VPTU.Pokedex.Pokemon.PokemonData)((ComboBoxItem)Basic_Species.SelectedItem).Tag).Species_DexID;
            #region Pokemon Types
            if (PokemonData.PokemonType == null)
                PokemonData.PokemonType = new List<BattleManager.Data.Type>();

            PokemonData.PokemonType.Clear();

            foreach (KeyValuePair<string, object> typesel in Basic_Types.SelectedItems)
            {
                BattleManager.Data.Type type = (BattleManager.Data.Type)typesel.Value;

                PokemonData.PokemonType.Add(type);
            }
            #endregion
            PokemonData.SizeClass = (VPTU.Pokedex.Entity.SizeClass)Basic_Size.SelectedItem;
            PokemonData.WeightClass = (VPTU.Pokedex.Entity.WeightClass)Basic_Weight.SelectedItem;
            PokemonData.Nature = (BattleManager.Data.Nature)Basic_Nature.SelectedItem;
            PokemonData.Notes = Basic_Desc.Text;
            #region Gender
            if (Basic_SexMale.IsChecked == true)
            {
                PokemonData.Gender = VPTU.Pokedex.Entity.Gender.Male;
            }
            else if (Basic_SexFemale.IsChecked == true)
            {
                PokemonData.Gender = VPTU.Pokedex.Entity.Gender.Female;
            }
            else if (Basic_SexNone.IsChecked == true)
            {
                PokemonData.Gender = VPTU.Pokedex.Entity.Gender.Genderless;
            }
            #endregion
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

        }
    }
}
