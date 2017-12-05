using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    /// Interaction logic for Pokemon_Species.xaml
    /// </summary>
    public partial class Pokemon_Species : UserControl
    {
        public Pokemon_Species()
        {
            InitializeComponent();
        }

        public void Update(VPTU.Pokedex.Pokemon.PokemonData Data)
        {
            Name.Content = Data.Species_Name;

            BaseStats_HP.Content = Data.Species_BaseStats_HP;
            BaseStats_Attack.Content = Data.Species_BaseStats_Attack;
            BaseStats_Defence.Content = Data.Species_BaseStats_Defence;
            BaseStats_SpAttack.Content = Data.Species_BaseStats_SpAttack;
            BaseStats_SpDefence.Content = Data.Species_BaseStats_SpDefence;
            BaseStats_Speed.Content = Data.Species_BaseStats_Speed;

            Skills_Athl.Content = (int)Data.Species_Skill_Data.Athletics_Rank + "D6+" + Data.Species_Skill_Data.Athletics_Mod;
            Skills_Acro.Content = (int)Data.Species_Skill_Data.Acrobatics_Rank + "D6+" + Data.Species_Skill_Data.Acrobatics_Mod;
            Skills_Combat.Content = (int)Data.Species_Skill_Data.Combat_Rank + "D6+" + Data.Species_Skill_Data.Combat_Mod;
            Skills_Stealth.Content = (int)Data.Species_Skill_Data.Stealth_Rank + "D6+" + Data.Species_Skill_Data.Stealth_Mod;
            Skills_Percep.Content = (int)Data.Species_Skill_Data.Perception_Rank + "D6+" + Data.Species_Skill_Data.Perception_Mod;
            Skills_Focus.Content = (int)Data.Species_Skill_Data.Focus_Rank + "D6+" + Data.Species_Skill_Data.Focus_Mod;

            Biology_Height.Content = Data.Species_SizeClass;
            Biology_Weight.Content = "Weight Class " + (int)Data.Species_WeightClass;
            //foreach(VPTU.Pokedex.Pokemon.Pokemon_Diets diet in )
            Biology_Diet.Content = "No Data Found";
            Biology_Habitat1.Content = "No Data Found";

            try
            {
                if (Data.Evolutions.Count <= 2)
                {
                    int i = 1;
                    foreach (VPTU.Pokedex.Pokemon.Link_Evolutions evo in Data.Evolutions)
                    {
                        if (evo.Evo_Type == VPTU.Pokedex.Pokemon.Evolution_Type.Normal)
                        {
                            if (i == 1) { Evolution_Name1.Content = "-"; Evolution_Level1.Content = "-"; i = 2; }
                            else { Evolution_Name2.Content = "-"; Evolution_Level2.Content = "-"; }
                        }
                    }
                }
                else
                {
                    Evolution_Name1.Content = "More than 2";
                    Evolution_Name2.Content = "Possible Forms";
                }
            }
            catch { /* Failed to Load Evolution Data */ }

            Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Resources.ImageResource
            {
                UseCommand = "Pokedex_Species",
                UseID = Data.Species_DexID.ToString(),
                Resource_ID = Data.Sprite_Normal
            });// Retrieves the Image
        }
        public void UpdateImage(Bitmap bmp)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            Image.Background = new ImageBrush(bitmapSource);
        }
    }
}
