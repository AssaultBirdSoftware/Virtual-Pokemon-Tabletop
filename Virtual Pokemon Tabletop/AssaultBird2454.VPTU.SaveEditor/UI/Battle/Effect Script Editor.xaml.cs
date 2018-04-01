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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Battle
{
    /// <summary>
    /// Interaction logic for Effect_Script_Editor.xaml
    /// </summary>
    public partial class Effect_Script_Editor : Window
    {
        SaveManager.SaveManager Mgr;
        string ID;

        public Effect_Script_Editor(SaveManager.SaveManager _mgr, string _ID)
        {
            Mgr = _mgr;
            ID = _ID;
            InitializeComponent();

            Load();
        }

        private void Load()
        {
            Script.Text = Mgr.LoadEffect_LuaScript(ID);
        }
        private void Save()
        {
            Mgr.SaveEffect_LuaScript(ID, Script.Text);
        }

        private void SaveExit_Click(object sender, RoutedEventArgs e)
        {
            Save();
            DialogResult = true;
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
