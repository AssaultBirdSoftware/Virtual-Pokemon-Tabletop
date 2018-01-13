using System;
using System.Windows.Controls;
using AssaultBird2454.VPTU.BattleManager.Data;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions.UI
{
    /// <summary>
    ///     Interaction logic for StatusEffect_Add.xaml
    /// </summary>
    public partial class StatusEffect_Remove : UserControl, EffectAction_Designer
    {
        public StatusEffect_Remove()
        {
            InitializeComponent();

            Status_Effects.ItemsSource = Enum.GetValues(typeof(Status_Afflictions));
        }

        public void Load(dynamic Data)
        {
            var EffectData = (RemoveStatusEffect) Data;

            Status_Effects.SelectedItem = EffectData.StatusEffect;
        }

        public void Save(dynamic Data)
        {
            var EffectData = (RemoveStatusEffect) Data;

            EffectData.StatusEffect = (Status_Afflictions) Status_Effects.SelectedItem;
        }
    }
}