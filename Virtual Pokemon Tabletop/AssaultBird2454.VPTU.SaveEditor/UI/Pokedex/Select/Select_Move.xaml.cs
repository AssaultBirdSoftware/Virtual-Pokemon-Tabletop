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
        private VPTU.Pokedex.Save_Data.Pokedex Pokedex;

        private List<VPTU.Pokedex.Pokemon.Link_Moves> Obtainable_Moves;
        private bool SearchPokemon = true;

        public Select_Move(VPTU.Pokedex.Save_Data.Pokedex _Pokedex, VPTU.Pokedex.Pokemon.PokemonData _Pokemon = null)
        {
            Pokedex = _Pokedex;

            if (_Pokemon != null)
            {
                Obtainable_Moves = _Pokemon.Moves;
                if(Obtainable_Moves == null)
                {
                    Obtainable_Moves = new List<VPTU.Pokedex.Pokemon.Link_Moves>();
                }
                
                SearchPokemon = true;
            }

            if (_Pokemon == null)
            {
                Obtainable_Moves = new List<VPTU.Pokedex.Pokemon.Link_Moves>();
                SearchPokemon = false;
            }

            InitializeComponent();

            if (SearchPokemon)
            {
                Hide_Unobtainable.Visibility = Visibility.Visible;
            }
            else
            {
                Hide_Unobtainable.Visibility = Visibility.Hidden;
            }

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
            }
            catch { }
            string Filter = textBox.Text;

            SearchThread = new Thread(new ThreadStart(new Action(() => ReloadList(Filter))));
            SearchThread.IsBackground = true;
            SearchThread.Start();
        }
        #endregion

        private void ReloadList(string SearchName = "")
        {
            this.Dispatcher.Invoke(new Action(() => Moves.Items.Clear()));

            foreach (VPTU.Pokedex.Moves.MoveData data in Pokedex.Moves)
            {
                if (data.Name.ToLower().Contains(SearchName.ToLower()))
                {
                    if (!SearchPokemon)
                    {
                        this.Dispatcher.Invoke(new Action(() => Moves.Items.Add(data)));
                        continue;
                    }
                    else if (Hide_Unobtainable.IsChecked == true && Obtainable_Moves.Find(x => x.MoveName.ToLower() == data.Name.ToLower()) != null)
                    {
                        this.Dispatcher.Invoke(new Action(() => Moves.Items.Add(data)));
                    }else if(Hide_Unobtainable.IsChecked == false)
                    {
                        this.Dispatcher.Invoke(new Action(() => Moves.Items.Add(data)));
                    }
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

        private void Hide_Unobtainable_Checked(object sender, RoutedEventArgs e)
        {
            ReloadList();
        }
        private void Hide_Unobtainable_Unchecked(object sender, RoutedEventArgs e)
        {
            ReloadList();
        }
    }
}
