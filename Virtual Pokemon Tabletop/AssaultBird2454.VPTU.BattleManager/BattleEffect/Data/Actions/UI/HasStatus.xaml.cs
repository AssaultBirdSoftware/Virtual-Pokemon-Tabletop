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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions.UI
{
    /// <summary>
    /// Interaction logic for HasStatus.xaml
    /// </summary>
    public partial class HasStatus : UserControl, EffectAction_Designer
    {
        public HasStatus()
        {
            InitializeComponent();

            Dictionary<string, object> itemSource = new Dictionary<string, object>();

            foreach(VPTU.BattleManager.Data.Status_Afflictions effect in Enum.GetValues(typeof(VPTU.BattleManager.Data.Status_Afflictions)))
            {
                itemSource.Add(effect.ToString(), effect);
            }

            Effects.ItemsSource = itemSource;
            
            //FunctionName.Items.Add();
        }

        public void Load(object Data)
        {
            Actions.HasStatus HasStatusData = (Actions.HasStatus)Data;

            HasAll.IsChecked = HasStatusData.HasAll;
            FunctionName.Text = HasStatusData.FunctionName;

            Dictionary<string, object> selecteditems = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> effect in Effects.ItemsSource)
            {
                if (HasStatusData.CheckedStatus.Contains((VPTU.BattleManager.Data.Status_Afflictions)effect.Value))
                {
                    selecteditems.Add(effect.Key, effect.Value);
                }
            }
            Effects.SelectedItems = selecteditems;
        }

        public void Save(object Data)
        {
            Actions.HasStatus HasStatusData = (Actions.HasStatus)Data;

            HasStatusData.HasAll = (bool)HasAll.IsChecked;
            HasStatusData.FunctionName = FunctionName.Text;

            List<VPTU.BattleManager.Data.Status_Afflictions> status = new List<VPTU.BattleManager.Data.Status_Afflictions>();
            foreach(KeyValuePair<string, object> sel in Effects.SelectedItems)
            {
                status.Add((VPTU.BattleManager.Data.Status_Afflictions)sel.Value);
            }
            HasStatusData.CheckedStatus = status;
        }
    }
}
