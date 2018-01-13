using System.Windows.Controls;

namespace AssaultBird2454.VPTU.BattleManager.BattleEffect.Data.Actions.UI
{
    /// <summary>
    ///     Interaction logic for SetDB.xaml
    /// </summary>
    public partial class SetDB : UserControl, EffectAction_Designer
    {
        public SetDB()
        {
            InitializeComponent();

            DB_Value.Value = 0;
        }

        public void Load(object Data)
        {
            var EffectData = (Actions.SetDB) Data;

            DB_Value.Value = EffectData.DB;
        }

        public void Save(object Data)
        {
            var EffectData = (Actions.SetDB) Data;

            EffectData.DB = (int) DB_Value.Value;
        }
    }
}