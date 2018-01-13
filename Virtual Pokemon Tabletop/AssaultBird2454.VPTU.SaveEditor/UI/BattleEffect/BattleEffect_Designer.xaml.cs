using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AssaultBird2454.VPTU.BattleManager.BattleEffect;

namespace AssaultBird2454.VPTU.SaveEditor.UI.BattleEffect
{
    /// <summary>
    ///     Interaction logic for BattleEffect_Designer.xaml
    /// </summary>
    public partial class BattleEffect_Designer : Window
    {
        /// <summary>
        ///     A Context Menu for the Effect Function Root
        /// </summary>
        private ContextMenu CM_EffectFunctionHead;

        public EffectData EffectData;

        public BattleEffect_Designer(EffectData _EffectData = null)
        {
            InitializeComponent();

            #region Effect Data

            if (_EffectData == null)
            {
                EffectData = new EffectData(); // Creates a new EffectData
            }
            else
            {
                EffectData = _EffectData; // Sets the EffectData
                Load(); // Loads the EffectData
            }

            #endregion

            RootTreeSetup(); // Sets up the context menu for the root tree items
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Save(); // Saves Effects
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            Save(); // Saves Effects
            DialogResult = true; // Sets Dialog Resault
            Close(); // Closes Window
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Sets Dialog Resault
            Close(); // Closes Window
        }

        private void RawData_Button_Click(object sender, RoutedEventArgs e)
        {
            var impexp = new RAW_JSON();
            impexp.Export(EffectData);
            var dr = impexp.ShowDialog();

            if (dr == true)
            {
                EffectData = impexp.Import<EffectData>();
                try
                {
                    Load();
                }
                catch
                {
                    MessageBox.Show("Failed to load data object!");
                }
            }
        }

        #region Base Functions

        /// <summary>
        ///     Saves all the data
        /// </summary>
        public void Save()
        {
            if (EffectData.Functions == null) EffectData.Functions = new List<EffectFunction>();
            EffectData.Functions.Clear(); // Clears List

            foreach (TreeViewItem tvi in EffectTree_Functions.Items)
                EffectData.Functions.Add((EffectFunction) tvi.Tag); // Adds Function Data to List
        }

        /// <summary>
        ///     Loads the data
        /// </summary>
        public void Load()
        {
            if (EffectData.Functions == null) EffectData.Functions = new List<EffectFunction>();

            EffectTree_Functions.Items.Clear(); // Clears the Function Tree

            foreach (var func in EffectData.Functions)
                EffectFunctions_RegisterFunction(func); // Register Function
        }

        #endregion

        #region Root Tree Functions

        private void RootTreeSetup()
        {
            CM_EffectFunctionHead = new ContextMenu(); // Creates Context Menu Object
            CM_EffectFunctionHead.Opened += CM_EffectFunctionHead_Opened;

            var Create_Effect = new MenuItem(); // Creates Create Menu Item
            Create_Effect.Header = "Create Effect Function"; // Sets Header
            Create_Effect.Click += CM_EffectFunctionHead_Create; // Sets Callback
            CM_EffectFunctionHead.Items.Add(Create_Effect); // Adds to Menu

            CM_EffectFunctionHead.Items.Add(new Separator()); // Adds a Spacer

            var Export_Effect = new MenuItem(); // Creates Create Menu Item
            Export_Effect.Header = "Export All Effect Functions"; // Sets Header
            Export_Effect.Click += CM_EffectFunctionHead_Export; // Sets Callback
            CM_EffectFunctionHead.Items.Add(Export_Effect); // Adds to Menu

            EffectTree.ContextMenu = CM_EffectFunctionHead; // Links CM
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
            var exp = new RAW_JSON();
            exp.Export<List<EffectData>>(null);
            var saved = exp.ShowDialog();

            if (saved == true)
            {
            }
        }

        #endregion

        #region Effect Functions

        private void EffectFunctions_RegisterFunction(EffectFunction Function_Effect = null)
        {
            EffectFunction Function;

            if (Function_Effect == null)
            {
                Function = new EffectFunction(); // Create a new Function
                Function.Function_Name = "New Function"; // Sets a name for the Function
            } // Create the new Function
            else
            {
                Function = Function_Effect;
            } // Set the Functions

            var tvi = new TreeViewItem();
            tvi.Tag = Function;

            var ctxmnu = new ContextMenu();
            ctxmnu.Opened += CM_EffectFunctionItem_Opened;
            ctxmnu.Tag = tvi;

            var editmenu = new MenuItem();
            editmenu.Header = "Edit Function";
            editmenu.Click += CM_EffectFunctionItem_Edit_Click;
            ctxmnu.Items.Add(editmenu);

            var deletemenu = new MenuItem();
            deletemenu.Header = "Delete Function";
            deletemenu.Click += CM_EffectFunctionItem_Delete_Click;
            ctxmnu.Items.Add(deletemenu);

            ctxmnu.Items.Add(new Separator()); // Adds a Spacer

            var exportmenu = new MenuItem();
            exportmenu.Header = "Export Function";
            exportmenu.Click += CM_EffectFunctionItem_Export_Click;
            ctxmnu.Items.Add(exportmenu);

            tvi.Header = Function.Function_Name;
            tvi.ContextMenu = ctxmnu;

            EffectTree_Functions.Items.Add(tvi);
        }

        private void EffectFunctions_UnRegisterFunction(string Function_Name)
        {
            object Item = null;
            foreach (var obj in EffectTree_Functions.Items)
                if (((TreeViewItem) obj).Header.ToString().ToLower() == Function_Name.ToLower())
                    Item = obj;

            if (Item != null)
            {
                EffectTree_Functions.Items.Remove(Item);
                Item = null;
            }
        }

        private void EffectFunctions_UnRegisterFunction(object Function_Item)
        {
            if (EffectTree_Functions.Items.Contains(Function_Item))
            {
                EffectTree_Functions.Items.Remove(Function_Item);
                Function_Item = null;
            }
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
            var fnc = (EffectFunction) ((TreeViewItem) ((ContextMenu) ((MenuItem) sender).Parent).Tag).Tag;
            var tvi = (TreeViewItem) ((ContextMenu) ((MenuItem) sender).Parent).Tag;

            var design = new EffectFunction_Designer(fnc);
            var display = design.ShowDialog();

            if (display == true)
                try
                {
                    tvi.Header = fnc.Function_Name; // Updates the name
                }
                catch (Exception ex)
                {
                }
        }

        private void CM_EffectFunctionItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            EffectFunctions_UnRegisterFunction(((ContextMenu) ((MenuItem) sender).Parent).Tag);
        }

        private void CM_EffectFunctionItem_Export_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion

        #endregion

        #region Effect Triggers

        public void EffectTriggers_RegisterTrigger()
        {
        }

        public void EffectTriggers_UnRegisterTrigger()
        {
        }

        public void EffectTrigger_GetTrigger()
        {
        }

        #region Context Menu Events

        #endregion

        #endregion
    }
}