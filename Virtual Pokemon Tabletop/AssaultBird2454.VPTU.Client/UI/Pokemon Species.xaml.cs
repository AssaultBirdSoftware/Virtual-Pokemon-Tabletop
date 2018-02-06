using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AssaultBird2454.VPTU.Pokedex.Pokemon;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Resources;

namespace AssaultBird2454.VPTU.Client.UI
{
    /// <summary>
    ///     Interaction logic for Pokemon_Species.xaml
    /// </summary>
    public partial class Pokemon_Species : UserControl
    {
        public Pokemon_Species()
        {
            InitializeComponent();
        }

        public void Update(PokemonData Data)
        {
            Name.Content = Data.Species_Name;

            BaseStats_HP.Content = Data.Species_BaseStats_HP;
            BaseStats_Attack.Content = Data.Species_BaseStats_Attack;
            BaseStats_Defence.Content = Data.Species_BaseStats_Defence;
            BaseStats_SpAttack.Content = Data.Species_BaseStats_SpAttack;
            BaseStats_SpDefence.Content = Data.Species_BaseStats_SpDefence;
            BaseStats_Speed.Content = Data.Species_BaseStats_Speed;

            Skills_Athl.Content = (int)Data.Species_Skill_Data.Athletics_Rank + "D6+" +
                                  Data.Species_Skill_Data.Athletics_Mod;
            Skills_Acro.Content = (int)Data.Species_Skill_Data.Acrobatics_Rank + "D6+" +
                                  Data.Species_Skill_Data.Acrobatics_Mod;
            Skills_Combat.Content = (int)Data.Species_Skill_Data.Combat_Rank + "D6+" +
                                    Data.Species_Skill_Data.Combat_Mod;
            Skills_Stealth.Content = (int)Data.Species_Skill_Data.Stealth_Rank + "D6+" +
                                     Data.Species_Skill_Data.Stealth_Mod;
            Skills_Percep.Content = (int)Data.Species_Skill_Data.Perception_Rank + "D6+" +
                                    Data.Species_Skill_Data.Perception_Mod;
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
                    var i = 1;
                    foreach (var evo in Data.Evolutions)
                        if (evo.Evo_Type == Evolution_Type.Normal)
                            if (i == 1)
                            {
                                Evolution_Name1.Content = "-";
                                Evolution_Level1.Content = "-";
                                i = 2;
                            }
                            else
                            {
                                Evolution_Name2.Content = "-";
                                Evolution_Level2.Content = "-";
                            }
                }
                else
                {
                    Evolution_Name1.Content = "More than 2";
                    Evolution_Name2.Content = "Possible Forms";
                }
            }
            catch
            {
                /* Failed to Load Evolution Data */
            }

            Program.ClientInstance.Client.SendData(new ImageResource
            {
                UseCommand = "Pokedex_Species",
                UseID = Data.Species_DexID.ToString(),
                Resource_ID = Data.Sprite_Normal
            }); // Retrieves the Image
        }

        public void UpdateImage(Bitmap bmp)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            Image.Background = new ImageBrush(bitmapSource);
        }
        public void UpdateTypeIcon(int Type, Bitmap bmp)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            switch (Type)
            {
                case 1:
                    Type_Icon1.Fill = new ImageBrush(bitmapSource);
                    break;
                case 2:
                    Type_Icon2.Fill = new ImageBrush(bitmapSource);
                    break;
            }
        }
    }
}