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
        public SaveManager.SaveManager Manager;
        public EntityManager.Pokemon.PokemonCharacter PokemonData;

        #region Base Functions
        public Pokemon_Character(SaveManager.SaveManager _Mgr, EntityManager.Pokemon.PokemonCharacter _PokemonData = null)
        {
            Manager = _Mgr;

            if (_PokemonData == null)
            {
                PokemonData = new EntityManager.Pokemon.PokemonCharacter();
            }
            else
            {
                PokemonData = _PokemonData;
            }

            InitializeComponent();
            Init();
            LoadSpecies();
        }
        private void Init()
        {
            #region Events
            Basic_Types.SelectionChangedEvent += Basic_Types_SelectionChangedEvent;
            #endregion

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

        /// <summary>
        /// Loads the pokemon species into the selection combobox
        /// </summary>
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
        #endregion

        #region Save & Load Functions
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
        #endregion

        #region Stats
        public void Reload_Stats()
        {

        }

        #region HP
        private void Stats_Mod_HP_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.HP_BaseMod = (int)Stats_Mod_HP.Value;

                Stats_Base_HP.Content = PokemonData.HP_Base;
            }
            catch { }
        }
        private void Stats_Add_HP_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
        #endregion

        #endregion

        #region Basic Info (Change Events)
        private void Basic_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { PokemonData.Name = Basic_Name.Text; } catch { }
        }
        /// <summary>
        /// Invokes a check to make sure that the species to to change because it will reset everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Basic_Species_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Updating pokemon with species data", "Selected Species", MessageBoxButton.OK, MessageBoxImage.Information);

            PokemonData.Species_DexID = (decimal)((VPTU.Pokedex.Pokemon.PokemonData)((ComboBoxItem)Basic_Species.SelectedItem).Tag).Species_DexID;
            LoadFromSpecies();
        }
        private void Basic_Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PokemonData.SizeClass = (VPTU.Pokedex.Entity.SizeClass)Basic_Size.SelectedItem;
        }
        private void Basic_Weight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PokemonData.WeightClass = (VPTU.Pokedex.Entity.WeightClass)Basic_Weight.SelectedItem;
        }
        private void Basic_Nature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PokemonData.Nature = (BattleManager.Data.Nature)Basic_Nature.SelectedItem;
        }
        private void Basic_SexMale_Checked(object sender, RoutedEventArgs e)
        {
            PokemonData.Gender = VPTU.Pokedex.Entity.Gender.Male;
        }
        private void Basic_SexFemale_Checked(object sender, RoutedEventArgs e)
        {
            PokemonData.Gender = VPTU.Pokedex.Entity.Gender.Female;
        }
        private void Basic_SexNone_Checked(object sender, RoutedEventArgs e)
        {
            PokemonData.Gender = VPTU.Pokedex.Entity.Gender.Genderless;
        }
        private void Basic_Types_SelectionChangedEvent()
        {
            if (PokemonData.PokemonType == null)
                PokemonData.PokemonType = new List<BattleManager.Data.Type>();

            PokemonData.PokemonType.Clear();
            foreach (KeyValuePair<string, object> seltype in Basic_Types.SelectedItems)
            {
                PokemonData.PokemonType.Add((BattleManager.Data.Type)seltype.Value);
            }
        }
        #endregion
    }
}
