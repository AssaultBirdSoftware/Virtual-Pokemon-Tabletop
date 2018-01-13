using System.Windows;
using System.Windows.Controls;
using AssaultBird2454.VPTU.BattleManager.BattleEffect.Data;
using AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions.UI;

namespace AssaultBird2454.VPTU.SaveEditor.UI.BattleEffect
{
    /// <summary>
    ///     Interaction logic for EffectAction_Designer.xaml
    /// </summary>
    public partial class EffectAction_Designer : Window
    {
        private readonly Action ActionData;
        private readonly Control EffectDesign;

        public EffectAction_Designer(Action Action)
        {
            InitializeComponent();
            ActionData = Action; // Sets the data variable

            #region Configures designer

            if (ActionData.Action_Command.ToLower().Equals("vptu.effects.status.add"))
            {
                EffectDesign = new StatusEffect_Add(); // Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else if (ActionData.Action_Command.ToLower().Equals("vptu.effects.status.remove"))
            {
                EffectDesign = new StatusEffect_Remove(); // Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else if (ActionData.Action_Command.ToLower().Equals("vptu.effects.move.db"))
            {
                EffectDesign = new SetDB(); // Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else if (ActionData.Action_Command.ToLower().Equals("vptu.effects.move.ac"))
            {
                EffectDesign = new SetAC(); // Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else if (ActionData.Action_Command.ToLower().Equals("vptu.condition.hasstatus"))
            {
                EffectDesign = new HasStatus(); // Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else if (ActionData.Action_Command.ToLower().Equals("vptu.action.statichealthchange"))
            {
                EffectDesign = new Static_HealthChange(); // Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else
            {
                MessageBox.Show("Unknown effect commend...\nClose the Action Designer and check the data...");
                //Close();
            }

            #endregion

            #region Sizing

            if (EffectDesign.Height > Height)
                Height = EffectDesign.Height;
            if (EffectDesign.Width > Width)
                Width = EffectDesign.Width;

            #endregion

            Load();
        }

        /// <summary>
        ///     Loads the designer and sub-designer
        /// </summary>
        public void Load()
        {
            var design = (BattleManager.BattleEffect.Data.Actions.UI.EffectAction_Designer) EffectDesign;
            design.Load(ActionData.Action_Data); // Loads the syb-designer

            Action_Name.Text = ActionData.Action_Name; // Loads the name
            Action_Comment.Text = ActionData.Action_Comment; // Loads the comment
        }

        /// <summary>
        ///     Saves the designer and sub-designer
        /// </summary>
        public void Save()
        {
            var design = (BattleManager.BattleEffect.Data.Actions.UI.EffectAction_Designer) EffectDesign;
            design.Save(ActionData.Action_Data); // Saves the sub-designer

            ActionData.Action_Name = Action_Name.Text; // Saves the Name
            ActionData.Action_Comment = Action_Comment.Text; // Saves the comment
        }

        private void SaveClose_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}