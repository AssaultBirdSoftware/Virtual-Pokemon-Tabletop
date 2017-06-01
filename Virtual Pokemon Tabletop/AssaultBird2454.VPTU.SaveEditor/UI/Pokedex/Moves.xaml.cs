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
using Xceed.Wpf.Toolkit;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex
{
    /// <summary>
    /// Interaction logic for Moves.xaml
    /// </summary>
    public partial class Moves : Window
    {
        public VPTU.Pokedex.Moves.MoveData MoveData;
        bool Update = false;
        private SaveManager.Data.SaveFile.PTUSaveData SaveData;

        public Moves(SaveManager.Data.SaveFile.PTUSaveData _SaveData, VPTU.Pokedex.Moves.MoveData _MoveData = null)
        {
            InitializeComponent();// Creates the Window

            SaveData = _SaveData;

            Setup();// Executes the setup script

            if (_MoveData == null)
            {
                MoveData = new VPTU.Pokedex.Moves.MoveData();// If no specified data, then create a new data class
            }
            else
            {
                Update = true;
                MoveData = _MoveData;// Save the specified data class to a variable for latter
                Load();// Load the data
            }
        }

        /// <summary>
        /// Set's up the form
        /// </summary>
        public void Setup()
        {

            #region Data Binding
            Battle_ActionType.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.Action_Type));
            Battle_Class.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.MoveClass));
            Battle_Frequency.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.Move_Frequency));
            Battle_Type.ItemsSource = Enum.GetValues(typeof(BattleManager.Data.Type));

            Contest_Effect.ItemsSource = Enum.GetValues(typeof(ContestManager.Data.Contest_Effects));
            Contest_Type.ItemsSource = Enum.GetValues(typeof(ContestManager.Data.Contest_Type));
            #endregion
            #region Data Defaulting
            Battle_ActionType.SelectedIndex = 0;
            Battle_Class.SelectedIndex = 0;
            Battle_Frequency.SelectedIndex = 0;
            Battle_Type.SelectedIndex = 0;

            Contest_Effect.SelectedIndex = 0;
            Contest_Type.SelectedIndex = 0;
            #endregion
        }

        /// <summary>
        /// Loads the data from the MoveData Object
        /// </summary>
        public void Load()
        {
            #region Basic Info
            Basic_Name.Text = MoveData.Name;
            Basic_Desc.Text = MoveData.Description;
            #endregion

            #region Battle Details
            Battle_ActionType.SelectedItem = MoveData.Move_ActionType;
            Battle_Class.SelectedItem = MoveData.Move_Class;
            Battle_Frequency.SelectedItem = MoveData.Move_Frequency;
            Battle_Type.SelectedItem = MoveData.Move_Type;

            Battle_AC.Value = MoveData.Move_Accuracy;
            Battle_DB.Value = (int)MoveData.Move_DamageBase;
            Battle_UseLimit.Value = MoveData.Move_Frequency_Limit;
            #endregion

            #region Contest Details
            Contest_Effect.SelectedItem = MoveData.Contest_Effect;
            Contest_Type.SelectedItem = MoveData.Contest_Type;
            #endregion

            #region Range Data
            if (MoveData.Range_Data == null) { MoveData.Range_Data = new List<VPTU.Pokedex.Moves.Move_RangeData>(); }
            #region All Adjacent Foes
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.All_Adjacent_Foes);
                AllAdj_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Any Target
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Any);
                AnyTarget_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Burst
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Burst);
                Burst_Enabled.IsChecked = Data.Enabled;
                Burst_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Cardinally Adjacent
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Cardinally_Adjacent);
                CardinallyAdj_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Close Blast
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Close_Blast);
                CloseBlast_Enabled.IsChecked = Data.Enabled;
                CloseBlast_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Cone
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Cone);
                Cone_Enabled.IsChecked = Data.Enabled;
                Cone_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Field
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Field);
                Field_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Line
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Line);
                Line_Enabled.IsChecked = Data.Enabled;
                Line_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Mele
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Melee);
                Mele_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #region Range
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Range);
                Range_Enabled.IsChecked = Data.Enabled;
                Range_Distance.Value = Data.Distance;
            }
            catch { }
            #endregion
            #region Ranged Blast
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Range_Blast);
                RangedBlast_Enabled.IsChecked = Data.Enabled;
                RangedBlast_Distance.Value = Data.Distance;
                RangedBlast_Size.Value = Data.Size;
            }
            catch { }
            #endregion
            #region Self
            try
            {
                VPTU.Pokedex.Moves.Move_RangeData Data = MoveData.Range_Data.Find(x => x.Range == BattleManager.Data.Move_Range.Self);
                Self_Enabled.IsChecked = Data.Enabled;
            }
            catch { }
            #endregion
            #endregion
        }

        /// <summary>
        /// Saves the data to the MoveData Object
        /// </summary>
        public void Save()
        {
            string OldName = MoveData.Name;// Gets the current name before saving for updateing later.

            #region Basic Info
            MoveData.Name = Basic_Name.Text;
            MoveData.Description = Basic_Desc.Text;
            #endregion

            #region Battle Details
            MoveData.Move_ActionType = (BattleManager.Data.Action_Type)Battle_ActionType.SelectedItem;
            MoveData.Move_Class = (BattleManager.Data.MoveClass)Battle_Class.SelectedItem;
            MoveData.Move_Frequency = (BattleManager.Data.Move_Frequency)Battle_Frequency.SelectedItem;
            MoveData.Move_Type = (BattleManager.Data.Type)Battle_Type.SelectedItem;

            MoveData.Move_Accuracy = (int)Battle_AC.Value;
            MoveData.Move_DamageBase = (BattleManager.Data.DamageBase)Battle_DB.Value;
            MoveData.Move_Frequency_Limit = (int)Battle_UseLimit.Value;
            #endregion

            #region Contest Details
            MoveData.Contest_Effect = (ContestManager.Data.Contest_Effects)Contest_Effect.SelectedItem;
            MoveData.Contest_Type = (ContestManager.Data.Contest_Type)Contest_Type.SelectedItem;
            #endregion

            #region Range Data
            MoveData.Range_Data = new List<VPTU.Pokedex.Moves.Move_RangeData>();

            #region All Adjacent Foes
            try
            {
                if ((bool)AllAdj_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.All_Adjacent_Foes, (bool)AllAdj_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Any Target
            try
            {
                if ((bool)AnyTarget_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Any, (bool)AnyTarget_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Burst
            try
            {
                if ((bool)Burst_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Burst, (bool)Burst_Enabled.IsChecked, 0, Convert.ToInt32(Decimal.Floor((decimal)Burst_Size.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Cardinally Adjacent
            try
            {
                if ((bool)CardinallyAdj_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Cardinally_Adjacent, (bool)CardinallyAdj_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Close Blast
            try
            {
                if ((bool)CloseBlast_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Close_Blast, (bool)CloseBlast_Enabled.IsChecked, 0, Convert.ToInt32(Decimal.Floor((decimal)CloseBlast_Size.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Cone
            try
            {
                if ((bool)Cone_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Cone, (bool)Cone_Enabled.IsChecked, 0, Convert.ToInt32(Decimal.Floor((decimal)Cone_Size.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Field
            try
            {
                if ((bool)Field_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Field, (bool)Field_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Line
            try
            {
                if ((bool)Line_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Line, (bool)Line_Enabled.IsChecked, Convert.ToInt32(Decimal.Floor((decimal)Line_Size.Value)), 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Mele
            try
            {
                if ((bool)Mele_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Melee, (bool)Mele_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Range
            try
            {
                if ((bool)Range_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Range, (bool)Range_Enabled.IsChecked, 0, Convert.ToInt32(Decimal.Floor((decimal)Range_Distance.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Ranged Blast
            try
            {
                if ((bool)RangedBlast_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Range_Blast, (bool)RangedBlast_Enabled.IsChecked, Convert.ToInt32(Decimal.Floor((decimal)RangedBlast_Size.Value)), Convert.ToInt32(Decimal.Floor((decimal)RangedBlast_Distance.Value)));
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #region Self
            try
            {
                if ((bool)Self_Enabled.IsChecked)
                {
                    VPTU.Pokedex.Moves.Move_RangeData Data = new VPTU.Pokedex.Moves.Move_RangeData(BattleManager.Data.Move_Range.Self, (bool)Self_Enabled.IsChecked, 0, 0);
                    MoveData.Range_Data.Add(Data);
                }
            }
            catch { }
            #endregion
            #endregion

            //Update links in different parts of the save data
            #region Update
            try
            {
                if (Update)
                {
                    if (MoveData.Name != OldName)
                    {
                        foreach (VPTU.Pokedex.Pokemon.PokemonData pokemon in SaveData.PokedexData.Pokemon)
                        {
                            if(pokemon.Moves == null)
                            {
                                pokemon.Moves = new List<VPTU.Pokedex.Pokemon.Link_Moves>();
                                continue;
                            }

                            List<VPTU.Pokedex.Pokemon.Link_Moves> moves = pokemon.Moves.FindAll(x => x.MoveName.ToLower() == OldName.ToLower());
                            if (moves != null)
                            {
                                foreach (VPTU.Pokedex.Pokemon.Link_Moves move in moves)
                                {
                                    move.MoveName = MoveData.Name;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error while updating Move Links");
                System.Windows.MessageBox.Show(ex.ToString());
            }
            #endregion
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();// Saves Data

            try { DialogResult = true; } catch { }// Sets the resault to true (Means Pass) & the error means it is not a dialogue
            Close();// Closes the dialogue
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            try { DialogResult = false; } catch { }// Sets the resault to true (Means Fail / Canceled) & the error means it is not a dialogue
            Close();// Closes the dialogue
        }
    }
}