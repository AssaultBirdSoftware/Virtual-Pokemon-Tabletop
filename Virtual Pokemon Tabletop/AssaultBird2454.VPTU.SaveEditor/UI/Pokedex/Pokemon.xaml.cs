using AssaultBird2454.VPTU.SaveEditor.UI.Pokedex.Link;
using AssaultBird2454.VPTU.Pokedex.Entity;
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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex
{
    /// <summary>
    /// Interaction logic for Pokemon.xaml
    /// </summary>
    public partial class Pokemon : Window
    {
        public VPTU.Pokedex.Pokemon.PokemonData PokemonData;
        private SaveManager.SaveManager Mgr;
        bool Update = false;

        public Pokemon(SaveManager.SaveManager _Mgr, VPTU.Pokedex.Pokemon.PokemonData _PokemonData = null)
        {
            InitializeComponent();// Sets up the window

            Mgr = _Mgr;// Creates Save Data Reference

            Setup();// Executes Setup Code

            if (_PokemonData == null)
            {
                PokemonData = new VPTU.Pokedex.Pokemon.PokemonData();
            }
            else
            {
                PokemonData = _PokemonData;
                Update = true;
                Load();
            }
        }

        #region Main Functions
        /// <summary>
        /// Used to setup some fields
        /// </summary>
        private void Setup()
        {
            #region Populating Fields
            //Basic Data
            #region Types
            Dictionary<string, object> itemSource = new Dictionary<string, object>();
            foreach (BattleManager.Data.Type effect in Enum.GetValues(typeof(BattleManager.Data.Type)))
            {
                itemSource.Add(effect.ToString(), effect);
            }
            Basic_Types.ItemsSource = itemSource;
            #endregion
            Basic_Weight.ItemsSource = Enum.GetValues(typeof(WeightClass));
            Basic_Size.ItemsSource = Enum.GetValues(typeof(SizeClass));

            //Skill Data
            Skill_Acrobatics_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Athletics_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Charm_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Combat_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Command_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Focus_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_GeneralEDU_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Gulie_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Intimidate_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Intuition_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_MedicineEDU_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_OccultEDU_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Perception_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_PokemonEDU_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Stealth_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_Survival_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));
            Skill_TechnologyEDU_Rank.ItemsSource = Enum.GetValues(typeof(SkillRank));

            //Capabilities Data
            Capabilities_NatureWalk_1.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.NatureWalk_Type));
            Capabilities_NatureWalk_2.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.NatureWalk_Type));
            #endregion
            #region Defaulting
            //Skill Data
            Skill_Acrobatics_Rank.SelectedIndex = 0;
            Skill_Athletics_Rank.SelectedIndex = 0;
            Skill_Charm_Rank.SelectedIndex = 0;
            Skill_Combat_Rank.SelectedIndex = 0;
            Skill_Command_Rank.SelectedIndex = 0;
            Skill_Focus_Rank.SelectedIndex = 0;
            Skill_GeneralEDU_Rank.SelectedIndex = 0;
            Skill_Gulie_Rank.SelectedIndex = 0;
            Skill_Intimidate_Rank.SelectedIndex = 0;
            Skill_Intuition_Rank.SelectedIndex = 0;
            Skill_MedicineEDU_Rank.SelectedIndex = 0;
            Skill_OccultEDU_Rank.SelectedIndex = 0;
            Skill_Perception_Rank.SelectedIndex = 0;
            Skill_PokemonEDU_Rank.SelectedIndex = 0;
            Skill_Stealth_Rank.SelectedIndex = 0;
            Skill_Survival_Rank.SelectedIndex = 0;
            Skill_TechnologyEDU_Rank.SelectedIndex = 0;

            //Capabilities Data
            Capabilities_NatureWalk_1.SelectedIndex = 0;
            Capabilities_NatureWalk_2.SelectedIndex = 0;
            #endregion

            #region Populating Special Capabilities
            foreach (VPTU.Pokedex.Pokemon.Pokemon_Capabilities cap in Enum.GetValues(typeof(VPTU.Pokedex.Pokemon.Pokemon_Capabilities)))
            {
                string CapName = cap.ToString();
                CheckBox box = new CheckBox();

                #region Logic for Values
                if (cap.ToString().ToUpper().StartsWith("I_"))
                {
                    MessageBox.Show("There is a Capabilities that needs a 'Numeric Value'. But is not implemented yet");
                }
                else if (cap.ToString().ToUpper().StartsWith("S_"))
                {
                    MessageBox.Show("There is a Capabilities that needs a 'String Value'. But is not implemented yet");
                }
                #endregion

                box.Content = cap.ToString().Replace('_', ' ');
                box.Tag = cap;
                box.Padding = new Thickness(0, 0, 5, 3);

                Capabilities_Wrap.Children.Add(box);
            }
            #endregion
        }

        /// <summary>
        /// Loads the data from a Dex Entry, Can be used to reset the page
        /// </summary>
        private void Load(VPTU.Pokedex.Pokemon.PokemonData _LoadData = null)
        {
            VPTU.Pokedex.Pokemon.PokemonData LoadData;
            if (_LoadData != null)
            {
                LoadData = _LoadData;
            }
            else
            {
                LoadData = PokemonData;
            }

            //Save Basic Pokemon Data
            #region Basic Info
            try { Basic_Name.Text = LoadData.Species_Name; } catch { }
            try { Basic_Desc.Text = LoadData.Species_Desc; } catch { } // Try and catch this as old versions of the save will not be able to read this?
            try { Basic_ID.Text = LoadData.Species_DexID.ToString(); } catch { }
            try
            {
                Dictionary<string, object> itemso = new Dictionary<string, object>();
                foreach (VPTU.BattleManager.Data.Type type in PokemonData.Species_Types)
                {
                    itemso.Add(type.ToString(), type);
                }
                Basic_Types.SelectedItems = itemso;
            }
            catch { }// Types
            try { Basic_Weight.SelectedItem = LoadData.Species_WeightClass; } catch { }
            try { Basic_Size.SelectedItem = LoadData.Species_SizeClass; } catch { }
            #endregion
            //Load Pokemon Skill Data
            #region Skill Data
            //Skill Rank Data
            try { Skill_Acrobatics_Rank.SelectedItem = LoadData.Species_Skill_Data.Acrobatics_Rank; } catch { }
            try { Skill_Athletics_Rank.SelectedItem = LoadData.Species_Skill_Data.Athletics_Rank; } catch { }
            try { Skill_Charm_Rank.SelectedItem = LoadData.Species_Skill_Data.Charm_Rank; } catch { }
            try { Skill_Combat_Rank.SelectedItem = LoadData.Species_Skill_Data.Combat_Rank; } catch { }
            try { Skill_Command_Rank.SelectedItem = LoadData.Species_Skill_Data.Command_Rank; } catch { }
            try { Skill_Focus_Rank.SelectedItem = LoadData.Species_Skill_Data.Focus_Rank; } catch { }
            try { Skill_GeneralEDU_Rank.SelectedItem = LoadData.Species_Skill_Data.General_Rank; } catch { }
            try { Skill_Gulie_Rank.SelectedItem = LoadData.Species_Skill_Data.Guile_Rank; } catch { }
            try { Skill_Intimidate_Rank.SelectedItem = LoadData.Species_Skill_Data.Intimidate_Rank; } catch { }
            try { Skill_Intuition_Rank.SelectedItem = LoadData.Species_Skill_Data.Intuition_Rank; } catch { }
            try { Skill_MedicineEDU_Rank.SelectedItem = LoadData.Species_Skill_Data.Medicine_Rank; } catch { }
            try { Skill_OccultEDU_Rank.SelectedItem = LoadData.Species_Skill_Data.Occult_Rank; } catch { }
            try { Skill_Perception_Rank.SelectedItem = LoadData.Species_Skill_Data.Perception_Rank; } catch { }
            try { Skill_PokemonEDU_Rank.SelectedItem = LoadData.Species_Skill_Data.Pokemon_Rank; } catch { }
            try { Skill_Stealth_Rank.SelectedItem = LoadData.Species_Skill_Data.Stealth_Rank; } catch { }
            try { Skill_Survival_Rank.SelectedItem = LoadData.Species_Skill_Data.Survival_Rank; } catch { }
            try { Skill_TechnologyEDU_Rank.SelectedItem = LoadData.Species_Skill_Data.Technology_Rank; } catch { }

            //Skill Mod Data
            try { Skill_Acrobatics_Mod.Value = LoadData.Species_Skill_Data.Acrobatics_Mod; } catch { }
            try { Skill_Athletics_Mod.Value = LoadData.Species_Skill_Data.Athletics_Mod; } catch { }
            try { Skill_Charm_Mod.Value = LoadData.Species_Skill_Data.Charm_Mod; } catch { }
            try { Skill_Combat_Mod.Value = LoadData.Species_Skill_Data.Combat_Mod; } catch { }
            try { Skill_Command_Mod.Value = LoadData.Species_Skill_Data.Command_Mod; } catch { }
            try { Skill_Focus_Mod.Value = LoadData.Species_Skill_Data.Focus_Mod; } catch { }
            try { Skill_GeneralEDU_Mod.Value = LoadData.Species_Skill_Data.General_Mod; } catch { }
            try { Skill_Gulie_Mod.Value = LoadData.Species_Skill_Data.Guile_Mod; } catch { }
            try { Skill_Intimidate_Mod.Value = LoadData.Species_Skill_Data.Intimidate_Mod; } catch { }
            try { Skill_Intuition_Mod.Value = LoadData.Species_Skill_Data.Intuition_Mod; } catch { }
            try { Skill_MedicineEDU_Mod.Value = LoadData.Species_Skill_Data.Medicine_Mod; } catch { }
            try { Skill_OccultEDU_Mod.Value = LoadData.Species_Skill_Data.Occult_Mod; } catch { }
            try { Skill_Perception_Mod.Value = LoadData.Species_Skill_Data.Perception_Mod; } catch { }
            try { Skill_PokemonEDU_Mod.Value = LoadData.Species_Skill_Data.Pokemon_Mod; } catch { }
            try { Skill_Stealth_Mod.Value = LoadData.Species_Skill_Data.Stealth_Mod; } catch { }
            try { Skill_Survival_Mod.Value = LoadData.Species_Skill_Data.Survival_Mod; } catch { }
            try { Skill_TechnologyEDU_Mod.Value = LoadData.Species_Skill_Data.Technology_Mod; } catch { }
            #endregion
            //Load Pokemon Capabilities Data
            #region Capabilities
            try { Capabilities_NatureWalk_1.SelectedItem = LoadData.Species_Capability_Data.NatureWalk_1; } catch { }
            try { Capabilities_NatureWalk_2.SelectedItem = LoadData.Species_Capability_Data.NatureWalk_2; } catch { }

            try { Capabilities_Burrow.Value = LoadData.Species_Capability_Data.Burrow; } catch { }
            try { Capabilities_HighJump.Value = LoadData.Species_Capability_Data.HighJump; } catch { }
            try { Capabilities_Levitate.Value = LoadData.Species_Capability_Data.Levitate; } catch { }
            try { Capabilities_LongJump.Value = LoadData.Species_Capability_Data.LongJump; } catch { }
            try { Capabilities_Overland.Value = LoadData.Species_Capability_Data.Overland; } catch { }
            try { Capabilities_Power.Value = LoadData.Species_Capability_Data.Power; } catch { }
            try { Capabilities_Sky.Value = LoadData.Species_Capability_Data.Sky; } catch { }
            try { Capabilities_Swim.Value = LoadData.Species_Capability_Data.Swim; } catch { }
            try { Capabilities_Teleport.Value = LoadData.Species_Capability_Data.Teleport; } catch { }
            try { Capabilities_ThrowingRange.Value = LoadData.Species_Capability_Data.ThrowingRange; } catch { }
            #endregion
            //Load Special Capabilities Data
            #region Special Capabilities
            try
            {
                foreach (CheckBox box in Capabilities_Wrap.Children)
                {
                    if (LoadData.Species_SpecialCapability.FindAll(x => x.Key == (VPTU.Pokedex.Pokemon.Pokemon_Capabilities)box.Tag).Count >= 1)
                    {
                        box.IsChecked = true;

                        //Set Values (If Applicable)
                    }
                    else
                    {
                        box.IsChecked = false;
                    }
                }
            }
            catch { }
            #endregion
            //Load Pokemon Base Stat Data
            #region Base Stats
            try { BaseStats_HP.Value = LoadData.Species_BaseStats_HP; } catch { }
            try { BaseStats_Attack.Value = LoadData.Species_BaseStats_Attack; } catch { }
            try { BaseStats_Defence.Value = LoadData.Species_BaseStats_Defence; } catch { }
            try { BaseStats_SpAttack.Value = LoadData.Species_BaseStats_SpAttack; } catch { }
            try { BaseStats_SpDefence.Value = LoadData.Species_BaseStats_SpDefence; } catch { }
            try { BaseStats_Speed.Value = LoadData.Species_BaseStats_Speed; } catch { }
            #endregion

            //All the linked objects get loaded here
            #region Links
            //Load Linked Resources
            #region Resources

            #endregion
            //Load Linked Move Data
            #region Moves
            try
            {
                Moves_List.Items.Clear();

                if (LoadData.Moves == null) { LoadData.Moves = new List<VPTU.Pokedex.Pokemon.Link_Moves>(); }
                foreach (VPTU.Pokedex.Pokemon.Link_Moves ML in LoadData.Moves)
                {
                    Moves_List.Items.Add(ML);
                }
            }
            catch { }
            #endregion
            //Load Linked Forms and Evolution Data here
            #region Evo Forms
            try
            {
                FormsAndEvos_List.Items.Clear();

                if (LoadData.Evolutions == null) { LoadData.Evolutions = new List<VPTU.Pokedex.Pokemon.Link_Evolutions>(); }
                foreach (VPTU.Pokedex.Pokemon.Link_Evolutions EL in LoadData.Evolutions)
                {
                    EvoLinks link = new EvoLinks();
                    link.LinkData = EL;
                    link.PokemonName = Mgr.SaveData.PokedexData.Pokemon.Find(x => x.Species_DexID == EL.Pokemon_Evo).Species_Name;

                    FormsAndEvos_List.Items.Add(link);
                }
            }
            catch { }
            #endregion
            #endregion
        }

        /// <summary>
        /// Saves Modifications or Adds a new entry to the Pokedex
        /// </summary>
        private void Save()
        {
            //Save Basic Pokemon Data
            #region Basic Info
            PokemonData.Species_Name = Basic_Name.Text;
            PokemonData.Species_Desc = Basic_Desc.Text;
            PokemonData.Species_DexID = Convert.ToDecimal(Basic_ID.Value);

            #region Pokemon Types
            if (PokemonData.Species_Types == null)
                PokemonData.Species_Types = new List<BattleManager.Data.Type>();

            PokemonData.Species_Types.Clear();

            try
            {
                foreach (KeyValuePair<string, object> typesel in Basic_Types.SelectedItems)
                {
                    BattleManager.Data.Type type = (BattleManager.Data.Type)typesel.Value;

                    PokemonData.Species_Types.Add(type);
                }
            }
            catch { }
            #endregion

            PokemonData.Species_WeightClass = (WeightClass)Basic_Weight.SelectedItem;
            PokemonData.Species_SizeClass = (SizeClass)Basic_Size.SelectedItem;
            #endregion
            //Save Pokemon Skill Data
            #region Skill Data
            //Skill Rank Data
            PokemonData.Species_Skill_Data.Acrobatics_Rank = (SkillRank)Skill_Acrobatics_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Athletics_Rank = (SkillRank)Skill_Athletics_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Charm_Rank = (SkillRank)Skill_Charm_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Combat_Rank = (SkillRank)Skill_Combat_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Command_Rank = (SkillRank)Skill_Command_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Focus_Rank = (SkillRank)Skill_Focus_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.General_Rank = (SkillRank)Skill_GeneralEDU_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Guile_Rank = (SkillRank)Skill_Gulie_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Intimidate_Rank = (SkillRank)Skill_Intimidate_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Intuition_Rank = (SkillRank)Skill_Intuition_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Medicine_Rank = (SkillRank)Skill_MedicineEDU_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Occult_Rank = (SkillRank)Skill_OccultEDU_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Perception_Rank = (SkillRank)Skill_Perception_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Pokemon_Rank = (SkillRank)Skill_PokemonEDU_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Stealth_Rank = (SkillRank)Skill_Stealth_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Survival_Rank = (SkillRank)Skill_Survival_Rank.SelectedItem;
            PokemonData.Species_Skill_Data.Technology_Rank = (SkillRank)Skill_TechnologyEDU_Rank.SelectedItem;

            //Skill Mod Data
            PokemonData.Species_Skill_Data.Acrobatics_Mod = (int)Skill_Acrobatics_Mod.Value;
            PokemonData.Species_Skill_Data.Athletics_Mod = (int)Skill_Athletics_Mod.Value;
            PokemonData.Species_Skill_Data.Charm_Mod = (int)Skill_Charm_Mod.Value;
            PokemonData.Species_Skill_Data.Combat_Mod = (int)Skill_Combat_Mod.Value;
            PokemonData.Species_Skill_Data.Command_Mod = (int)Skill_Command_Mod.Value;
            PokemonData.Species_Skill_Data.Focus_Mod = (int)Skill_Focus_Mod.Value;
            PokemonData.Species_Skill_Data.General_Mod = (int)Skill_GeneralEDU_Mod.Value;
            PokemonData.Species_Skill_Data.Guile_Mod = (int)Skill_Gulie_Mod.Value;
            PokemonData.Species_Skill_Data.Intimidate_Mod = (int)Skill_Intimidate_Mod.Value;
            PokemonData.Species_Skill_Data.Intuition_Mod = (int)Skill_Intuition_Mod.Value;
            PokemonData.Species_Skill_Data.Medicine_Mod = (int)Skill_MedicineEDU_Mod.Value;
            PokemonData.Species_Skill_Data.Occult_Mod = (int)Skill_OccultEDU_Mod.Value;
            PokemonData.Species_Skill_Data.Perception_Mod = (int)Skill_Perception_Mod.Value;
            PokemonData.Species_Skill_Data.Pokemon_Mod = (int)Skill_PokemonEDU_Mod.Value;
            PokemonData.Species_Skill_Data.Stealth_Mod = (int)Skill_Stealth_Mod.Value;
            PokemonData.Species_Skill_Data.Survival_Mod = (int)Skill_Survival_Mod.Value;
            PokemonData.Species_Skill_Data.Technology_Mod = (int)Skill_TechnologyEDU_Mod.Value;
            #endregion
            //Load Pokemon Capabilities Data
            #region Capabilities
            PokemonData.Species_Capability_Data.NatureWalk_1 = (BattleManager.Data.NatureWalk_Type)Capabilities_NatureWalk_1.SelectedItem;
            PokemonData.Species_Capability_Data.NatureWalk_2 = (BattleManager.Data.NatureWalk_Type)Capabilities_NatureWalk_2.SelectedItem;

            PokemonData.Species_Capability_Data.Burrow = (int)Capabilities_Burrow.Value;
            PokemonData.Species_Capability_Data.HighJump = (int)Capabilities_HighJump.Value;
            PokemonData.Species_Capability_Data.Levitate = (int)Capabilities_Levitate.Value;
            PokemonData.Species_Capability_Data.LongJump = (int)Capabilities_LongJump.Value;
            PokemonData.Species_Capability_Data.Overland = (int)Capabilities_Overland.Value;
            PokemonData.Species_Capability_Data.Power = (int)Capabilities_Power.Value;
            PokemonData.Species_Capability_Data.Sky = (int)Capabilities_Sky.Value;
            PokemonData.Species_Capability_Data.Swim = (int)Capabilities_Swim.Value;
            PokemonData.Species_Capability_Data.Teleport = (int)Capabilities_Teleport.Value;
            PokemonData.Species_Capability_Data.ThrowingRange = (int)Capabilities_ThrowingRange.Value;
            #endregion
            //Save Special Capabilities Data
            #region Special Capabilities
            PokemonData.Species_SpecialCapability.Clear();
            foreach (CheckBox box in Capabilities_Wrap.Children)
            {
                if (box.IsChecked == true)
                {
                    if (((VPTU.Pokedex.Pokemon.Pokemon_Capabilities)box.Tag).ToString().ToUpper().StartsWith("I_"))
                    {
                        //Value needs to be added (Value is an int)
                    }
                    if (((VPTU.Pokedex.Pokemon.Pokemon_Capabilities)box.Tag).ToString().ToUpper().StartsWith("S_"))
                    {
                        //Value needs to be added (Value is a string)
                    }

                    PokemonData.Species_SpecialCapability.Add(new KeyValuePair<VPTU.Pokedex.Pokemon.Pokemon_Capabilities, object>((VPTU.Pokedex.Pokemon.Pokemon_Capabilities)box.Tag, true));
                }
            }
            #endregion
            //Save Pokemon Base Stat Data
            #region Base Stats
            PokemonData.Species_BaseStats_HP = (int)BaseStats_HP.Value;
            PokemonData.Species_BaseStats_Attack = (int)BaseStats_Attack.Value;
            PokemonData.Species_BaseStats_Defence = (int)BaseStats_Defence.Value;
            PokemonData.Species_BaseStats_SpAttack = (int)BaseStats_SpAttack.Value;
            PokemonData.Species_BaseStats_SpDefence = (int)BaseStats_SpDefence.Value;
            PokemonData.Species_BaseStats_Speed = (int)BaseStats_Speed.Value;
            #endregion

            //All the linked objects get saved here
            #region Links
            //Save Linked Move Data
            #region Moves
            PokemonData.Moves.Clear();
            foreach (VPTU.Pokedex.Pokemon.Link_Moves link in Moves_List.Items)
            {
                PokemonData.Moves.Add(link);
            }
            #endregion
            //Save Linked Evolution Data
            #region Evo Forms
            PokemonData.Evolutions.Clear();
            foreach (EvoLinks link in FormsAndEvos_List.Items)
            {
                PokemonData.Evolutions.Add(link.LinkData);
            }
            #endregion
            #endregion
        }

        /// <summary>
        /// Validates settings and fixes some automatically fixable errors. This is always run before Saving Data.
        /// </summary>
        /// <returns>Validation Pass</returns>
        [Obsolete("Code is not complete, Contains Errors", true)]
        private bool ValidateSettings()
        {
            bool Pass = true;// The variable that defines if the settings pass validation

            try
            {
                #region Basic Info
                #region Name
                if (String.IsNullOrWhiteSpace(Basic_Name.Text))
                {
                    MessageBox.Show("Name is not valid!", "Name Error", MessageBoxButton.OK, MessageBoxImage.Error);// Name is Not Valid
                    Pass = false;
                }
                else if (Mgr.SaveData.PokedexData.Pokemon.FindAll(x => x.Species_Name.ToLower() == Basic_Name.Text.ToLower()).Count >= 1 && !Update)
                {
                    MessageBox.Show("Name taken by another Pokemon!", "Name Error", MessageBoxButton.OK, MessageBoxImage.Error);// Name is Taken
                    Pass = false;
                }
                #endregion
                #region Typeing
                // Mechanic Changed... No Validation Avaliable
                #endregion
                #region ID
                try
                {
                    Decimal ID = Convert.ToDecimal(Basic_ID.Text);

                    if (Mgr.SaveData.PokedexData.Pokemon.FindAll(x => x.Species_DexID == ID).Count >= 1)
                    {
                        if (!Update || (ID != PokemonData.Species_DexID && Update))
                        {
                            MessageBox.Show("Dex ID taken by another Pokemon!", "Dex Error", MessageBoxButton.OK, MessageBoxImage.Error);// Dex ID is Taken
                            Pass = false;
                        }
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Dex Number not a valid Decimal!", "Dex Error", MessageBoxButton.OK, MessageBoxImage.Error);// Dex ID is not Valid
                    Pass = false;
                }
                #endregion
                #region Size
                if (Basic_Size.SelectedIndex == -1)// Check if the Primary Type is set, if not set it will fail the validation check
                {
                    MessageBox.Show("You have not selected the pokemons size class!", "Basic Information Error -> Size Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Pass = false;
                }
                #endregion
                #region Weight
                if (Basic_Weight.SelectedIndex == -1)// Check if the Primary Type is set, if not set it will fail the validation check
                {
                    MessageBox.Show("You have not selected the pokemons weight class!", "Basic Information Error -> Weight Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Pass = false;
                }
                #endregion
                #endregion

                #region Breeding Info
                #region Gender Chances
                try
                {
                    decimal MC = Convert.ToDecimal(Breeding_MaleChance.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("You have not specified a valid Decimal for the Male Gender Chance!", "Breeding Information Error -> Gender Chance Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Pass = false;
                }
                #endregion

                #endregion
            }
            #region Other Errors
            catch (NullReferenceException)
            {
                MessageBox.Show("No Save File Selected! Unable to Add, Edit or Validate Pokemon Save Data", "Name Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Pass = false;
            }
            catch
            {
                MessageBox.Show("Unknown Validation Error", "Unknown Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Pass = false;
            }
            #endregion

            return Pass;// returns the validation pass state
        }

        /// <summary>
        /// When the Add / Update Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();

            DialogResult = true;
            Close();
        }

        /// <summary>
        /// When the Cancel Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("All unsaved work will be lost!\nDo you want to close?\n\nYes = Close\nNo = Clear all changes and continue to edit\nCancel = Keep Changes and Keep editing", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (mbr == MessageBoxResult.Yes)
            {
                DialogResult = false;
                Close();
                return;
            }
            else if (mbr == MessageBoxResult.No)
            {
                Load();
                DialogResult = null;
                return;
            }
            else if (mbr == MessageBoxResult.Cancel)
            {
                DialogResult = null;
                return;
            }
        }
        #endregion

        #region Form Components Change
        private void Breeding_MaleChance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Breeding_FemaleChance.Text = (100 - Breeding_MaleChance.Value).ToString();
        }
        #endregion

        #region Link Moves
        /// <summary>
        /// Add Link Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkMove_Button_Click(object sender, RoutedEventArgs e)
        {
            Move_Link link = new Move_Link();// Create a new MoveLink Window to create a link with
            bool? add = link.ShowDialog();// Shows the Link Window, Creates a Dialog to return true if it added successfully

            if (add == true)
            {
                Moves_List.Items.Add(link.LinkData);// Add MoveLink to list if not canceled
            }
        }
        /// <summary>
        /// Edit Link Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditLinkMove_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Moves_List.SelectedItem == null) { return; }// Returns if selection is null

            Move_Link link = new Move_Link((VPTU.Pokedex.Pokemon.Link_Moves)Moves_List.SelectedItem);// Creates a new MoveLink Window to modify link with
            link.ShowDialog();// Shows the Link Window
        }
        /// <summary>
        /// Remove Link Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveLinkMove_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Moves_List.SelectedItem == null) { return; }// Returns if selection is null
            Moves_List.Items.Remove(Moves_List.SelectedItem);// Removes Link from list
        }
        #endregion

        #region Link Evolution's and Forms
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkEvo_Button_Click(object sender, RoutedEventArgs e)
        {
            Link_Evolution link = new Link_Evolution();// Create a new EvoLink Window to create a link with
            bool? add = link.ShowDialog();// Shows the Link Window, Creates a Dialog to return true if it added successfully

            if (add == true)
            {
                string name = Mgr.SaveData.PokedexData.Pokemon.Find(x => x.Species_DexID == link.LinkData.Pokemon_Evo).Species_Name;
                FormsAndEvos_List.Items.Add(new EvoLinks(link.LinkData, name));// Add EvoLink to list if not canceled
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditLinkEvo_Button_Click(object sender, RoutedEventArgs e)
        {
            if (FormsAndEvos_List.SelectedItem == null) { return; }// Returns if selection is null

            Link_Evolution link = new Link_Evolution(((EvoLinks)FormsAndEvos_List.SelectedItem).LinkData);// Creates a new EvoLink Window to modify link with
            link.ShowDialog();// Shows the Link Window
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveLinkEvo_Button_Click(object sender, RoutedEventArgs e)
        {
            if (FormsAndEvos_List.SelectedItem == null) { return; }// Returns if selection is null
            FormsAndEvos_List.Items.Remove(FormsAndEvos_List.SelectedItem);// Removes Link from list
        }
        #endregion

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void RawData_Button_Click(object sender, RoutedEventArgs e)
        {
            RAW_JSON impexp = new RAW_JSON();
            impexp.Export<VPTU.Pokedex.Pokemon.PokemonData>(PokemonData);
            bool? dr = impexp.ShowDialog();

            if (dr == true)
            {
                PokemonData = impexp.Import<VPTU.Pokedex.Pokemon.PokemonData>();
                try { Load(); } catch { MessageBox.Show("Failed to load data object!"); }

                //Load(impexp.Import<VPTU.Pokedex.Pokemon.PokemonData>());
            }
        }

        private void SelectIMG_Normal_Click(object sender, RoutedEventArgs e)
        {
            UI.Resources.Search_Resources Select = new UI.Resources.Search_Resources(Mgr);

            bool? dr = Select.ShowDialog();

            if (dr == true)
            {
                //PokemonData.Sprite_Normal = Select;
            }
        }

        private void SelectIMG_Shiny_Click(object sender, RoutedEventArgs e)
        {
            UI.Resources.Search_Resources Select = new UI.Resources.Search_Resources(Mgr);

            bool? dr = Select.ShowDialog();

            if (dr == true)
            {
                //PokemonData.Sprite_Shiny = Select;
            }
        }

        private void SelectIMG_Egg_Click(object sender, RoutedEventArgs e)
        {
            UI.Resources.Search_Resources Select = new UI.Resources.Search_Resources(Mgr);

            bool? dr = Select.ShowDialog();

            if (dr == true)
            {
                //PokemonData.Sprite_Egg = Select;
            }
        }
    }

    #region Data Classes

    public class EvoLinks
    {
        public EvoLinks(VPTU.Pokedex.Pokemon.Link_Evolutions _LinkData = null, string _PokemonName = null)
        {
            LinkData = _LinkData;
            PokemonName = _PokemonName;
        }

        public VPTU.Pokedex.Pokemon.Link_Evolutions LinkData { get; set; }
        public string PokemonName { get; set; }
    }

    #endregion

}