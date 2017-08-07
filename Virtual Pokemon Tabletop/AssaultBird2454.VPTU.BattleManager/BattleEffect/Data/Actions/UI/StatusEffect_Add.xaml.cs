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
    /// Interaction logic for StatusEffect_Add.xaml
    /// </summary>
    public partial class StatusEffect_Add : UserControl, EffectAction_Designer
    {
        public StatusEffect_Add()
        {
            InitializeComponent();

            Status_Effects.ItemsSource = Enum.GetValues(typeof(VPTU.BattleManager.Data.Status_Afflictions));
        }

        public void Load(dynamic Data)
        {
            AddStatusEffect EffectData = (AddStatusEffect)Data;

            try { Status_Effects.SelectedItem = EffectData.StatusEffect; } catch { }
            //try { Expiry_Method.SelectedItem = EffectData.StatusExpiry_Method; } catch{ }
            try { Expiry_Turns.Value = EffectData.StatusExpiry_Time; } catch { }
            try { Effect_Forced.IsChecked = EffectData.StatusEffect_Force; } catch { }
            try { Effect_Persistant.IsChecked = EffectData.StatusEffect_Persistant; } catch { }
        }

        public void Save(dynamic Data)
        {
            AddStatusEffect EffectData = (AddStatusEffect)Data;

            EffectData.StatusEffect = (VPTU.BattleManager.Data.Status_Afflictions)Status_Effects.SelectedItem;
            //EffectData.StatusExpiry_Method = (object)Expiry_Method.SelectedItem;
            EffectData.StatusExpiry_Time = (int)Expiry_Turns.Value;
            EffectData.StatusEffect_Force = (bool)Effect_Forced.IsChecked;
            EffectData.StatusEffect_Persistant = (bool)Effect_Persistant.IsChecked;
        }
    }
}
