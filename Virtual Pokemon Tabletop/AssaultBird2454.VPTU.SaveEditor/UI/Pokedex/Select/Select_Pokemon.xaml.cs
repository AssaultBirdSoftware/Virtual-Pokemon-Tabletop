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
    public partial class Select_Pokemon : Window
    {
        /// <summary>
        /// The Move Data that was selected
        /// </summary>
        public VPTU.Pokedex.Pokemon.PokemonData Selected_Pokemon;
        private VPTU.Pokedex.Save_Data.Pokedex Pokedex;

        public Select_Pokemon(VPTU.Pokedex.Save_Data.Pokedex _Pokedex)
        {
            Pokedex = _Pokedex;

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
            this.Dispatcher.Invoke(new Action(() => Pokemon.Items.Clear()));

            foreach (VPTU.Pokedex.Pokemon.PokemonData data in Pokedex.Pokemon)
            {
                if (data.Species_Name.ToLower().Contains(SearchName.ToLower()) || data.Species_DexID.ToString().Contains(SearchName))
                {
                    this.Dispatcher.Invoke(new Action(() => Pokemon.Items.Add(data)));
                }
            }
        }

        private void Moves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selected_Pokemon = (VPTU.Pokedex.Pokemon.PokemonData)Pokemon.SelectedItem;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Selected_Pokemon == null)
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
