using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AssaultBird2454.VPTU.BattleManager.BattleEffect;
using AssaultBird2454.VPTU.BattleManager.BattleEffect.Data;
using AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions;

namespace AssaultBird2454.VPTU.SaveEditor.UI.BattleEffect
{
    /// <summary>
    ///     Interaction logic for EffectFunction_Designer.xaml
    /// </summary>
    public partial class EffectFunction_Designer : Window
    {
        private readonly EffectFunction FunctionData;

        public EffectFunction_Designer(EffectFunction _FunctionData = null)
        {
            InitializeComponent();

            #region Function Data

            if (_FunctionData == null)
            {
                FunctionData = new EffectFunction(); // Creates a new Function Data Class
            }
            else
            {
                FunctionData = _FunctionData; // Sets the data
                Load(); // Loads the data
            }

            #endregion
        }

        private void Add_Action_Click(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxItem) Add_Action_Selector.SelectedItem).Name.ToLower().Equals("statuseffect_add"))
            {
                dynamic data = new AddStatusEffect();
                var Action = new Action("Add Status Effect", "VPTU.Effects.Status.Add", "Adds a status condition",
                    data);

                Actions_Display.Items.Add(Action);
            }
            else if (((ComboBoxItem) Add_Action_Selector.SelectedItem).Name.ToLower().Equals("statuseffect_remove"))
            {
                dynamic data = new RemoveStatusEffect();
                var Action = new Action("Remove Status Effect", "VPTU.Effects.Status.Remove",
                    "Removes a status condition", data);

                Actions_Display.Items.Add(Action);
            }
            else if (((ComboBoxItem) Add_Action_Selector.SelectedItem).Name.ToLower().Equals("target_push"))
            {
            }
            else if (((ComboBoxItem) Add_Action_Selector.SelectedItem).Name.ToLower().Equals("damage_db"))
            {
                dynamic data = new SetDB();
                var Action = new Action("Set DamageBase Value", "VPTU.Effects.Move.DB",
                    "Sets the moves DamageBase value for that action", data);

                Actions_Display.Items.Add(Action);
            }
            else if (((ComboBoxItem) Add_Action_Selector.SelectedItem).Name.ToLower().Equals("damage_roll"))
            {
            }
            else if (((ComboBoxItem) Add_Action_Selector.SelectedItem).Name.ToLower().Equals("condition_status"))
            {
                dynamic data = new HasStatus();
                var Action = new Action("Status Effect Check", "VPTU.Condition.HasStatus",
                    "Checks if the user or the targets have any or all of the specified status conditions, and invokes a function if it does",
                    data);

                Actions_Display.Items.Add(Action);
            }
            else if (((ComboBoxItem) Add_Action_Selector.SelectedItem).Name.ToLower().Equals("static_health_change"))
            {
                dynamic data = new HasStatus();
                var Action = new Action("Static Health Change", "VPTU.Action.StaticHealthChange",
                    "Changes the health of the target", data);

                Actions_Display.Items.Add(Action);
            }
            else if (((ComboBoxItem) Add_Action_Selector.SelectedItem).Name.ToLower().Equals("invoke_executefunction"))
            {
            }

            /*if (((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString().ToLower().StartsWith("has"))
            {
                EffectFunction_ActionItem item = new EffectFunction_ActionItem();
                item.Name = ((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString();
                item.Type = "Condition";
                item.Comment = "Comment Here";

                Actions_Display.Items.Add(item);
            }
            else if (((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString().ToLower().StartsWith("execute"))
            {
                EffectFunction_ActionItem item = new EffectFunction_ActionItem();
                item.Name = ((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString();
                item.Type = "Invoke";
                item.Comment = "Comment Here";

                Actions_Display.Items.Add(item);
            }
            else
            {
                EffectFunction_ActionItem item = new EffectFunction_ActionItem();
                item.Name = ((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString();
                item.Type = "Action";
                item.Comment = "Comment Here";

                Actions_Display.Items.Add(item);
            }
            */
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Save(); // Save the data
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Sets the dialog resault
            Close(); // Closes the window
        }

        private void Actions_Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Actions_Display.SelectedItem == null)
            {
                Move_Action_Up.IsEnabled = false; // No Item Selected Dissable Moving
                Move_Action_Down.IsEnabled = false; // No Item Selected Dissable Moving
                Edit_Action.IsEnabled = false; // No Item Selected Dissable Edit Button
                Delete_Action.IsEnabled = false; // No Item Selected Dissable Delete Button
            }
            else
            {
                Move_Action_Up.IsEnabled = true; // Item Selected Enable Moving
                Move_Action_Down.IsEnabled = true; // Item Selected Enable Moving
                Edit_Action.IsEnabled = true; // Item Selected Enable Edit Button
                Delete_Action.IsEnabled = true; //Item Selected Enable Delete Button
            }
        }

        private void Edit_Action_Click(object sender, RoutedEventArgs e)
        {
            var des = new EffectAction_Designer((Action) Actions_Display.SelectedItems[0]);
            var saved = des.ShowDialog();

            if (saved == true)
                Actions_Display.Items.Refresh();
        }

        private void Move_Action_Up_Click(object sender, RoutedEventArgs e)
        {
            var act = (Action) Actions_Display.SelectedItem; // Gets the data
            var pos = Actions_Display.SelectedIndex; // Gets and changes index

            try
            {
                Actions_Display.Items.Remove(act); // Removes object from list
                Actions_Display.Items.Insert(pos - 1, act); // Inserts into new position

                Actions_Display.SelectedItem = act; // Sets the Selection
            }
            catch
            {
                Actions_Display.Items.Insert(pos, act); // Inserts into new position
                /* Dont Care */
            }
        }

        private void Move_Action_Down_Click(object sender, RoutedEventArgs e)
        {
            var act = (Action) Actions_Display.SelectedItem; // Gets the data
            var pos = Actions_Display.SelectedIndex; // Gets and changes index

            try
            {
                Actions_Display.Items.Remove(act); // Removes object from list
                Actions_Display.Items.Insert(pos + 1, act); // Inserts into new position

                Actions_Display.SelectedItem = act; // Sets the Selection
            }
            catch
            {
                Actions_Display.Items.Insert(pos, act);
                /* Dont Care */
            }
        }

        private void Delete_Action_Click(object sender, RoutedEventArgs e)
        {
            Actions_Display.Items.Remove(Actions_Display.SelectedItem); // Removes object from list
        }

        #region Base Functions

        /// <summary>
        ///     Saves the data
        /// </summary>
        public void Save()
        {
            FunctionData.Function_Name = Function_Name.Text; // Saves the Function Name
            FunctionData.Function_Comment = Function_Description.Text; // Saves the Function Description

            #region Actions

            if (FunctionData.Actions == null) FunctionData.Actions = new List<Action>();
            FunctionData.Actions.Clear(); // Clears the Function's Actions

            foreach (Action obj in Actions_Display.Items)
                FunctionData.Actions.Add(obj);

            #endregion

            #region Triggers

            if (FunctionData.Triggers == null) FunctionData.Triggers = new List<Triggers>();
            FunctionData.Triggers.Clear(); // Clears the Function's Triggers

            #endregion
        }

        /// <summary>
        ///     Loads the data
        /// </summary>
        public void Load()
        {
            Function_Name.Text = FunctionData.Function_Name; // Loads the Function Name
            Function_Description.Text = FunctionData.Function_Comment; // Loads the Function Description

            #region Actions

            if (FunctionData.Actions == null) FunctionData.Actions = new List<Action>();
            Actions_Display.Items.Clear(); // Clears the Function's Actions Display

            foreach (var obj in FunctionData.Actions)
                Actions_Display.Items.Add(obj);

            #endregion

            #region Triggers

            if (FunctionData.Triggers == null) FunctionData.Triggers = new List<Triggers>();
            //FunctionData.Triggers.Clear();// Clears the Function's Triggers Display

            #endregion
        }

        #endregion
    }
}