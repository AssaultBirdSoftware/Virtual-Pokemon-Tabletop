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

namespace AssaultBird2454.VPTU.SaveEditor.UI.BattleEffect
{
    /// <summary>
    /// Interaction logic for EffectFunction_Designer.xaml
    /// </summary>
    public partial class EffectFunction_Designer : Window
    {
        private BattleManager.BattleEffect.EffectFunction FunctionData;

        public EffectFunction_Designer(BattleManager.BattleEffect.EffectFunction _FunctionData = null)
        {
            InitializeComponent();

            #region Function Data
            if (_FunctionData == null)
            {
                FunctionData = new BattleManager.BattleEffect.EffectFunction();// Creates a new Function Data Class
            }
            else
            {
                FunctionData = _FunctionData;// Sets the data
                //Load();// Loads the data
            }
            #endregion
        }

        #region Base Functions
        /// <summary>
        /// Saves the data
        /// </summary>
        public void Save()
        {
            FunctionData.Function_Name = Function_Name.Text;// Saves the Function Name
            FunctionData.Function_Comment = Function_Description.Text;// Saves the Function Description

            #region Actions
            if (FunctionData.Actions == null) { FunctionData.Actions = new List<object>(); }// Checks if the Actions List is null and creates a new list
            FunctionData.Actions.Clear();// Clears the Function's Actions
            #endregion
            #region Triggers
            if (FunctionData.Triggers == null) { FunctionData.Triggers = new List<BattleManager.BattleEffect.Data.Triggers>(); }// Checks if the Triggers List is null and creates a new list
            FunctionData.Triggers.Clear();// Clears the Function's Triggers
            #endregion
        }

        /// <summary>
        /// Loads the data
        /// </summary>
        public void Load()
        {
            FunctionData.Function_Name = Function_Name.Text;// Saves the Function Name
            FunctionData.Function_Comment = Function_Description.Text;// Saves the Function Description

            #region Actions
            if (FunctionData.Actions == null) { FunctionData.Actions = new List<object>(); }// Checks if the Actions List is null and creates a new list
            Actions_Display.Items.Clear();// Clears the Function's Actions Display
            #endregion
            #region Triggers
            if (FunctionData.Triggers == null) { FunctionData.Triggers = new List<BattleManager.BattleEffect.Data.Triggers>(); }// Checks if the Triggers List is null and creates a new list
            //FunctionData.Triggers.Clear();// Clears the Function's Triggers Display
            #endregion
        }
        #endregion

        private void Add_Action_Click(object sender, RoutedEventArgs e)
        {
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
            Save();// Save the data
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;// Sets the dialog resault
            Close();// Closes the window
        }

        private void Actions_Display_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Actions_Display.SelectedItem == null)
            {
                Move_Action_Up.IsEnabled = false;// No Item Selected Dissable Moving
                Move_Action_Down.IsEnabled = false;// No Item Selected Dissable Moving
            }
            else
            {
                Move_Action_Up.IsEnabled = true;// Item Selected Enable Moving
                Move_Action_Down.IsEnabled = true;// Item Selected Enable Moving
            }
        }
    }
}
