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
    /// Interaction logic for SetDB.xaml
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
            Actions.SetDB EffectData = (Actions.SetDB)Data;

            DB_Value.Value = EffectData.DB;
        }

        public void Save(object Data)
        {
            Actions.SetDB EffectData = (Actions.SetDB)Data;

            EffectData.DB = (int)DB_Value.Value;
        }
    }
}
