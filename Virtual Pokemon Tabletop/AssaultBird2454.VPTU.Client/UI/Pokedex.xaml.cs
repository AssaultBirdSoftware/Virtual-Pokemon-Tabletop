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

        private void ToolBar_Reload_Click(object sender, RoutedEventArgs e)
        {
            List.Items.Clear();

            Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Pokedex.Pokedex_Pokemon_GetList());
        }

        public void Pokedex_Pokemon_Get_Executed(List<VPTU.Pokedex.Pokemon.PokemonData> Pokemon)
        {

            foreach (VPTU.Pokedex.Pokemon.PokemonData data in Pokemon)
            {
                List.Dispatcher.Invoke(new Action(() => List.Items.Add(data)));
            }
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Pokedex.Pokedex_Pokemon()
            {
                Command = "Pokedex_Pokemon_Get",
                DexID = ((VPTU.Pokedex.Pokemon.PokemonData)List.SelectedItems[0]).Species_DexID
            });
        }
    }
}
