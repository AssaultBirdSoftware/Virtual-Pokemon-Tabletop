using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using AssaultBird2454.VPTU.Pokedex.Moves;
using AssaultBird2454.VPTU.Pokedex.Pokemon;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Pokedex.Select
{
    /// <summary>
    ///     Interaction logic for Select_Move.xaml
    /// </summary>
    public partial class Select_Move : Window
    {
        private readonly List<Link_Moves> Obtainable_Moves;
        private readonly VPTU.Pokedex.Save_Data.Pokedex Pokedex;
        private readonly bool SearchPokemon = true;

        /// <summary>
        ///     The Move Data that was selected
        /// </summary>
        public MoveData Selected_Move;

        public Select_Move(VPTU.Pokedex.Save_Data.Pokedex _Pokedex, PokemonData _Pokemon = null)
        {
            Pokedex = _Pokedex;

            if (_Pokemon != null)
            {
                Obtainable_Moves = _Pokemon.Moves;
                if (Obtainable_Moves == null)
                    Obtainable_Moves = new List<Link_Moves>();

                SearchPokemon = true;
            }

            if (_Pokemon == null)
            {
                Obtainable_Moves = new List<Link_Moves>();
                SearchPokemon = false;
            }

            InitializeComponent();

            if (SearchPokemon)
                Hide_Unobtainable.Visibility = Visibility.Visible;
            else
                Hide_Unobtainable.Visibility = Visibility.Hidden;

            ReloadList();
        }

        private void ReloadList(string SearchName = "")
        {
            Dispatcher.Invoke(() => Moves.Items.Clear());

            foreach (var data in Pokedex.Moves)
                if (data.Name.ToLower().Contains(SearchName.ToLower()))
                    if (!SearchPokemon)
                        Dispatcher.Invoke(new Action(() => Moves.Items.Add(data)));
                    else if (Hide_Unobtainable.IsChecked == true &&
                             Obtainable_Moves.Find(x => x.MoveName.ToLower() == data.Name.ToLower()) != null)
                        Dispatcher.Invoke(new Action(() => Moves.Items.Add(data)));
                    else if (Hide_Unobtainable.IsChecked == false)
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

        private void Hide_Unobtainable_Checked(object sender, RoutedEventArgs e)
        {
            ReloadList();
        }

        private void Hide_Unobtainable_Unchecked(object sender, RoutedEventArgs e)
        {
            ReloadList();
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