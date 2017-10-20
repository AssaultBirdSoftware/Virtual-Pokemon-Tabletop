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
            Basic_Name.Content = Data.Species_Name;// Name

            //Basic_Type.Content = "Type: ";// Type
            //if (Data.Species_Types != null)
            //{
            //    foreach (BattleManager.Data.Type type in Data.Species_Types)
            //    {
            //        Basic_Type.Content += type.ToString() + ", ";
            //    }
            //    Basic_Type.Content = Basic_Type.Content.ToString().Remove(Basic_Type.Content.ToString().Length - 2, 2);
            //}
            //else { Basic_Type.Content += "??"; }// Adds Type's and removes bad characters

            Basic_Height.Content = Data.Species_SizeClass.ToString();// Size
            Basic_Weight.Content = Data.Species_WeightClass.ToString();// Weight
            Basic_Diet.Content = "Diet: ???";
            Basic_Habitat_1.Content = "Habitat: ???";

            Breed_Gender_Ratio.Content = Data.Species_Gender_Chance_Male + "% M / " + Data.Species_Gender_Chance_Female + "% F";
            Breed_EggGroups_1.Content = "???";
            Breed_HatchRate.Content = "Rate: ???";

            Stats_Base_HP.Content = Data.Species_BaseStats_HP;
            Stats_Base_Attack.Content = Data.Species_BaseStats_Attack;
            Stats_Base_Defence.Content = Data.Species_BaseStats_Defence;
            Stats_Base_SpAttack.Content = Data.Species_BaseStats_SpAttack;
            Stats_Base_SpDefence.Content = Data.Species_BaseStats_SpDefence;
            Stats_Base_Speed.Content = Data.Species_BaseStats_Speed;
        }
    }
}
