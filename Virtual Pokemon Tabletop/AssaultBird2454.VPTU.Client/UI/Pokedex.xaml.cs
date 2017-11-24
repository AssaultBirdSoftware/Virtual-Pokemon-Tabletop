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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    /// Interaction logic for Pokedex.xaml
    /// </summary>
    public partial class Pokedex : UserControl
    {
        public Pokedex()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the user wants to update the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBar_Reload_Click(object sender, RoutedEventArgs e)
        {
            Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Pokedex.Pokedex_Pokemon_GetList());// Gets the list again
        }

        Thread UpdateThread;
        /// <summary>
        /// Updates the list of pokemon
        /// </summary>
        /// <param name="Pokemon">The Pokedex List to use</param>
        public void Pokedex_Pokemon_Get_Executed(List<VPTU.Pokedex.Pokemon.PokemonData> Pokemon)
        {
            try
            {
                UpdateThread.Abort();
                UpdateThread = null;
            }
            catch { /* Dont Care */ }

            UpdateThread = new Thread(new ThreadStart(() =>// Runs the update in a thread that can be aborted if a new list is retrieved
            {
                List.Dispatcher.Invoke(new Action(() =>// Runs the update on the correct thread
                {
                    List.Items.Clear();// Clears the ListView

                    foreach (VPTU.Pokedex.Pokemon.PokemonData data in Pokemon)// Runs through the Pokedex List
                    {
                        List.Dispatcher.Invoke(new Action(() => List.Items.Add(data)));// And adds it to the ListView
                    }
                }));
            }));
            UpdateThread.IsBackground = true;
            UpdateThread.Start();
        }

        /// <summary>
        /// Gets the pokemon selected and opens a Pokedex Card for it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Pokedex.Pokedex_Pokemon()// Gets the Pokemon Selected
            {
                Command = "Pokedex_Pokemon_Get",// Sets the command
                DexID = ((VPTU.Pokedex.Pokemon.PokemonData)List.SelectedItems[0]).Species_DexID// Sets the Pokemon ID to get
            });
        }
    }
}
