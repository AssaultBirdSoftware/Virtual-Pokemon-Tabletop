using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AssaultBird2454.VPTU.Pokedex.Pokemon;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Pokedex;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    ///     Interaction logic for Pokedex.xaml
    /// </summary>
    public partial class Pokedex : UserControl
    {
        private List<PokemonData> Pokemon;

        private Thread UpdateThread;

        public Pokedex()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     When the user wants to update the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBar_Reload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Program.ClientInstance.Client.SendData(new Pokedex_Pokemon_GetList()); // Gets the list again
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You need to connect to a running server first before you can load this list!");
            }
        }

        /// <summary>
        ///     Updates the list of pokemon
        /// </summary>
        /// <param name="Pokemon">The Pokedex List to use</param>
        public void Pokedex_Pokemon_Get_Executed(List<PokemonData> _Pokemon)
        {
            Pokemon = _Pokemon;

            Reload();
        }

        public void Reload()
        {
            try
            {
                UpdateThread.Abort();
                UpdateThread = null;
            }
            catch
            {
                /* Dont Care */
            }

            UpdateThread = new Thread(
                () => // Runs the update in a thread that can be aborted if a new list is retrieved
                {
                    List.Dispatcher.Invoke(() => // Runs the update on the correct thread
                    {
                        List.Items.Clear(); // Clears the ListView

                        foreach (var data in Pokemon) // Runs through the Pokedex List
                            if (data.Species_Name.ToLower().Contains(ToolBar_Search.Text.ToLower()))
                                List.Dispatcher.Invoke(
                                    new Action(() => List.Items.Add(data))); // And adds it to the ListView
                    });
                });
            UpdateThread.IsBackground = true;
            UpdateThread.Start();
        }

        /// <summary>
        ///     Gets the pokemon selected and opens a Pokedex Card for it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Program.ClientInstance.Client.SendData(new Pokedex_Pokemon // Gets the Pokemon Selected
                {
                    DexID = ((PokemonData)List.SelectedItems[0]).Species_DexID // Sets the Pokemon ID to get
                });
            }
            catch { /* Dont Care */ }
        }

        private void ToolBar_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Reload();
        }
    }
}