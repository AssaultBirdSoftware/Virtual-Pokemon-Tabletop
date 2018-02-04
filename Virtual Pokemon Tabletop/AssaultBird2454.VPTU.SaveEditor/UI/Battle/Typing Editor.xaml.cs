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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Battle
{
    /// <summary>
    /// Interaction logic for Typing_Editor.xaml
    /// </summary>
    public partial class Typing_Editor : Window
    {
        public Typing_Editor()
        {
            InitializeComponent();

            Load();
        }

        public void Load()
        {
            if (MainWindow.SaveManager.SaveData.Typing_Manager.Types == null)
                return;

            Type_List.Items.Clear();
            foreach (BattleManager.Typing.Typing_Data data in MainWindow.SaveManager.SaveData.Typing_Manager.Types)
            {
                Type_List.Items.Add(data);
            }
        }

        private void Type_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Type_List.SelectedItem != null)
            {
                BattleManager.Typing.Typing_Data Type = (BattleManager.Typing.Typing_Data)Type_List.SelectedItem;
                Typing_Details details = new Typing_Details(Type);
                details.ShowDialog();

                Load();
            }
        }
    }
}
