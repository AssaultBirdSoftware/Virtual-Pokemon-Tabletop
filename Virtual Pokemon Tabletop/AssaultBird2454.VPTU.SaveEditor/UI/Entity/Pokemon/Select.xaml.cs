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

        public Select()
        {
            InitializeComponent();
        }

        public void Load()
        {
            Pokemon_List.Items.Clear();

            if(Search_WildPokemon.IsChecked == true)
            {
                foreach(EntityManager.Pokemon.PokemonCharacter pokemon in Manager.SaveData.Pokemon)
                {
                    if (pokemon.Name.ToLower().Contains(Search_Name.Text.ToLower()))
                    {
                        Pokemon_List.Items.Add(pokemon);
                    }
                }
            }

            if(Search_Trainer_Pokemon.IsChecked == true)
            {
                foreach(EntityManager.Trainer.TrainerCharacter trainer in Manager.SaveData.Trainers)
                {
                    trainer.
                    // foreach Pokemon in party
                    // add to list if name
                }
            }
        }
    }
}
