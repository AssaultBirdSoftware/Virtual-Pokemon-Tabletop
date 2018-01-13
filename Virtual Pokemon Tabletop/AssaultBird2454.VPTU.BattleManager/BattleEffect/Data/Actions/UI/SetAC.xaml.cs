using System.Windows.Controls;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions.UI
{
    /// <summary>
    ///     Interaction logic for SetAC.xaml
    /// </summary>
    public partial class SetAC : UserControl, EffectAction_Designer
    {
        public SetAC()
        {
            InitializeComponent();
        }

        public void Load(object Data)
        {
            var EffectData = (Actions.SetAC) Data;

            AC_Value.Value = EffectData.AC;
        }

        public void Save(object Data)
        {
            var EffectData = (Actions.SetAC) Data;

            EffectData.AC = (int) AC_Value.Value;
        }
    }
}