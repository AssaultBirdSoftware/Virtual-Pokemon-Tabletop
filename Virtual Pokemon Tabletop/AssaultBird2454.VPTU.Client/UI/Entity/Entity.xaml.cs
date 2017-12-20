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
        public Entity()
        {
            InitializeComponent();
        }

        public void Update_Pokemon(EntityManager.Pokemon.PokemonCharacter Pokemon)
        {
            Basic_CharacterType.Content = "Pokemon";
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

        private void ScrollViewer_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
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
