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
    /// Interaction logic for SetAC.xaml
    /// </summary>
    public partial class SetAC : UserControl, EffectAction_Designer
    {
        public SetAC()
        {
            InitializeComponent();
        }

        public void Load(object Data)
        {
            Actions.SetAC EffectData = (Actions.SetAC)Data;

            AC_Value.Value = EffectData.AC;
        }

        public void Save(object Data)
        {
            Actions.SetAC EffectData = (Actions.SetAC)Data;

            EffectData.AC = (int)AC_Value.Value;
        }
    }
}
