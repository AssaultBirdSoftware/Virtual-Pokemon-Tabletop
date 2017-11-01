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
    /// Interaction logic for Static_DMG.xaml
    /// </summary>
    public partial class Static_HealthChange : UserControl, EffectAction_Designer
    {
        public Static_HealthChange()
        {
            InitializeComponent();
        }

        public void Load(object Data)
        {
            Actions.Static_HealthChange HCData = (Actions.Static_HealthChange)Data;

            Damage.IsChecked = HCData.Damage;
            Percentage.IsChecked = HCData.Percentage;
            Value.Value = HCData.Value;
        }

        public void Save(object Data)
        {
            Actions.Static_HealthChange HCData = (Actions.Static_HealthChange)Data;

            HCData.Damage = (bool)Damage.IsChecked;
            HCData.Percentage = (bool)Percentage.IsChecked;
            HCData.Value = (int)Value.Value;
        }
    }
}
