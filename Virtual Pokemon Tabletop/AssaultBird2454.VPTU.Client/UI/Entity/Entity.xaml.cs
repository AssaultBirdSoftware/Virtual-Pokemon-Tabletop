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
}
