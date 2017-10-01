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

namespace AssaultBird2454.VPTU.Client.Class.Controls
{
    /// <summary>
    /// Interaction logic for Pokedex_Pokemon.xaml
    /// </summary>
    public partial class Pokedex_Pokemon : UserControl
    {
        public Pokedex_Pokemon()
        {
            InitializeComponent();
        }

        public void Load(VPTU.Pokedex.Pokemon.PokemonData Data)
        {
            Basic_Name.Content = "Name: " + Data.Species_Name;// Name

            Basic_Type.Content = "Type: ";// Type
            if (Data.Species_Types != null)
            {
                foreach (BattleManager.Data.Type type in Data.Species_Types)
                {
                    Basic_Type.Content += type.ToString() + ", ";
                }
                Basic_Type.Content = Basic_Type.Content.ToString().Remove(Basic_Type.Content.ToString().Length - 2, 2);
            }
            else { Basic_Type.Content += "??"; }// Adds Type's and removes bad characters

            Basic_Size.Content = "Size: " + Data.Species_SizeClass.ToString();// Size
            Basic_Weight.Content = "Weight: " + Data.Species_WeightClass.ToString();// Weight
            Basic_Diet.Content = "Diet: ???";
            Basic_Habitat.Content = "Habitat: ???";

            Breading_Male.Content = "Male Ratio: " + Data.Species_Gender_Chance_Male.ToString() + "%";
            Breading_Female.Content = "Female Ratio: " + Data.Species_Gender_Chance_Female.ToString() + "%";
            Breading_Group.Content = "Group: ???";
            Breading_Rate.Content = "Rate: ???";

            Stat_HP.Content = "HP: " + Data.Species_BaseStats_HP;
            Stat_Attack.Content = "Attack: " + Data.Species_BaseStats_Attack;
            Stat_Defence.Content = "Defence: " + Data.Species_BaseStats_Defence;
            Stat_SpAttack.Content = "Sp. Attack: " + Data.Species_BaseStats_SpAttack;
            Stat_SpDefence.Content = "Sp. Defence: " + Data.Species_BaseStats_SpDefence;
            Stat_Speed.Content = "Speed: " + Data.Species_BaseStats_Speed;
        }
    }
}
