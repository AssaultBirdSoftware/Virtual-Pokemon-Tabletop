using System;
using System.Windows.Controls;
using AssaultBird2454.VPTU.BattleManager.Data;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions.UI
{
    /// <summary>
    ///     Interaction logic for StatusEffect_Add.xaml
    /// </summary>
    public partial class StatusEffect_Add : UserControl, EffectAction_Designer
    {
        public StatusEffect_Add()
        {
            InitializeComponent();

            Status_Effects.ItemsSource = Enum.GetValues(typeof(Status_Afflictions));
        }

        public void Load(dynamic Data)
        {
            var EffectData = (AddStatusEffect) Data;

            try
            {
                Status_Effects.SelectedItem = EffectData.StatusEffect;
            }
            catch
            {
            }
            //try { Expiry_Method.SelectedItem = EffectData.StatusExpiry_Method; } catch{ }
            try
            {
                Expiry_Turns.Value = EffectData.StatusExpiry_Time;
            }
            catch
            {
            }
            try
            {
                Effect_Forced.IsChecked = EffectData.StatusEffect_Force;
            }
            catch
            {
            }
            try
            {
                Effect_Persistant.IsChecked = EffectData.StatusEffect_Persistant;
            }
            catch
            {
            }
        }

        public void Save(dynamic Data)
        {
            var EffectData = (AddStatusEffect) Data;

            EffectData.StatusEffect = (Status_Afflictions) Status_Effects.SelectedItem;
            //EffectData.StatusExpiry_Method = (object)Expiry_Method.SelectedItem;
            EffectData.StatusExpiry_Time = (int) Expiry_Turns.Value;
            EffectData.StatusEffect_Force = (bool) Effect_Forced.IsChecked;
            EffectData.StatusEffect_Persistant = (bool) Effect_Persistant.IsChecked;
        }
    }
}