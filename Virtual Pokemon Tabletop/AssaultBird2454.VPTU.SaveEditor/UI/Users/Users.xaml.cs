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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Users
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Authentication_Manager.Data.User User;

        public Users(Authentication_Manager.Data.User _User = null)
        {
            InitializeComponent();

            if (_User == null)
            {
                User = new Authentication_Manager.Data.User(true);
            }
            else
            {
                User = _User;
            }

            Load();
        }

        public void Load()
        {
            Player_ID.Text = User.UserID;
            Player_Name.Text = User.Name;
            Player_ICN.Text = User.IC_Name;

            PlayerColor_Picker.SelectedColor = User.UserColor;

            // Groups
        }
        public void Save()
        {
            User.Name = Player_Name.Text;
            User.IC_Name = Player_ICN.Text;

            User.UserColor = (Color)PlayerColor_Picker.SelectedColor;

            // Groups
        }

        private void PlayerColor_Picker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (PlayerColor_Picker.SelectedColor != null) {
                User.UserColor = (Color)PlayerColor_Picker.SelectedColor;
            }
        }

        private void View_PlayerKey_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MainWindow.SaveManager.SaveData.Identitys.Find(x => x.UserID == User.UserID).Key, "Player Key");
        }

        private void Export_PlayerKey_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReGenerate_PlayerKey_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SaveManager.SaveData.Identitys.Find(x => x.UserID == User.UserID).ReGenerate_PlayerKey();
            MessageBox.Show(MainWindow.SaveManager.SaveData.Identitys.Find(x => x.UserID == User.UserID).Key, "New Player Key");
        }

        private void Player_ID_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void Player_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            User.Name = Player_Name.Text;
        }
        private void Player_ICN_TextChanged(object sender, TextChangedEventArgs e)
        {
            User.IC_Name = Player_ICN.Text;
        }
    }
}
