using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex.Select
{
    /// <summary>
    /// Interaction logic for Select_Move.xaml
    /// </summary>
    public partial class Select_Move : Window
    {
        /// <summary>
        /// The Move Data that was selected
        /// </summary>
        public VPTU.Pokedex.Moves.MoveData Selected_Move;
        private SaveManager.SaveManager SaveManager;

        public Select_Move(SaveManager.SaveManager _SaveManager)
        {
            SaveManager = _SaveManager;

            InitializeComponent();

            ReloadList();
        }

        #region Searching Code
        Thread SearchThread;
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Stop the thread from searching
            try
            {
                SearchThread.Abort();
                SearchThread = null;
            }catch { }
            string Filter = textBox.Text;

            SearchThread = new Thread(new ThreadStart(new Action(() => ReloadList(Filter))));
            SearchThread.IsBackground = true;
            SearchThread.Start();
        }
        #endregion

        private void ReloadList(string SearchName = "")
        {
            this.Dispatcher.Invoke(new Action(() => Moves.Items.Clear()));

            foreach (VPTU.Pokedex.Moves.MoveData data in SaveManager.SaveData.PokedexData.Moves)
            {
                if (data.Name.ToLower().Contains(SearchName.ToLower()))
                {
                    this.Dispatcher.Invoke(new Action(() => Moves.Items.Add(data)));
                }
            }
        }

        private void Moves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selected_Move = (VPTU.Pokedex.Moves.MoveData)Moves.SelectedItem;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_Move == null)
            {
                DialogResult = false;
            }
            else
            {
                DialogResult = true;
            }

            Close();
        }
    }
}
