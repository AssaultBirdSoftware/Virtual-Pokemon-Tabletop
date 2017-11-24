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

            Effect.ItemsSource = Enum.GetValues(typeof(VPTU.BattleManager.Data.Status_Afflictions));

            //FunctionName.Items.Add();
        }

        public void Load(object Data)
        {
            Actions.HasStatus HasStatusData = (Actions.HasStatus)Data;

            HasEffect.IsChecked = HasStatusData.Effected_State;
            Effect.SelectedItem = HasStatusData.Status;
        }

        public void Save(object Data)
        {
            Actions.HasStatus HasStatusData = (Actions.HasStatus)Data;

            HasStatusData.Effected_State = (bool)HasEffect.IsChecked;
            HasStatusData.Status = (VPTU.BattleManager.Data.Status_Afflictions)(Effect.SelectedItem);
        }
    }
}
