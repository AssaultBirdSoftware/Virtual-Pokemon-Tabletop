using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AssaultBird2454.VPTU.Client.UI.Entity
{
    /// <summary>
    /// Interaction logic for Player_Entity.xaml
    /// </summary>
    public partial class Entity : UserControl
    {
        EntityManager.Entity_Type EntityType { get; set; }
        EntityManager.Pokemon.PokemonCharacter Pokemon;
        EntityManager.Trainer.TrainerCharacter Trainer;

        public Entity()
        {
            InitializeComponent();
        }

        public void Update_Pokemon(EntityManager.Pokemon.PokemonCharacter _Pokemon)
        {
            Basic_CharacterType.Content = "Pokemon";// Set Character Type
            EntityType = EntityManager.Entity_Type.Pokemon;// Set Entity Type
            Pokemon = _Pokemon;// Sets Pokemon
            Trainer = null;// Clears Trainer

            #region Basic Info Tab
            #region Basic
            Basic_Name.Text = Pokemon.Name;// Sets the Name
            Basic_Species.Text = "Not Available";// Sets the Species Name
            Basic_Type.Text = Pokemon.TypeString;// Sets the Type
            Basic_HeldItem.Text = "Not Available";// Sets the Held Item
            Basic_Nature.Text = Pokemon.Nature.ToString();// Sets the Neature
            Basic_EXP.Value = Pokemon.EXP;// Sets the EXP Level
            Basic_EXP_TNL.Text = Pokemon.Next_EXP_Requirement.ToString();// Sets the EXP required for next level
            EXP_Bar.Minimum = Pokemon.Prev_EXP_Requirement; EXP_Bar.Maximum = EntityManager.Pokemon.PokemonCharacter.EXP_Markers(Pokemon.Level + 1); EXP_Bar.Value = Pokemon.EXP;
            Basic_UsedAP.Value = 0;// Sets the UsedAP
            Basic_MaxAP.Text = "-";// Sets the UsedAP
            Basic_Gender.Text = Pokemon.Gender.ToString();// Sets the Gender
            Basic_Height.Text = "Class " + ((int)Pokemon.SizeClass).ToString();// Sets the Size / Height
            Basic_Weight.Text = "Class " + ((int)Pokemon.WeightClass).ToString();// Sets the Weight
            #endregion

            #region Stats

            #endregion
            #endregion
        }
        public void Update_Trainer(EntityManager.Trainer.TrainerCharacter Trainer)
        {
            Basic_CharacterType.Content = "Trainer";
        }

        public void Update_Token(Bitmap Image)
        {
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            Basic_Token.Background = new ImageBrush(bitmapSource);
        }

        #region Value Update Events
        private void Basic_EXP_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int exp = 0;
            if (!int.TryParse(Basic_EXP.Value.ToString(), out exp))
            {
                Basic_Level.Text = "-";// Sets the Level
                Basic_EXP_TNL.Text = "-";// Sets the EXP required for next level
                EXP_Bar.Minimum = 0; EXP_Bar.Maximum = 1; EXP_Bar.Value = 1;
                EXP_Bar.Foreground = new SolidColorBrush(new System.Windows.Media.Color() { R = 255, G = 0, B = 0, A = 30 });// #4CFF0000
                EXP_Bar.IsIndeterminate = true;
                return;
            }
            else
            {
                EXP_Bar.IsIndeterminate = false;
                EXP_Bar.Foreground = new SolidColorBrush(new System.Windows.Media.Color() { R = 0, G = 92, B = 255, A = 30 });// #4C005CFF
            }

            if (EntityType == EntityManager.Entity_Type.Pokemon)
            {
                if (Basic_EXP.IsEnabled)
                    Pokemon.EXP = (int)Basic_EXP.Value;
                Basic_Level.Text = Pokemon.Level.ToString();// Sets the Level
                Basic_EXP_TNL.Text = Pokemon.Next_EXP_Requirement.ToString();// Sets the EXP required for next level
                EXP_Bar.Minimum = Pokemon.Prev_EXP_Requirement; EXP_Bar.Maximum = EntityManager.Pokemon.PokemonCharacter.EXP_Markers(Pokemon.Level + 1); EXP_Bar.Value = Pokemon.EXP; EXP_Bar.IsIndeterminate = false;
            }
            else if (EntityType == EntityManager.Entity_Type.Trainer)
            {

            }
        }
        #endregion
    }

    public static class CustomDesignAttributes
    {
        private static bool? _isInDesignMode;

        public static DependencyProperty VerticalScrollToProperty = DependencyProperty.RegisterAttached(
          "VerticalScrollTo",
          typeof(double),
          typeof(CustomDesignAttributes),
          new PropertyMetadata(ScrollToChanged));

        public static DependencyProperty HorizontalScrollToProperty = DependencyProperty.RegisterAttached(
          "HorizontalScrollTo",
          typeof(double),
          typeof(CustomDesignAttributes),
          new PropertyMetadata(ScrollToChanged));

        private static bool IsInDesignMode
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode =
                      (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
                }

                return _isInDesignMode.Value;
            }
        }

        public static void SetVerticalScrollTo(UIElement element, double value)
        {
            element.SetValue(VerticalScrollToProperty, value);
        }

        public static double GetVerticalScrollTo(UIElement element)
        {
            return (double)element.GetValue(VerticalScrollToProperty);
        }

        public static void SetHorizontalScrollTo(UIElement element, double value)
        {
            element.SetValue(HorizontalScrollToProperty, value);
        }

        public static double GetHorizontalTo(UIElement element)
        {
            return (double)element.GetValue(HorizontalScrollToProperty);
        }

        private static void ScrollToChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!IsInDesignMode)
                return;
            ScrollViewer viewer = d as ScrollViewer;
            if (viewer == null)
                return;
            if (e.Property == VerticalScrollToProperty)
            {
                viewer.ScrollToVerticalOffset((double)e.NewValue);
            }
            else if (e.Property == HorizontalScrollToProperty)
            {
                viewer.ScrollToHorizontalOffset((double)e.NewValue);
            }
        }
    }
}
