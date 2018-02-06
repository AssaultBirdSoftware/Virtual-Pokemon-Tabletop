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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Settings
{
    public enum Setting_Type { Bool, Button, Decimal, DropDown, String }
    /// <summary>
    /// Interaction logic for Settings_Panel.xaml
    /// </summary>
    public partial class Settings_Panel : UserControl
    {
        public Settings_Panel()
        {
            InitializeComponent();
        }

        public object Register_SettingNode(string node, Setting_Type type)
        {
            object control;

            switch (type)
            {
                case Setting_Type.Bool:
                    control = new Setting_Value_Bool();
                    break;
                case Setting_Type.Button:
                    control = new Setting_Value_Button();
                    break;
                case Setting_Type.Decimal:
                    control = new Setting_Value_Decimal();
                    break;
                case Setting_Type.DropDown:
                    control = new Setting_Value_DropDown();
                    break;
                case Setting_Type.String:
                    control = new Setting_Value_String();
                    break;
                default:
                    return null;
            }

            return control;
        }
    }
}
