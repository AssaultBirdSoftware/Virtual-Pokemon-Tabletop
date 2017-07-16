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

namespace AssaultBird2454.VPTU.SaveEditor.UI.BattleEffect
{
    /// <summary>
    /// Interaction logic for BattleEffect_Designer.xaml
    /// </summary>
    public partial class BattleEffect_Designer : Window
    {
        /// <summary>
        /// A Context Menu for the Effect Function Root
        /// </summary>
        private ContextMenu CM_EffectFunctionHead;

        public BattleEffect_Designer()
        {
            InitializeComponent();

            #region Effect Function Head Context Menu
            CM_EffectFunctionHead = new ContextMenu();// Creates Context Menu Object
            CM_EffectFunctionHead.Opened += CM_EffectFunctionHead_Opened;

            MenuItem Create_Effect = new MenuItem();// Creates Create Menu Item
            Create_Effect.Header = "Create Effect Function";// Sets Header
            Create_Effect.Click += CM_EffectFunctionHead_Create;// Sets Callback
            CM_EffectFunctionHead.Items.Add(Create_Effect);// Adds to Menu

            CM_EffectFunctionHead.Items.Add(new Separator());// Adds a Spacer

            MenuItem Export_Effect = new MenuItem();// Creates Create Menu Item
            Export_Effect.Header = "Export All Effect Functions";// Sets Header
            Export_Effect.Click += CM_EffectFunctionHead_Export;// Sets Callback
            CM_EffectFunctionHead.Items.Add(Export_Effect);// Adds to Menu

            EffectTree_Effects.ContextMenu = CM_EffectFunctionHead;// Links CM
            #endregion
        }

        private void CM_EffectFunctionHead_Opened(object sender, RoutedEventArgs e)
        {
            
        }

        private void CM_EffectFunctionHead_Create(object sender, RoutedEventArgs e)
        {
            EffectFunctions_RegisterFunction();
        }

        private void CM_EffectFunctionHead_Export(object sender, RoutedEventArgs e)
        {
            UI.RAW_JSON exp = new RAW_JSON();
            exp.Export<List<BattleManager.BattleEffect.EffectData>>(null);
            bool? saved = exp.ShowDialog();

            if(saved == true)
            {

            }
        }

        #region Effect Functions
        private void EffectFunctions_RegisterFunction(string Function_Name = "Effect Function 1")
        {
            TreeViewItem tvi = new TreeViewItem();
            tvi.Tag = new BattleManager.BattleEffect.EffectData();

            ContextMenu ctxmnu = new ContextMenu();
            ctxmnu.Opened += CM_EffectFunctionItem_Opened;
            ctxmnu.Tag = tvi;

            MenuItem editmenu = new MenuItem();
            editmenu.Header = "Edit Function";
            editmenu.Click += CM_EffectFunctionItem_Edit_Click;
            ctxmnu.Items.Add(editmenu);

            MenuItem deletemenu = new MenuItem();
            deletemenu.Header = "Delete Function";
            deletemenu.Click += CM_EffectFunctionItem_Delete_Click;
            ctxmnu.Items.Add(deletemenu);

            tvi.Header = Function_Name;
            tvi.ContextMenu = ctxmnu;

            EffectTree_Effects.Items.Add(tvi);
        }

        private void EffectFunctions_UnRegisterFunction(string Function_Name)
        {
            object Item = null;
            foreach(object obj in EffectTree_Effects.Items)
            {
                if ((((TreeViewItem)obj).Header).ToString().ToLower() == Function_Name.ToLower())
                {
                    Item = obj;
                }
            }

            if(Item != null)
            {
                EffectTree_Effects.Items.Remove(Item);
                Item = null;
            }
            else { /* Item Not Found */ }
        }
        private void EffectFunctions_UnRegisterFunction(object Function_Item)
        {
            if (EffectTree_Effects.Items.Contains(Function_Item))
            {
                EffectTree_Effects.Items.Remove(Function_Item);
                Function_Item = null;
            }
            else { /* Item Not Found */ }
        }
        private void EffectFunctions_GetFunction(string Function_Name)
        {

        }

        #region Context Menu Events
        private void CM_EffectFunctionItem_Opened(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void CM_EffectFunctionItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void CM_EffectFunctionItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            EffectFunctions_UnRegisterFunction(((ContextMenu)((MenuItem)sender).Parent).Tag);
        }
        #endregion
        #endregion
    }
}
