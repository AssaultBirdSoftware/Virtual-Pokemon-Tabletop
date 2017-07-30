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
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.SaveEditor.UI.BattleEffect
{
    /// <summary>
    /// Interaction logic for EffectFunction_Designer.xaml
    /// </summary>
    public partial class EffectFunction_Designer : Window
    {
        public EffectFunction_Designer()
        {
            InitializeComponent();
        }

        private void Add_Action_Click(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString().ToLower().StartsWith("has"))
            {
                EffectFunction_ActionItem item = new EffectFunction_ActionItem();
                item.Name = ((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString();
                item.Type = "Condition";
                item.Comment = "Comment Here";

                Actions_Display.Items.Add(item);
            }
            else if (((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString().ToLower().StartsWith("execute"))
            {
                EffectFunction_ActionItem item = new EffectFunction_ActionItem();
                item.Name = ((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString();
                item.Type = "Invoke";
                item.Comment = "Comment Here";

                Actions_Display.Items.Add(item);
            }
            else
            {
                EffectFunction_ActionItem item = new EffectFunction_ActionItem();
                item.Name = ((ComboBoxItem)Add_Action_Selector.SelectedItem).Content.ToString();
                item.Type = "Action";
                item.Comment = "Comment Here";

                Actions_Display.Items.Add(item);
            }
        }
    }
    public class EffectFunction_ActionItem
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
