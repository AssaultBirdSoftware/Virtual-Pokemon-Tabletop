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

namespace AssaultBird2454.VPTU.Client.Class.Controls
{
    /// <summary>
    /// Interaction logic for Pokedex.xaml
    /// </summary>
    public partial class Pokedex : UserControl
    {
        public event Button_Pressed Reload_Pressed;
        List<VPTU.Pokedex.Pokemon.PokemonData> PokemonData { get; set; }

        public Pokedex()
        {
            InitializeComponent();
            PokemonData = new List<VPTU.Pokedex.Pokemon.PokemonData>();
        }

        private void Tools_Reload_Click(object sender, RoutedEventArgs e)
        {
            List.Dispatcher.Invoke(new Action(() => List.Items.Clear()));
            Reload_Pressed?.Invoke();
        }

        public void Update_Pokemon_List(List<VPTU.Pokedex.Pokemon.PokemonData> _PokemonData)
        {
            PokemonData = _PokemonData;

            Reload_List();
        }

        private Thread Search_Thread;
        public void Reload_List()
        {
            try
            {
                Search_Thread.Abort();
                Search_Thread = null;
            }
            catch { }

            Search_Thread = new Thread(new ThreadStart(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    List.Items.Clear();

                    #region Pokemon
                    if (Search_Pokemon.IsChecked == true)
                    {
                        foreach (VPTU.Pokedex.Pokemon.PokemonData pokemon in PokemonData)
                        {
                            if (pokemon.Species_Name.ToLower().Contains(Search_Name.Text.ToLower()))
                            {
                                List.Items.Add(pokemon);
                            }
                        }
                    }
                    #endregion

                }));
            }));
            Search_Thread.Start();
        }

        internal void Update_PokedexData(object Data)
        {
            if (Data is Server.Instances.CommandData.Pokedex.Get_Pokedex_Pokemon)
            {
                try
                {
                    Search_Thread.Abort();
                    Search_Thread = null;
                }
                catch { }

                try
                {
                    PokemonData = ((Server.Instances.CommandData.Pokedex.Get_Pokedex_Pokemon)Data).Pokemon_Dex;
                }
                catch { }

                Reload_List();
            }
        }
    }
}
