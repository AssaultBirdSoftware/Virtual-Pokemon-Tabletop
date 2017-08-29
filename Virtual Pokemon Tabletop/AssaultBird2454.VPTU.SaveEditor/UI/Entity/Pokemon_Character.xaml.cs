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

            Load();
        }
        private void Init()
        {
            #region Events
            Basic_Types.SelectionChangedEvent += Basic_Types_SelectionChangedEvent;
            #endregion

            Dictionary<string, object> itemso = new Dictionary<string, object>();
            foreach (BattleManager.Data.Type effect in Enum.GetValues(typeof(BattleManager.Data.Type)))
            {
                itemso.Add(effect.ToString(), effect);
            }
            Basic_Types.ItemsSource = itemso;

            Basic_Weight.ItemsSource = Enum.GetValues(typeof(VPTU.Pokedex.Entity.WeightClass));
            Basic_Size.ItemsSource = Enum.GetValues(typeof(VPTU.Pokedex.Entity.SizeClass));
            Basic_Nature.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.Nature));

            #region Defaulting
            Basic_Weight.SelectedIndex = 0;
            Basic_Size.SelectedIndex = 0;
            Basic_Nature.SelectedIndex = 0;
            #endregion

            LoadSpecies();
        }

        private List<ComboBoxItem> Species_List;
        /// <summary>
        /// Loads the pokemon species into the selection combobox
        /// </summary>
        private void LoadSpecies()
        {
            Species_List = new List<ComboBoxItem>();
            foreach (VPTU.Pokedex.Pokemon.PokemonData data in Manager.SaveData.PokedexData.Pokemon)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = data.Species_DexID + " (" + data.Species_Name + ")";
                cbi.Tag = data;

                Basic_Species.Items.Add(cbi);
                Species_List.Add(cbi);
            }
        }
        #endregion

        #region Save & Load Functions
        /// <summary>
        /// Load the pokemon from the data
        /// </summary>
        public void Load()
        {
            try { Basic_Name.Text = PokemonData.Name; } catch { }
            try
            {
                VPTU.Pokedex.Pokemon.PokemonData pokemon = Manager.SaveData.PokedexData.Pokemon.Find(x => x.Species_DexID == PokemonData.Species_DexID);
                Basic_Species.SelectedItem = Species_List.Find(x => x.Tag == pokemon);
            }
            catch { }

            try
            {
                #region Pokemon Types
                Dictionary<string, object> itemso = new Dictionary<string, object>();
                foreach (VPTU.BattleManager.Data.Type type in PokemonData.PokemonType)
                {
                    itemso.Add(type.ToString(), type);
                }
                Basic_Types.SelectedItems = itemso;
                #endregion
            }
            catch { }

            try { Basic_Size.SelectedItem = PokemonData.SizeClass; } catch { }
            try { Basic_Weight.SelectedItem = PokemonData.WeightClass; } catch { }
            try { Basic_Nature.SelectedItem = PokemonData.Nature; } catch { }
            try { Basic_Desc.Text = PokemonData.Notes; } catch { }

            try
            {
                #region Gender
                if (PokemonData.Gender == VPTU.Pokedex.Entity.Gender.Male)
                {
                    Basic_SexMale.IsChecked = true;
                }
                else if (PokemonData.Gender == VPTU.Pokedex.Entity.Gender.Female)
                {
                    Basic_SexFemale.IsChecked = true;
                }
                else if (PokemonData.Gender == VPTU.Pokedex.Entity.Gender.Genderless)
                {
                    Basic_SexNone.IsChecked = true;
                }
                #endregion
            }
            catch { }

            Reload_Stats();

            Basic_Species.IsEnabled = true;
        }
        /// <summary>
        /// Loads the pokemon from the species list (Used when creating a new pokemon)
        /// </summary>
        public void LoadFromSpecies()
        {
            VPTU.Pokedex.Pokemon.PokemonData NewPokemon = (VPTU.Pokedex.Pokemon.PokemonData)((ComboBoxItem)Basic_Species.SelectedItem).Tag;

            //try { PokemonData.Name = NewPokemon.Species_Name; } catch { }

            try
            {
                #region Pokemon Types
                PokemonData.PokemonType.Clear();
                foreach (VPTU.BattleManager.Data.Type type in NewPokemon.Species_Types)
                {
                    PokemonData.PokemonType.Add(type);
                }
                #endregion
            }
            catch { }

            try { PokemonData.SizeClass = NewPokemon.Species_SizeClass; } catch { }
            try { PokemonData.WeightClass = NewPokemon.Species_WeightClass; } catch { }

            try
            {
                PokemonData.HP_SpeciesBase = NewPokemon.Species_BaseStats_HP;
                PokemonData.Attack_SpeciesBase = NewPokemon.Species_BaseStats_Attack;
                PokemonData.Defence_SpeciesBase = NewPokemon.Species_BaseStats_Defence;
                PokemonData.SpAttack_SpeciesBase = NewPokemon.Species_BaseStats_SpAttack;
                PokemonData.SpDefence_SpeciesBase = NewPokemon.Species_BaseStats_SpDefence;
                PokemonData.Speed_SpeciesBase = NewPokemon.Species_BaseStats_Speed;
            }
            catch { }

            Load();
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
            Stats_SBase_HP.Content = PokemonData.HP_SpeciesBase;
            Stats_Base_HP.Content = PokemonData.HP_Base;
            Stats_Total_HP.Content = PokemonData.HP_Total;

            Stats_SBase_Attack.Content = PokemonData.Attack_SpeciesBase;
            Stats_Base_Attack.Content = PokemonData.Attack_Base;
            Stats_Total_Attack.Content = PokemonData.Attack_Total;
            Stats_Adj_Attack.Content = PokemonData.Attack_Adjusted;

            Stats_SBase_Defence.Content = PokemonData.Defence_SpeciesBase;
            Stats_Base_Defence.Content = PokemonData.Defence_Base;
            Stats_Total_Defence.Content = PokemonData.Defence_Total;
            Stats_Adj_Defence.Content = PokemonData.Defence_Adjusted;

            Stats_SBase_SpAttack.Content = PokemonData.SpAttack_SpeciesBase;
            Stats_Base_SpAttack.Content = PokemonData.SpAttack_Base;
            Stats_Total_SpAttack.Content = PokemonData.SpAttack_Total;
            Stats_Adj_SpAttack.Content = PokemonData.SpAttack_Adjusted;

            Stats_SBase_SpDefence.Content = PokemonData.SpDefence_SpeciesBase;
            Stats_Base_SpDefence.Content = PokemonData.SpDefence_Base;
            Stats_Total_SpDefence.Content = PokemonData.SpDefence_Total;
            Stats_Adj_SpDefence.Content = PokemonData.SpDefence_Adjusted;

            Stats_SBase_Speed.Content = PokemonData.Speed_SpeciesBase;
            Stats_Base_Speed.Content = PokemonData.Speed_Base;
            Stats_Total_Speed.Content = PokemonData.Speed_Total;
            Stats_Adj_Speed.Content = PokemonData.Speed_Adjusted;
        }

        #region HP
        private void Stats_Mod_HP_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.HP_BaseMod = (int)Stats_Mod_HP.Value;

                Stats_Base_HP.Content = PokemonData.HP_Base;
                Stats_Total_HP.Content = PokemonData.HP_Total;
            }
            catch { }
        }
        private void Stats_Add_HP_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.HP_AddStat = (int)Stats_Add_HP.Value;

                Stats_Total_HP.Content = PokemonData.HP_Total;
            }
            catch { }
        }
        #endregion
        #region Attack
        private void Stats_Mod_Attack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Attack_BaseMod = (int)Stats_Mod_Attack.Value;

                Stats_Base_Attack.Content = PokemonData.Attack_Base;
                Stats_Total_Attack.Content = PokemonData.Attack_Total;
                Stats_Adj_Attack.Content = PokemonData.Attack_Adjusted;
            }
            catch { }
        }
        private void Stats_Add_Attack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Attack_AddStat = (int)Stats_Add_Attack.Value;

                Stats_Total_Attack.Content = PokemonData.Attack_Total;
                Stats_Adj_Attack.Content = PokemonData.Attack_Adjusted;
            }
            catch { }
        }
        private void Stats_CS_Attack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Attack_CombatStage = (int)Stats_CS_Attack.Value;

                Stats_Adj_Attack.Content = PokemonData.Attack_Adjusted;
            }
            catch { }
        }
        #endregion
        #region Defence
        private void Stats_Mod_Defence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Defence_BaseMod = (int)Stats_Mod_Defence.Value;

                Stats_Base_Defence.Content = PokemonData.Defence_Base;
                Stats_Total_Defence.Content = PokemonData.Defence_Total;
                Stats_Adj_Defence.Content = PokemonData.Defence_Adjusted;
            }
            catch { }
        }
        private void Stats_Add_Defence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Defence_AddStat = (int)Stats_Add_Defence.Value;

                Stats_Total_Defence.Content = PokemonData.Defence_Total;
                Stats_Adj_Defence.Content = PokemonData.Defence_Adjusted;
            }
            catch { }
        }
        private void Stats_CS_Defence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Defence_CombatStage = (int)Stats_CS_Defence.Value;

                Stats_Adj_Defence.Content = PokemonData.Defence_Adjusted;
            }
            catch { }
        }
        #endregion
        #region Sp. Attack
        private void Stats_Mod_SpAttack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.SpAttack_BaseMod = (int)Stats_Mod_SpAttack.Value;

                Stats_Base_SpAttack.Content = PokemonData.SpAttack_Base;
                Stats_Total_SpAttack.Content = PokemonData.SpAttack_Total;
                Stats_Adj_SpAttack.Content = PokemonData.SpAttack_Adjusted;
            }
            catch { }
        }
        private void Stats_Add_SpAttack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.SpAttack_AddStat = (int)Stats_Add_SpAttack.Value;

                Stats_Total_SpAttack.Content = PokemonData.SpAttack_Total;
                Stats_Adj_SpAttack.Content = PokemonData.SpAttack_Adjusted;
            }
            catch { }
        }
        private void Stats_CS_SpAttack_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.SpAttack_CombatStage = (int)Stats_CS_SpAttack.Value;

                Stats_Adj_SpAttack.Content = PokemonData.SpAttack_Adjusted;
            }
            catch { }
        }
        #endregion
        #region Sp. Defence
        private void Stats_Mod_SpDefence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.SpDefence_BaseMod = (int)Stats_Mod_SpDefence.Value;

                Stats_Base_SpDefence.Content = PokemonData.SpDefence_Base;
                Stats_Total_SpDefence.Content = PokemonData.SpDefence_Total;
                Stats_Adj_SpDefence.Content = PokemonData.SpDefence_Adjusted;
            }
            catch { }
        }
        private void Stats_Add_SpDefence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.SpDefence_AddStat = (int)Stats_Add_SpDefence.Value;

                Stats_Total_SpDefence.Content = PokemonData.SpDefence_Total;
                Stats_Adj_SpDefence.Content = PokemonData.SpDefence_Adjusted;
            }
            catch { }
        }
        private void Stats_CS_SpDefence_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.SpDefence_CombatStage = (int)Stats_CS_SpDefence.Value;

                Stats_Adj_SpDefence.Content = PokemonData.SpDefence_Adjusted;
            }
            catch { }
        }
        #endregion
        #region Speed
        private void Stats_Mod_Speed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Speed_BaseMod = (int)Stats_Mod_Speed.Value;

                Stats_Base_Speed.Content = PokemonData.Speed_Base;
                Stats_Total_Speed.Content = PokemonData.Speed_Total;
                Stats_Adj_Speed.Content = PokemonData.Speed_Adjusted;
            }
            catch { }
        }
        private void Stats_Add_Speed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Speed_AddStat = (int)Stats_Add_Speed.Value;

                Stats_Total_Speed.Content = PokemonData.Speed_Total;
                Stats_Adj_Speed.Content = PokemonData.Speed_Adjusted;
            }
            catch { }
        }
        private void Stats_CS_Speed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                PokemonData.Speed_CombatStage = (int)Stats_CS_Speed.Value;

                Stats_Adj_Speed.Content = PokemonData.Speed_Adjusted;
            }
            catch { }
        }
        #endregion
        #endregion

        #region Basic Info (Change Events)
        private void Basic_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { PokemonData.Name = Basic_Name.Text; } catch { }
        }
        private void Basic_Species_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("Updating pokemon with species data", "Selected Species", MessageBoxButton.OK, MessageBoxImage.Information);

            if (Basic_Species.IsEnabled == true)
            {
                PokemonData.Species_DexID = (decimal)((VPTU.Pokedex.Pokemon.PokemonData)((ComboBoxItem)Basic_Species.SelectedItem).Tag).Species_DexID;
                LoadFromSpecies();
            }
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
            Reload_Stats();
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
