using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using AssaultBird2454.VPTU.Pokedex.Moves;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex.Select
{
    /// <summary>
    ///     Interaction logic for Select_Move.xaml
    /// </summary>
    public partial class Select_Move : Window
    {
        /// <summary>
        ///     The Move Data that was selected
        /// </summary>
        public MoveData Selected_Move;

        public Select_Move()
        {
            InitializeComponent();

            ReloadList();
        }

        private void ReloadList(string SearchName = "")
        {
            Dispatcher.Invoke(() => Moves.Items.Clear());

            foreach (var data in MainWindow.SaveManager.SaveData.PokedexData.Moves)
                if (data.Name.ToLower().Contains(SearchName.ToLower()))
                    Dispatcher.Invoke(new Action(() => Moves.Items.Add(data)));
        }

        private void Moves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selected_Move = (MoveData) Moves.SelectedItem;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_Move == null)
                DialogResult = false;
            else
                DialogResult = true;

            Close();
        }

        #region Searching Code

        private Thread SearchThread;

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Stop the thread from searching
            try
            {
                SearchThread.Abort();
                SearchThread = null;
            }
            catch
            {
            }
            var Filter = textBox.Text;

            SearchThread = new Thread(() => ReloadList(Filter));
            SearchThread.IsBackground = true;
            SearchThread.Start();
        }

        #endregion
    }
}