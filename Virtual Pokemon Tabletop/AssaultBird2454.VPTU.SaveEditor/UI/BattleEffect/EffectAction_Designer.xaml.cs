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
        object ActionData;
        Control EffectDesign;

        public EffectAction_Designer(object Action)
        {
            InitializeComponent();
            ActionData = Action;// Sets the data variable

            #region Configures designer
            MessageBox.Show(ActionData.GetType().ToString());
            BattleManager.BattleEffect.Data.Action act = ActionData as BattleManager.BattleEffect.Data.Action;// Casts to an interface
            if (act.Action_Command.ToLower().Equals("vptu.effects.status.add"))
            {
                EffectDesign = new BattleManager.BattleEffect.Data.UI.StatusEffect_Add();// Loads the Correct Designer
                Effect_Display.Children.Add(EffectDesign);
            }
            else
            {
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
            BattleManager.BattleEffect.Data.UI.EffectAction_Designer design = (BattleManager.BattleEffect.Data.UI.EffectAction_Designer)EffectDesign;
            BattleManager.BattleEffect.Data.Action act = (BattleManager.BattleEffect.Data.Action)ActionData;// Casts to an interface
            design.Load(ActionData);// Loads the syb-designer

            Action_Name.Text = act.Action_Name;// Loads the name
            Action_Comment.Text = act.Comment;// Loads the comment
        }
        /// <summary>
        /// Saves the designer and sub-designer
        /// </summary>
        public void Save()
        {
            BattleManager.BattleEffect.Data.UI.EffectAction_Designer design = (BattleManager.BattleEffect.Data.UI.EffectAction_Designer)EffectDesign;
            BattleManager.BattleEffect.Data.Action act = (BattleManager.BattleEffect.Data.Action)ActionData;// Casts to an interface
            design.Save(ActionData);// Saves the sub-designer

            act.Action_Name = Action_Name.Text;// Saves the Name
            act.Comment = Action_Comment.Text;// Saves the comment
        }
    }
}
