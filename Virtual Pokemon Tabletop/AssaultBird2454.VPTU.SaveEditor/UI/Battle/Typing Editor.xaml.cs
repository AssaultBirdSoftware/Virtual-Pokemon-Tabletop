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

        public void Edit()
        {
            if (Type_List.SelectedItem != null)
            {
                BattleManager.Typing.Typing_Data Type = (BattleManager.Typing.Typing_Data)Type_List.SelectedItem;
                Typing_Details details = new Typing_Details(Type);
                details.ShowDialog();

                Load();
            }
            else
            {
                MessageBox.Show("You cant edit nothing...", "Why nothing?");
            }
        }

        private void Type_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Edit();
        }

        private void Type_Add_Click(object sender, RoutedEventArgs e)
        {
            BattleManager.Typing.Typing_Data Data = new BattleManager.Typing.Typing_Data(true);
            Typing_Details TD = new Typing_Details(Data);
            TD.ShowDialog();
            if (!String.IsNullOrWhiteSpace(Data.Type_Name))
            {
                foreach (BattleManager.Typing.Typing_Data type in MainWindow.SaveManager.SaveData.Typing_Manager.Types)
                {
                    type.Effect_Normal.Add(Data.Type_Name);
                }

                MainWindow.SaveManager.SaveData.Typing_Manager.Types.Add(Data);
                Load();
            }
        }

        private void Type_Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        private void Type_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Type_List.SelectedItem != null)
            {
                BattleManager.Typing.Typing_Data Data = (BattleManager.Typing.Typing_Data)Type_List.SelectedItem;

                MainWindow.SaveManager.SaveData.Typing_Manager.Types.Remove(Data);
                foreach (BattleManager.Typing.Typing_Data type in MainWindow.SaveManager.SaveData.Typing_Manager.Types)
                {
                    type.Effect_SuperEffective.RemoveAll(x => x == Data.Type_Name);
                    type.Effect_Normal.RemoveAll(x => x == Data.Type_Name);
                    type.Effect_NotVery.RemoveAll(x => x == Data.Type_Name);
                    type.Effect_Immune.RemoveAll(x => x == Data.Type_Name);
                }

                Load();
            }
            else
            {
                MessageBox.Show("You cant delete nothing...", "How do you delete nothing?");
            }
        }
    }
}
