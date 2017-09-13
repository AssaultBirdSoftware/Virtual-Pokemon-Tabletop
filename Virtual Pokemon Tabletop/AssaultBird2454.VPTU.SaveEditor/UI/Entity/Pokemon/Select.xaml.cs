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

namespace AssaultBird2454.VPTU.SaveEditor.UI.Entity.Pokemon
{
    /// <summary>
    /// Interaction logic for Select.xaml
    /// </summary>
    public partial class Select : Window
    {
        public EntityManager.Pokemon.PokemonCharacter SelectedPokemon;
        private SaveManager.SaveManager Manager;

        public Select(SaveManager.SaveManager _Manager)
        {
            Manager = _Manager;

            InitializeComponent();
        }

        public Thread search_thread;
        public void Load()
        {
            try { search_thread.Abort(); } catch { }
            search_thread = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => Reload()));
            }));

            search_thread.Start();
        }

        public void Reload()
        {
            Pokemon_List.Items.Clear();

            if (Search_WildPokemon.IsChecked == true)
            {
                foreach (EntityManager.Pokemon.PokemonCharacter pokemon in Manager.SaveData.Pokemon)
                {
                    if (pokemon.Name.ToLower().Contains(Search_Name.Text.ToLower()))
                    {
                        Pokemon_List.Items.Add(new Pokemon_DataBind(pokemon, pokemon.Species_DexID + " (" + Manager.SaveData.PokedexData.Pokemon.Find(x => x.Species_DexID == pokemon.Species_DexID).Species_Name + ")"));
                    }
                }
            }

            if (Search_Trainer_Pokemon.IsChecked == true)
            {
                foreach (EntityManager.Trainer.TrainerCharacter trainer in Manager.SaveData.Trainers)
                {
                    foreach (EntityManager.Pokemon.PokemonCharacter pokemon in trainer.PartyPokemon)
                    {
                        if (pokemon.Name.ToLower().Contains(Search_Name.Text.ToLower()))
                        {
                            Pokemon_List.Items.Add(new Pokemon_DataBind(pokemon, pokemon.Species_DexID + " (" + Manager.SaveData.PokedexData.Pokemon.Find(x => x.Species_DexID == pokemon.Species_DexID).Species_Name + ")", trainer.Name));
                        }
                    }
                }
            }
        }


        private void Search_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load();
        }
        private void Search_Trainer_Pokemon_Unchecked(object sender, RoutedEventArgs e)
        {
            Load();
        }
        private void Search_WildPokemon_Unchecked(object sender, RoutedEventArgs e)
        {
            Load();
        }
        private void Search_WildPokemon_Checked(object sender, RoutedEventArgs e)
        {
            Load();
        }
        private void Search_Trainer_Pokemon_Checked(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void Select_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            SelectedPokemon = ((Pokemon_DataBind)Pokemon_List.SelectedItem).Pokemon;
            Close();
        }
    }

    public class Pokemon_DataBind
    {
        public Pokemon_DataBind(EntityManager.Pokemon.PokemonCharacter _Pokemon, string _Species, string _Owner = "")
        {
            Pokemon = _Pokemon;
            Species = _Species;
            Owner = _Owner;
        }

        public EntityManager.Pokemon.PokemonCharacter Pokemon { get; set; }
        public string Species { get; set; }
        public string Owner { get; set; }
    }
}
