using AssaultBird2454.VPTU.SaveEditor.UI.Resources;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.SaveEditor.UI.Battle
{
    /// <summary>
    /// Interaction logic for Type_Editor.xaml
    /// </summary>
    public partial class Typing_Details : Window
    {
        public BattleManager.Typing.Typing_Data TypeData;
        private string Icon_ID = "";

        public Typing_Details(BattleManager.Typing.Typing_Data _TypeData = null)
        {
            InitializeComponent();

            if (_TypeData == null)
            {
                TypeData = new BattleManager.Typing.Typing_Data();
            }
            else { TypeData = _TypeData; }

            var itemSource = new Dictionary<string, object>();
            foreach (BattleManager.Typing.Typing_Data type in MainWindow.SaveManager.SaveData.Typing_Manager.Types)
                itemSource.Add(type.Type_Name, type.Type_Name);
            Type_SE.ItemsSource = itemSource;
            Type_NVE.ItemsSource = itemSource;
            Type_NE.ItemsSource = itemSource;

            Load();
        }

        public void Load_Icon()
        {
            try
            {
                var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                        MainWindow.SaveManager.LoadImage(Icon_ID).GetHbitmap(), IntPtr.Zero,
                        Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                Type_Icon.Background = new ImageBrush(bitmapSource)
                {
                    Stretch = Stretch.Uniform,
                    TileMode = TileMode.None
                };
            }
            catch (Exception ex)
            {
                /* Dont Care, Leave Blank */
            }
        }

        public void Load()
        {
            Icon_ID = TypeData.Resource_Icon;
            Load_Icon();

            Type_Name.Text = TypeData.Type_Name;
            Type_Color.SelectedColor = TypeData.Type_Color;

            #region Super Effective
            Dictionary<string, object> SE_Selection = new Dictionary<string, object>();
            foreach (string type in TypeData.Effect_SuperEffective)
            {
                if (MainWindow.SaveManager.SaveData.Typing_Manager.Types.Find(x => x.Type_Name == type) == null)
                    continue;
                SE_Selection.Add(type, type);
            }
            Type_SE.SelectedItems = SE_Selection;
            #endregion
            #region Not Effective
            Dictionary<string, object> NVE_Selection = new Dictionary<string, object>();
            foreach (string type in TypeData.Effect_NotVery)
            {
                if (MainWindow.SaveManager.SaveData.Typing_Manager.Types.Find(x => x.Type_Name == type) == null)
                    continue;
                NVE_Selection.Add(type, type);
            }
            Type_NVE.SelectedItems = NVE_Selection;
            #endregion
            #region No Effect
            Dictionary<string, object> NE_Selection = new Dictionary<string, object>();
            foreach (string type in TypeData.Effect_NoEffect)
            {
                if (MainWindow.SaveManager.SaveData.Typing_Manager.Types.Find(x => x.Type_Name == type) == null)
                    continue;
                NE_Selection.Add(type, type);
            }
            Type_NE.SelectedItems = NE_Selection;
            #endregion
        }

        private void Type_ChangeIcon_Click(object sender, RoutedEventArgs e)
        {
            var Select = new Search_Resources();

            var dr = Select.ShowDialog();

            if (dr == true)
            {
                Icon_ID = Select.Selected_Resource;
                TypeData.Resource_Icon = Icon_ID;
                Load_Icon();
            }
        }

        #region Save
        private void Type_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            TypeData.Type_Name = Type_Name.Text;
        }
        private void Type_Color_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Type_Color.SelectedColor == null)
            {
                TypeData.Type_Color = Color.FromArgb(255, 0, 0, 0);
            }
            else
            {
                TypeData.Type_Color = (Color)Type_Color.SelectedColor;
            }
        }
        private void Effective_Changed()
        {
            TypeData.Effect_Normal = MainWindow.SaveManager.SaveData.Typing_Manager.Type_Names;
            TypeData.Effect_SuperEffective.Clear();
            TypeData.Effect_NotVery.Clear();
            TypeData.Effect_NoEffect.Clear();

            foreach (KeyValuePair<string, object> SE in Type_SE.SelectedItems)
            {
                if (TypeData.Effect_Normal.Contains(SE.Key))
                {
                    TypeData.Effect_Normal.Remove(SE.Key);
                    TypeData.Effect_SuperEffective.Add(SE.Key);
                }
            }
            foreach (KeyValuePair<string, object> NVE in Type_NVE.SelectedItems)
            {
                if (TypeData.Effect_Normal.Contains(NVE.Key))
                {
                    TypeData.Effect_Normal.Remove(NVE.Key);
                    TypeData.Effect_NotVery.Add(NVE.Key);
                }
            }
            foreach (KeyValuePair<string, object> NE in Type_NE.SelectedItems)
            {
                if (TypeData.Effect_Normal.Contains(NE.Key))
                {
                    TypeData.Effect_Normal.Remove(NE.Key);
                    TypeData.Effect_NoEffect.Add(NE.Key);
                }
            }
        }
        #endregion
    }
}
