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
    /// Interaction logic for EffectAction_Designer.xaml
    /// </summary>
    public partial class EffectAction_Designer : Window
    {
        BattleManager.BattleEffect.Data.Action ActionData;
        Control EffectDesign;

        public EffectAction_Designer(BattleManager.BattleEffect.Data.Action Action)
        {
            InitializeComponent();
            ActionData = Action;// Sets the data variable

            #region Configures designer
            if (ActionData.Action_Command.ToLower().Equals("vptu.effects.status.add"))
            {
                EffectDesign = new BattleManager.BattleEffect.Data.Actions.UI.StatusEffect_Add();// Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else if (ActionData.Action_Command.ToLower().Equals("vptu.effects.status.remove"))
            {
                EffectDesign = new BattleManager.BattleEffect.Data.Actions.UI.StatusEffect_Remove();// Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else if (ActionData.Action_Command.ToLower().Equals("vptu.effects.move.db"))
            {
                EffectDesign = new BattleManager.BattleEffect.Data.Actions.UI.SetDB();// Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else if (ActionData.Action_Command.ToLower().Equals("vptu.effects.move.ac"))
            {
                EffectDesign = new BattleManager.BattleEffect.Data.Actions.UI.SetAC();// Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }else if (ActionData.Action_Command.ToLower().Equals("VPTU.Condition.HasStatus"))
            {
                EffectDesign = new BattleManager.BattleEffect.Data.Actions.UI.HasStatus();// Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else
            {
                MessageBox.Show("Unknown effect commend...\nClosing Action Designer");
                Close();
                return;
            }
            #endregion

            #region Sizing
            if (EffectDesign.Height > this.Height)
            {
                this.Height = EffectDesign.Height;
            }
            if (EffectDesign.Width > this.Width)
            {
                this.Width = EffectDesign.Width;
            }
            #endregion

            Load();
        }

        /// <summary>
        /// Loads the designer and sub-designer
        /// </summary>
        public void Load()
        {
            BattleManager.BattleEffect.Data.Actions.UI.EffectAction_Designer design = (BattleManager.BattleEffect.Data.Actions.UI.EffectAction_Designer)EffectDesign;
            design.Load(ActionData.Action_Data);// Loads the syb-designer

            Action_Name.Text = ActionData.Action_Name;// Loads the name
            Action_Comment.Text = ActionData.Action_Comment;// Loads the comment
        }
        /// <summary>
        /// Saves the designer and sub-designer
        /// </summary>
        public void Save()
        {
            BattleManager.BattleEffect.Data.Actions.UI.EffectAction_Designer design = (BattleManager.BattleEffect.Data.Actions.UI.EffectAction_Designer)EffectDesign;
            design.Save(ActionData.Action_Data);// Saves the sub-designer

            ActionData.Action_Name = Action_Name.Text;// Saves the Name
            ActionData.Action_Comment = Action_Comment.Text;// Saves the comment
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