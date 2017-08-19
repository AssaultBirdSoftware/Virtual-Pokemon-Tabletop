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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex.Link
{
    /// <summary>
    /// Interaction logic for Move_Link.xaml
    /// </summary>
    public partial class Move_Link : Window
    {
        public VPTU.Pokedex.Pokemon.Link_Moves LinkData;

        public Move_Link(VPTU.Pokedex.Pokemon.Link_Moves _LinkData = null)
        {
            InitializeComponent();

            if(_LinkData != null)
            {
                LinkData = _LinkData;
                Load();
            }
            else { LinkData = new VPTU.Pokedex.Pokemon.Link_Moves(); }
        }

        public void Save()
        {
            LinkData.MoveName = Move_Name.Text;

            LinkData.LevelUp_Move = (bool)LevelUp_Move.IsChecked;
            LinkData.Tutor_Move = (bool)Tutor_Move.IsChecked;
            LinkData.Egg_Move = (bool)Egg_Move.IsChecked;
            LinkData.TMHM_Move = (bool)TMHM_Move.IsChecked;

            LinkData.LevelUp_Level = (int)Level_Learned.Value;
        }
        public void Load()
        {
            Move_Name.Text = LinkData.MoveName;

            LevelUp_Move.IsChecked = LinkData.LevelUp_Move;
            Tutor_Move.IsChecked = LinkData.Tutor_Move;
            Egg_Move.IsChecked = LinkData.Egg_Move;
            TMHM_Move.IsChecked = LinkData.TMHM_Move;

            Level_Learned.Value = (decimal)LinkData.LevelUp_Level;
        }

        private void Link_Button_Click(object sender, RoutedEventArgs e)
        {
            Save();

            DialogResult = true;
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        #region Search For Move
        private void Move_Name_Select_Click(object sender, RoutedEventArgs e)
        {
            Select.Select_Move sm = new Select.Select_Move();
            bool? pass = sm.ShowDialog();

            if(pass == true && sm.Selected_Move != null)
            {
                Move_Name.Text = sm.Selected_Move.Name;
            }
        }
        #endregion
    }
}
