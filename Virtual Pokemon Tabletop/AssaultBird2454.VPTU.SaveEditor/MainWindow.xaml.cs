using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace AssaultBird2454.VPTU.SaveEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Form Code
        public static SaveManager.SaveManager SaveManager;
        public ProjectInfo VersioningInfo;
        /// <summary>
        /// Assembly Directory
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }

        public MainWindow()
        {
            try
            {
                if (File.Exists(AssemblyDirectory + "\\SaveEditor.pid"))
                {
                    if (Process.GetProcessById(Convert.ToInt32(File.ReadAllText(AssemblyDirectory + "\\SaveEditor.pid"))).ProcessName == Process.GetCurrentProcess().ProcessName)
                    {
                        MessageBox.Show("Process Already Running!");
                        this.Close();
                        return;
                    }
                    else
                    {
                        File.Delete(AssemblyDirectory + "\\SaveEditor.pid");
                    }
                }

                File.WriteAllText(AssemblyDirectory + "\\SaveEditor.pid", Process.GetCurrentProcess().Id.ToString());
            }
            catch
            {

            }

            InitializeComponent();
            Create_EntityManager_ContextMenu();

            #region Versioning Info
            using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("AssaultBird2454.VPTU.SaveEditor.ProjectVariables.json"))
            {
                using (StreamReader read = new StreamReader(str))
                {
                    VersioningInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                    this.Title = "Virtual Pokemon Tabletop - SaveEditor (Version: " + VersioningInfo.Version + ") (Commit: " + VersioningInfo.Compile_Commit.Remove(7) + ")";
                }
            }
            #endregion

            Setup();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            File.Delete(AssemblyDirectory + "\\SaveEditor.pid");
        }
        private void Menu_About_Licence_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream(AssemblyDirectory + "/LICENSE", FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        UI.About.License lic = new UI.About.License(sr.ReadToEnd(), "License");
                        lic.ShowDialog();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The \"LICENSE\" File was not found when trying to read it! The \"LICENSE\" file is avalable on GitHub.", "License File Missing");
            }
        }
        private void Menu_About_LegalNotices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream(AssemblyDirectory + "/LEGAL NOTICE", FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        UI.About.License lic = new UI.About.License(sr.ReadToEnd(), "Legal Notices");
                        lic.ShowDialog();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The \"LEGAL NOTICE\" File was not found when trying to read it! The \"LEGAL NOTICE\" file is avalable on GitHub.", "License File Missing");
            }
        }

        #region Setup Code
        private void Setup()
        {

        }
        #endregion
        #endregion

        #region Save Manager Related Code
        #region Triggering Events
        //When The "Open File" Button is clicked
        private void Menu_Menu_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.Multiselect = false;
            openFile.Title = "Open Virtual PTU Save File";
            openFile.DefaultExt = ".ptu";

            openFile.FileOk += new System.ComponentModel.CancelEventHandler((object obj, System.ComponentModel.CancelEventArgs args) =>
            {
                Load(openFile.FileName);
            });

            openFile.ShowDialog();
        }
        //When The "Save File" Button is clicked
        private void Menu_Menu_Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        //When The "Save File As" Button is clicked
        private void Menu_Menu_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature is not currently working...");
        }
        #endregion

        /// <summary>
        /// Saves the Save Data to Save File
        /// </summary>
        public void Save()
        {
            if (SaveManager == null)
            {
                MessageBox.Show("No File is open... Please open a file before saving");
                return;
            }

            SaveManager.Save_SaveData();
        }
        /// <summary>
        /// Loads a Save File
        /// </summary>
        /// <param name="Path">Path to Save file to be opened</param>
        public void Load(string Path)
        {
            SaveManager = new VPTU.SaveManager.SaveManager(Path);
            SaveManager.Load_SaveData();
            this.SaveEditor_TabPanel.IsEnabled = true;

            OverViewSettings_Reload();// Reload Settings and other info
            PokedexManager_ReloadList();//Reload Pokedex List
            ResourceManager_ReloadList();//Reload Resource List
            EntityManager_ReloadList();// Reload Characters List
            UserGroup_Users_Reload();// Reload Users List
        }

        #region Save Data Tools
        private void Menu_SaveTools_UnPack_Click(object sender, RoutedEventArgs e)
        {
            if (SaveManager != null)
            {
                MessageBoxResult mbr = MessageBox.Show("To Un-Pack the open Save Data File, The data needs to be saved...\nSave and Un-Pack?", "Select Save to Un-Pack", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (mbr == MessageBoxResult.Yes)
                {
                    Save();// Save the Save File

                    string DataPath = Path.GetDirectoryName(SaveManager.SaveFileDir) + @"\" + Path.GetFileName(SaveManager.SaveFileDir).Split('.')[0];// Gets the path to extract the save data

                    if (Directory.Exists(DataPath))// Checks if path exists
                    {
                        //If Path Exists, Notify the user that the data will be deleted and offer the ability to acknoledge or cancel the opperation
                        MessageBoxResult mbed = MessageBox.Show("The Path that the save data will be extracted to already exists...\nAll Data in the folder will be deleted!\n\nDirectory: " + DataPath, "Path Exists", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                        if (mbed == MessageBoxResult.Cancel)
                        {
                            //Cancel the opperation
                            MessageBox.Show("Un-Packing Save Data was canceled and no files have been changed.\n\nReasion: Canceled By User", "Un-Pack Save Data Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        else if (mbed == MessageBoxResult.OK)
                        {
                            //Delete the Directory and files, then continue
                            Directory.Delete(DataPath, true);
                        }
                    }

                    VPTU.SaveManager.SaveFileConverter.Extract_Save(SaveManager.SaveFileDir, DataPath);// Extract the data to the Directory

                    //Ask if the user wants the folder to be opened...
                    MessageBoxResult mbr2 = MessageBox.Show("Un-Packing Complete!\nOpen Save Data?\n\nPath to Data: " + DataPath, "Un-Pack Complete", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                    if (mbr2 == MessageBoxResult.Yes)
                    {
                        //Open the directory
                        Process.Start("Explorer.exe", DataPath);
                    }
                }
            }
            else
            {
                //No Save file open, Cant Un-Pack Anything
                MessageBox.Show("You cant Un-Pack nothing!\nOpen a Save File and try again", "Un-Packing Save Data");
            }
        }
        private void Menu_SaveTools_Pack_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();// creates a FolderBrowser dialog
            System.Windows.Forms.DialogResult dr = fbd.ShowDialog();// Shows the dialog to select the path containing save data

            if (dr == System.Windows.Forms.DialogResult.Cancel)// Check if Pack Opperation was canceled
            {
                MessageBox.Show("Packing Save Data was canceled and no files have been changed.\n\nReasion: Canceled By User", "Pack Save Data Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string DataPath = fbd.SelectedPath;// Gets the path to the the save data to pack from
            string SavePath = Directory.GetParent(DataPath) + @"\Packed Pokemon Tabletop Save.ptu";

            if (!Directory.Exists(DataPath))// Checks if path does not exist
            {
                //If Path does not exist, Notify the user that the selected path does not exist
                MessageBoxResult mbed = MessageBox.Show("The Path specified does not exist...\n\nSelected Directory: " + DataPath, "Path does not Exist", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            VPTU.SaveManager.SaveFileConverter.Compress_Save(SavePath, DataPath);// Pack the data to the a file

            //Ask if the user wants the folder to be opened...
            MessageBoxResult mbr2 = MessageBox.Show("Packing Complete!\nOpen SaveFile?\n\nPath to File: " + SavePath, "Packing Complete", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
            if (mbr2 == MessageBoxResult.Yes)
            {
                //Open the file
                Load(SavePath);
            }
        }
        #endregion
        #endregion

        #region Pokedex Manager Code
        #region Pokedex Manager Variables
        Thread PokedexSearchThread;
        #endregion

        #region Right SideBar Events
        //When The "Add Pokemon" Button is clicked
        private void PokedexManager_AddDex_Pokemon_Click(object sender, RoutedEventArgs e)
        {
            UI.Pokedex.Pokemon pokemon = new UI.Pokedex.Pokemon(SaveManager.SaveData.PokedexData);// Creates Pokemon Editor Page
            bool? OK = pokemon.ShowDialog();// Shows the dialog, waits for return

            if (OK == true)// When Return
            {
                SaveManager.SaveData.PokedexData.Pokemon.Add(pokemon.PokemonData);// Add Pokemon to List
                PokedexManager_ReloadList();// Reload Pokedex List
            }
        }
        //When The "Add Move" Button is clicked
        private void PokedexManager_AddDex_Move_Click(object sender, RoutedEventArgs e)
        {
            UI.Pokedex.Moves move = new UI.Pokedex.Moves(SaveManager.SaveData);// Creates Move Editor Page
            bool? OK = move.ShowDialog();// Shows the Dialog, waits for return

            if (OK == true)// When Return
            {
                SaveManager.SaveData.PokedexData.Moves.Add(move.MoveData);// Add Move to List
                PokedexManager_ReloadList();// Reload Pokedex List
            }
        }
        //When The "Add Ability" Button is clicked
        private void PokedexManager_AddDex_Ability_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature not Available for that Data Type.");
        }
        //When The "Add Item" Button is clicked
        private void PokedexManager_AddDex_Items_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature not Available for that Data Type.");
        }
        //When The "Edit" Button is clicked
        private void PokedexManager_ManageDex_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditSelected_Pokedex();
        }
        //When an item on the list is double clicked
        private void PokedexManager_List_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EditSelected_Pokedex();
        }
        //When The "Delete" Button is clicked
        private void PokedexManager_ManageDex_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((PokedexList_DataBind)PokedexManager_List.SelectedValue).DataType == PokedexList_DataType.Pokemon)
                {
                    Pokedex.Pokemon.PokemonData Data = (Pokedex.Pokemon.PokemonData)((PokedexList_DataBind)PokedexManager_List.SelectedValue).DataTag;
                    SaveManager.SaveData.PokedexData.Pokemon.Remove(Data);

                    Data.Dispose();
                    Data = null;
                }
                else if (((PokedexList_DataBind)PokedexManager_List.SelectedValue).DataType == PokedexList_DataType.Move)
                {
                    Pokedex.Moves.MoveData Data = (Pokedex.Moves.MoveData)((PokedexList_DataBind)PokedexManager_List.SelectedValue).DataTag;
                    SaveManager.SaveData.PokedexData.Moves.Remove(Data);

                    #region Remove Links to the move
                    #region Pokemon
                    foreach (VPTU.Pokedex.Pokemon.PokemonData pokemon in SaveManager.SaveData.PokedexData.Pokemon)
                    {
                        if (pokemon.Moves == null)
                        {
                            pokemon.Moves = new List<Pokedex.Pokemon.Link_Moves>();
                            continue;
                        }
                        List<VPTU.Pokedex.Pokemon.Link_Moves> moves = pokemon.Moves.FindAll(x => x.MoveName.ToLower() == Data.Name.ToLower());
                        foreach (VPTU.Pokedex.Pokemon.Link_Moves move in moves)
                        {
                            pokemon.Moves.Remove(move);
                        }
                    }
                    #endregion
                    #endregion

                    Data.Dispose();
                    Data = null;
                }

                PokedexManager_ReloadList();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You cant delete nothing! or can you?");
            }
        }
        //When The Search Box has been changed
        private void PokedexManager_SearchDex_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        #endregion

        #region List Events
        private void PokedexManager_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //All of these events will reload the Pokedex List
        private void PokedexManager_SearchDex_Pokemon_Checked(object sender, RoutedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        private void PokedexManager_SearchDex_Pokemon_Unchecked(object sender, RoutedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        private void PokedexManager_SearchDex_Moves_Checked(object sender, RoutedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        private void PokedexManager_SearchDex_Moves_Unchecked(object sender, RoutedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        private void PokedexManager_SearchDex_Abilitys_Checked(object sender, RoutedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        private void PokedexManager_SearchDex_Abilitys_Unchecked(object sender, RoutedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        private void PokedexManager_SearchDex_Items_Checked(object sender, RoutedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        private void PokedexManager_SearchDex_Items_Unchecked(object sender, RoutedEventArgs e)
        {
            PokedexManager_ReloadList();
        }
        #endregion

        /// <summary>
        /// Reloads the Pokedex list
        /// </summary>
        public void PokedexManager_ReloadList()
        {
            try
            {
                PokedexSearchThread.Abort();
                PokedexSearchThread = null;
            }
            catch { }

            PokedexSearchThread = new Thread(new ThreadStart(() =>
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        PokedexManager_List.Items.Clear();

                        if (SaveManager == null) { return; }

                        if (PokedexManager_SearchDex_Pokemon.IsChecked == true)
                        {
                            foreach (Pokedex.Pokemon.PokemonData Pokemon in SaveManager.SaveData.PokedexData.Pokemon)
                            {
                                if (Pokemon.Species_Name.ToLower().Contains(PokedexManager_SearchDex_Search.Text.ToLower()))
                                {
                                    PokedexList_DataBind PokemonDB = new PokedexList_DataBind();
                                    PokemonDB.Name = Pokemon.Species_Name;
                                    PokemonDB.ID = Pokemon.Species_DexID;
                                    PokemonDB.Class = "";
                                    PokemonDB.EntryType = "Pokemon";
                                    //PokemonDB.Type = "";
                                    PokemonDB.DataType = PokedexList_DataType.Pokemon;
                                    PokemonDB.DataTag = Pokemon;

                                    PokedexManager_List.Items.Add(PokemonDB);
                                }
                            }
                        }

                        if (PokedexManager_SearchDex_Moves.IsChecked == true)
                        {
                            foreach (Pokedex.Moves.MoveData Move in SaveManager.SaveData.PokedexData.Moves)
                            {
                                if (Move.Name.ToLower().Contains(PokedexManager_SearchDex_Search.Text.ToLower()))
                                {
                                    PokedexList_DataBind MoveDB = new PokedexList_DataBind();
                                    MoveDB.Name = Move.Name;
                                    MoveDB.ID = (SaveManager.SaveData.PokedexData.Moves.IndexOf(Move) + 1);
                                    //MoveDB.Type = Move.Move_Type.ToString();
                                    MoveDB.Class = Move.Move_Class.ToString();
                                    MoveDB.EntryType = "Move";

                                    MoveDB.DataType = PokedexList_DataType.Move;
                                    MoveDB.DataTag = Move;

                                    PokedexManager_List.Items.Add(MoveDB);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error occured while loading the Pokedex Manager's List!\n\n" + ex.ToString(), "Pokedex Manager - Loading Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    }
                }));
            }));
            PokedexSearchThread.IsBackground = true;
            PokedexSearchThread.Start();
        }

        /// <summary>
        /// Opens the edit page for the selected item in the pokedex panel
        /// </summary>
        public void EditSelected_Pokedex()
        {
            try
            {
                //Edit Pokemon Here!
                if (((PokedexList_DataBind)PokedexManager_List.SelectedValue).DataType == PokedexList_DataType.Pokemon)
                {
                    Pokedex.Pokemon.PokemonData Data = (Pokedex.Pokemon.PokemonData)((PokedexList_DataBind)PokedexManager_List.SelectedValue).DataTag;// Gets the Data
                    UI.Pokedex.Pokemon pokemon = new UI.Pokedex.Pokemon(SaveManager.SaveData.PokedexData, Data);// Creates a new window
                    pokemon.ShowDialog();// Shows the window

                    PokedexManager_ReloadList();// Updates the list
                }
                //Edit Moves Here!
                else if (((PokedexList_DataBind)PokedexManager_List.SelectedValue).DataType == PokedexList_DataType.Move)
                {
                    Pokedex.Moves.MoveData Data = (Pokedex.Moves.MoveData)((PokedexList_DataBind)PokedexManager_List.SelectedItem).DataTag;// Gets the Data
                    UI.Pokedex.Moves move = new UI.Pokedex.Moves(SaveManager.SaveData, Data);// Creates a new window
                    move.ShowDialog();// Shows the window

                    PokedexManager_ReloadList();// Updates the list
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You cant edit nothing! or can you?");
            }
        }
        #endregion

        #region Resource Manager Code
        #region Resource Variables
        Thread ResourceSearchThread;
        #endregion
        #region Right SideBar Events
        /// <summary>
        /// Add Audio Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceManager_AddRes_Audio_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Add Image Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceManager_AddRes_Images_Click(object sender, RoutedEventArgs e)
        {
            UI.Resources.Import_ImageResource imp = new UI.Resources.Import_ImageResource();
            bool? pass = imp.ShowDialog();

            if (pass == true)
            {
                //Update
                ResourceManager_ReloadList();
            }
        }
        /// <summary>
        /// Edit Resource Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceManager_ManageRes_Edit_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Delete Resource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceManager_ManageRes_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ResourceManager_List.SelectedItem != null)
            {
                ResourceManager_List.Items.Remove(ResourceManager_List.SelectedItem);
            }
        }

        private void ResourceManager_SearchRes_Images_Checked(object sender, RoutedEventArgs e)
        {
            ResourceManager_ReloadList();
        }
        private void ResourceManager_SearchRes_Audio_Checked(object sender, RoutedEventArgs e)
        {
            ResourceManager_ReloadList();
        }
        private void ResourceManager_SearchRes_Audio_Unchecked(object sender, RoutedEventArgs e)
        {
            ResourceManager_ReloadList();
        }
        private void ResourceManager_SearchRes_Images_Unchecked(object sender, RoutedEventArgs e)
        {
            ResourceManager_ReloadList();
        }

        /// <summary>
        /// Search for names that contain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResourceManager_SearchRes_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ResourceSearchThread.Abort();
                ResourceSearchThread = null;
            }
            catch { }

            ResourceSearchThread = new Thread(new ThreadStart(new Action(() =>
            {
                ResourceManager_List.Dispatcher.Invoke(new Action(() => ResourceManager_ReloadList()));
            })));
            ResourceSearchThread.IsBackground = true;
            ResourceSearchThread.Start();
        }
        #endregion

        /// <summary>
        /// Reloads the Resource List
        /// </summary>
        public void ResourceManager_ReloadList()
        {
            try
            {
                ResourceManager_List.Items.Clear();

                if (ResourceManager_SearchRes_Images.IsChecked == true)
                {
                    foreach (VPTU.SaveManager.Resource_Data.Resources res in SaveManager.SaveData.ImageResources)
                    {
                        if (res.Name.ToLower().Contains(ResourceManager_SearchRes_Search.Text.ToLower()))
                        {
                            ResourceManager_List.Items.Add(res);
                        }
                    }
                }

            }
            catch { }
        }
        #endregion

        #region Entity Manager Code
        List<TreeViewItem> EntityManager_Folders = new List<TreeViewItem>();
        List<TreeViewItem> EntityManager_Entrys = new List<TreeViewItem>();
        ContextMenu EntityManager_Root;

        public void EntityManager_ReloadList()
        {
            EntityManager_Folders.Clear();
            EntityManager_Entrys.Clear();
            EntityManager_Tree.Items.Clear();

            EntityManager_Display();
        }
        public void Create_EntityManager_ContextMenu()
        {
            #region Root
            EntityManager_Root = new ContextMenu();
            MenuItem ctxm_Root_CreateFolder = new MenuItem();
            ctxm_Root_CreateFolder.Header = "Create Folder in Root";
            ctxm_Root_CreateFolder.Click += Ctxm_Root_CreateFolder_Click;
            MenuItem ctxm_Root_CreatePokemonEntity = new MenuItem();
            ctxm_Root_CreatePokemonEntity.Header = "Create Pokemon Entity in Root";
            ctxm_Root_CreatePokemonEntity.Click += Ctxm_Root_CreatePokemonEntity_Click;
            MenuItem ctxm_Root_CreateTrainerEntity = new MenuItem();
            ctxm_Root_CreateTrainerEntity.Header = "Create Trainer Entity in Root";
            ctxm_Root_CreateTrainerEntity.Click += Ctxm_Root_CreateTrainerEntity_Click;
            ctxm_Root_CreateTrainerEntity.IsEnabled = false;

            EntityManager_Root.Items.Add(ctxm_Root_CreateFolder);
            EntityManager_Root.Items.Add(ctxm_Root_CreatePokemonEntity);
            EntityManager_Root.Items.Add(ctxm_Root_CreateTrainerEntity);
            EntityManager_Tree.ContextMenu = EntityManager_Root;
            #endregion
        }

        #region Context Menu Events
        private void Ctxm_Entity_Delete_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntityManager_DeleteEntry(((EntityManager.Entry_Data)ctxm.Tag).ID);
        }
        private void Ctxm_Entity_Duplicate_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);
        }
        private void Ctxm_Entity_Edit_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntityManager_EditPokemonEntity(SaveManager.SaveData.Pokemon.Find(x => x.ID == ((EntityManager.Entry_Data)ctxm.Tag).ID));
        }
        private void Ctxm_Folder_Delete_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntityManager_DeleteDir(((EntityManager.Folder)ctxm.Tag).ID);
        }
        private void Ctxm_Folder_CreatePokemonEntity_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntityManager_CreatePokemonEntity(((EntityManager.Folder)ctxm.Tag).ID);
        }
        private void Ctxm_Folder_CreateTrainerEntity_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);
        }
        private void Ctxm_Folder_CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            UI.String_Prompt SP = new UI.String_Prompt("Folder Name");
            bool? Pass = SP.ShowDialog();

            if (Pass == true)
            {
                EntityManager_CreateDir(SP.Input, ((EntityManager.Folder)ctxm.Tag).ID);
            }
        }
        private void Ctxm_Root_CreatePokemonEntity_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntityManager_CreatePokemonEntity();
        }
        private void Ctxm_Root_CreateTrainerEntity_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);
        }
        private void Ctxm_Root_CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            UI.String_Prompt SP = new UI.String_Prompt("Folder Name");
            bool? Pass = SP.ShowDialog();

            if (Pass == true)
            {
                EntityManager_CreateDir(SP.Input);
            }
        }
        #endregion

        public void EntityManager_CreateDir(string Name, string Parent = null)
        {
            EntityManager.Folder folder = new EntityManager.Folder()
            {
                ID = RNG.Generators.RSG.GenerateString(16),
                Name = Name,
                Parent = Parent
            };

            #region Context Menu
            ContextMenu EntityManager_Folder = new ContextMenu();
            EntityManager_Folder.Tag = folder;
            MenuItem ctxm_Folder_CreateFolder = new MenuItem();
            ctxm_Folder_CreateFolder.Header = "Create Folder";
            ctxm_Folder_CreateFolder.Click += Ctxm_Folder_CreateFolder_Click;
            MenuItem ctxm_Folder_CreatePokemonEntity = new MenuItem();
            ctxm_Folder_CreatePokemonEntity.Header = "Create Pokemon Entity";
            ctxm_Folder_CreatePokemonEntity.Click += Ctxm_Folder_CreatePokemonEntity_Click;
            MenuItem ctxm_Folder_CreateTrainerEntity = new MenuItem();
            ctxm_Folder_CreateTrainerEntity.Header = "Create Trainer Entity";
            ctxm_Folder_CreateTrainerEntity.Click += Ctxm_Folder_CreateTrainerEntity_Click;
            ctxm_Folder_CreateTrainerEntity.IsEnabled = false;
            Separator ctxm_Folder_S1 = new Separator();
            MenuItem ctxm_Folder_Delete = new MenuItem();
            ctxm_Folder_Delete.Header = "Delete";
            ctxm_Folder_Delete.Click += Ctxm_Folder_Delete_Click;

            EntityManager_Folder.Items.Add(ctxm_Folder_CreateFolder);
            EntityManager_Folder.Items.Add(ctxm_Folder_CreatePokemonEntity);
            EntityManager_Folder.Items.Add(ctxm_Folder_CreateTrainerEntity);
            EntityManager_Folder.Items.Add(ctxm_Folder_S1);
            EntityManager_Folder.Items.Add(ctxm_Folder_Delete);
            #endregion

            if (Parent != null)
            {
                TreeViewItem ParentTVI = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == Parent);
                TreeViewItem TVI = new TreeViewItem()
                {
                    Header = folder.Name,
                    Tag = folder,
                    ContextMenu = EntityManager_Folder
                };

                SaveManager.SaveData.Folders.Add(folder);
                ParentTVI.Items.Add(TVI);
                EntityManager_Folders.Add(TVI);
            }
            else
            {
                TreeViewItem TVI = new TreeViewItem()
                {
                    Header = folder.Name,
                    Tag = folder.ID,
                    ContextMenu = EntityManager_Folder
                };

                SaveManager.SaveData.Folders.Add(folder);
                EntityManager_Tree.Items.Add(TVI);
                EntityManager_Folders.Add(TVI);
            }
        }
        public void EntityManager_DeleteDir(string ID)
        {
            foreach (EntityManager.Folder ChildFolder in SaveManager.SaveData.Folders.FindAll(x => x.Parent == ID))
            {
                EntityManager_DeleteDir(ChildFolder.ID);
            }
            foreach (EntityManager.Entry Trainer in SaveManager.SaveData.Trainers.FindAll(x => x.Parent_Folder == ID))
            {
                EntityManager_DeleteEntry(Trainer.ID);
            }
            foreach (EntityManager.Entry Pokemon in SaveManager.SaveData.Pokemon.FindAll(x => x.Parent_Folder == ID))
            {
                EntityManager_DeleteEntry(Pokemon.ID);
            }

            EntityManager.Folder Folder = SaveManager.SaveData.Folders.Find(x => x.ID == ID);

            TreeViewItem TVI = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == Folder.ID);
            if (Folder.Parent == null)
            {
                EntityManager_Tree.Items.Remove(TVI);
            }
            else
            {
                TreeViewItem ParentTVI = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == Folder.Parent);
                ParentTVI.Items.Remove(TVI);
            }

            EntityManager_Folders.Remove(TVI);
            SaveManager.SaveData.Folders.Remove(Folder);
        }
        public void EntityManager_DeleteEntry(string ID)
        {
            EntityManager.Entry_Data Entry = (EntityManager.Entry_Data)EntityManager_Entrys.Find(x => ((EntityManager.Entry_Data)x.Tag).ID == ID).Tag;

            if (Entry.Entity_Type == EntityManager.Entity_Type.Pokemon)
            {
                try
                {
                    EntityManager.Pokemon.PokemonCharacter pchar = SaveManager.SaveData.Pokemon.Find(x => x.ID == ID);
                    SaveManager.SaveData.Pokemon.Remove(pchar);
                }
                catch { /* Dont Care */ }
            }
            else if (Entry.Entity_Type == EntityManager.Entity_Type.Trainer)
            {
                try
                {
                    EntityManager.Trainer.TrainerCharacter tchar = SaveManager.SaveData.Trainers.Find(x => x.ID == ID);
                    SaveManager.SaveData.Trainers.Remove(tchar);
                }
                catch { /* Dont Care */ }
            }

            TreeViewItem TVI = EntityManager_Entrys.Find(x => ((EntityManager.Entry_Data)x.Tag).ID == ID);
            if (Entry.Parent_Folder == null)
            {
                EntityManager_Tree.Items.Remove(TVI);
            }
            else
            {
                TreeViewItem Parent = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == Entry.Parent_Folder);
                Parent.Items.Remove(TVI);
            }

            EntityManager_Entrys.Remove(TVI);
        }
        private void EntityManager_DisplayFolder(EntityManager.Folder folder)
        {
            #region Context Menu
            ContextMenu EntityManager_Folder = new ContextMenu();
            EntityManager_Folder.Tag = folder;
            MenuItem ctxm_Folder_CreateFolder = new MenuItem();
            ctxm_Folder_CreateFolder.Header = "Create Folder";
            ctxm_Folder_CreateFolder.Click += Ctxm_Folder_CreateFolder_Click;
            MenuItem ctxm_Folder_CreatePokemonEntity = new MenuItem();
            ctxm_Folder_CreatePokemonEntity.Header = "Create Pokemon Entity";
            ctxm_Folder_CreatePokemonEntity.Click += Ctxm_Folder_CreatePokemonEntity_Click;
            MenuItem ctxm_Folder_CreateTrainerEntity = new MenuItem();
            ctxm_Folder_CreateTrainerEntity.Header = "Create Trainer Entity";
            ctxm_Folder_CreateTrainerEntity.Click += Ctxm_Folder_CreateTrainerEntity_Click;
            ctxm_Folder_CreateTrainerEntity.IsEnabled = false;
            Separator ctxm_Folder_S1 = new Separator();
            MenuItem ctxm_Folder_Delete = new MenuItem();
            ctxm_Folder_Delete.Header = "Delete";
            ctxm_Folder_Delete.Click += Ctxm_Folder_Delete_Click;

            EntityManager_Folder.Items.Add(ctxm_Folder_CreateFolder);
            EntityManager_Folder.Items.Add(ctxm_Folder_CreatePokemonEntity);
            EntityManager_Folder.Items.Add(ctxm_Folder_CreateTrainerEntity);
            EntityManager_Folder.Items.Add(ctxm_Folder_S1);
            EntityManager_Folder.Items.Add(ctxm_Folder_Delete);
            #endregion

            TreeViewItem Parent = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == folder.Parent);

            TreeViewItem Child = new TreeViewItem()
            {
                Header = folder.Name,
                Tag = folder,
                ContextMenu = EntityManager_Folder
            };

            if (Parent == null)
            {
                EntityManager_Tree.Items.Add(Child);
            }
            else
            {
                Parent.Items.Add(Child);
            }
            EntityManager_Folders.Add(Child);

            EntityManager_Display(folder.ID);
        }

        public void EntityManager_CreatePokemonEntity(string Folder = null)
        {
            UI.Entity.Pokemon_Character pc = new UI.Entity.Pokemon_Character(SaveManager);
            pc.PokemonData.Parent_Folder = Folder;
            pc.ShowDialog();

            SaveManager.SaveData.Pokemon.Add(pc.PokemonData);

            EntityManager_DisplayEntry(pc.PokemonData.EntryData);
        }
        public void EntityManager_EditPokemonEntity(EntityManager.Pokemon.PokemonCharacter Pokemon)
        {
            List<KeyValuePair<System.Windows.Media.Color, string>> View = new List<KeyValuePair<System.Windows.Media.Color, string>>();
            UI.Entity.Pokemon_Character pc = new UI.Entity.Pokemon_Character(SaveManager, Pokemon);
            pc.ShowDialog();

            foreach(string user in pc.PokemonData.View)
            {
                Authentication_Manager.Data.User UData = SaveManager.SaveData.Users.Find(x => x.UserID == user);

                View.Add(new KeyValuePair<System.Windows.Media.Color, string>(UData.UserColor, UData.IC_Name));
            }

            TreeViewItem TVI = EntityManager_Entrys.Find(x => ((EntityManager.Entry_Data)x.Tag).ID == Pokemon.ID);
            UI.Entity.EntityListItem ELI = (UI.Entity.EntityListItem)TVI.Header;

            ELI.Update(SaveManager.LoadImage(Pokemon.Token_ResourceID), Pokemon.Name, View);
        }
        private void EntityManager_DisplayEntry(EntityManager.Entry_Data entry)
        {
            List<KeyValuePair<System.Windows.Media.Color, string>> View = new List<KeyValuePair<System.Windows.Media.Color, string>>();
            UI.Entity.EntityListItem ELI = new UI.Entity.EntityListItem();

            foreach (string user in entry.View)
            {
                Authentication_Manager.Data.User UData = SaveManager.SaveData.Users.Find(x => x.UserID == user);

               View.Add(new KeyValuePair<System.Windows.Media.Color, string>(UData.UserColor, UData.IC_Name));
            }
            ELI.Update(SaveManager.LoadImage(entry.Token_ResourceID), entry.Name, View);

            #region Context Menu
            ContextMenu EntityManager_Entity = new ContextMenu();
            EntityManager_Entity.Tag = entry;
            MenuItem ctxm_Entity_Edit = new MenuItem();
            ctxm_Entity_Edit.Header = "Edit";
            ctxm_Entity_Edit.Click += Ctxm_Entity_Edit_Click;
            MenuItem ctxm_Entity_Duplicate = new MenuItem();
            ctxm_Entity_Duplicate.Header = "Duplicate";
            ctxm_Entity_Duplicate.Click += Ctxm_Entity_Duplicate_Click;
            ctxm_Entity_Duplicate.IsEnabled = false;
            Separator ctxm_Entity_S1 = new Separator();
            MenuItem ctxm_Entity_Delete = new MenuItem();
            ctxm_Entity_Delete.Header = "Delete";
            ctxm_Entity_Delete.Click += Ctxm_Entity_Delete_Click;

            EntityManager_Entity.Items.Add(ctxm_Entity_Edit);
            EntityManager_Entity.Items.Add(ctxm_Entity_Duplicate);
            EntityManager_Entity.Items.Add(ctxm_Entity_S1);
            EntityManager_Entity.Items.Add(ctxm_Entity_Delete);
            #endregion

            TreeViewItem TVI = new TreeViewItem()
            {
                Header = ELI,
                Tag = entry,
                ContextMenu = EntityManager_Entity
            };

            if (entry.Parent_Folder == null)
            {
                EntityManager_Tree.Items.Add(TVI);
            }
            else
            {
                TreeViewItem Parent = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == entry.Parent_Folder);
                Parent.Items.Add(TVI);
            }
            EntityManager_Entrys.Add(TVI);
        }

        private void EntityManager_Display(string ParentID = null)
        {
            TreeViewItem Child;

            foreach (EntityManager.Pokemon.PokemonCharacter pokemon in SaveManager.SaveData.Pokemon.FindAll(x => x.Parent_Folder == ParentID))
            {
                EntityManager.Entry_Data entry = pokemon.EntryData;
                EntityManager_DisplayEntry(entry);
            }

            foreach (EntityManager.Folder folder in SaveManager.SaveData.Folders.FindAll(x => x.Parent == ParentID))
            {
                EntityManager_DisplayFolder(folder);
            }
        }
        #endregion

        #region Overview and Settings Tab
        #region Basic
        /// <summary>
        /// Loads Campaign Info
        /// </summary>
        public void OverViewSettings_Reload()
        {
            OverViewSettings_Basic_CampaignName.Text = SaveManager.SaveData.Campaign_Data.Campaign_Name;
            OverViewSettings_Basic_GMName.Text = SaveManager.SaveData.Campaign_Data.Campaign_GM_Name;
            OverViewSettings_Basic_Description.Text = SaveManager.SaveData.Campaign_Data.Campaign_Desc;
        }

        private void OverViewSettings_Basic_CampaignName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SaveManager.SaveData.Campaign_Data.Campaign_Name = OverViewSettings_Basic_CampaignName.Text;
            }
            catch { }
        }

        private void OverViewSettings_Basic_GMName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SaveManager.SaveData.Campaign_Data.Campaign_GM_Name = OverViewSettings_Basic_GMName.Text;
            }
            catch { }
        }

        private void OverViewSettings_Basic_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                SaveManager.SaveData.Campaign_Data.Campaign_Desc = OverViewSettings_Basic_Description.Text;
            }
            catch { }
        }
        #endregion
        #region Features

        #endregion
        #region Save File Settings

        #endregion
        #region Users & Groups
        public void UserGroup_Users_Reload()
        {
            OverViewSettings_UsersGroups_UserList.Items.Clear();

            foreach (Authentication_Manager.Data.User User in SaveManager.SaveData.Users)
            {
                OverViewSettings_UsersGroups_UserList.Items.Add(User);
            }
        }
        public void UserGroup_Groups_Reload()
        {
            OverViewSettings_UsersGroups_GroupList.Items.Clear();
        }

        private void OverViewSettings_UsersGroups_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            UI.Users.Users User = new UI.Users.Users();
            bool? Pass = User.ShowDialog();

            SaveManager.SaveData.Users.Add(User.User);
            OverViewSettings_UsersGroups_UserList.Items.Add(User.User);
        }

        private void OverViewSettings_UsersGroups_EditUser_Click(object sender, RoutedEventArgs e)
        {
            UI.Users.Users User = new UI.Users.Users((Authentication_Manager.Data.User)OverViewSettings_UsersGroups_UserList.SelectedItems[0]);
            bool? Pass = User.ShowDialog();

            UserGroup_Users_Reload();
        }
        #endregion
        #endregion

        private void PokedexManager_Export_Pokemon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PokedexManager_Export_Moves_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    /// <summary>
    /// All the types that the pokedex list will display (Used for Pharsing Selections)
    /// </summary>
    public enum PokedexList_DataType { Pokemon, Move, Ability, Item }
    /// <summary>
    /// A Class designed for Data Binding Pokedex Data to the Pokedex List
    /// </summary>
    public class PokedexList_DataBind
    {
        /// <summary>
        /// Creates a new instance of the PokedexList DataBind Object
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_Name"></param>
        /// <param name="_EntryType"></param>
        public PokedexList_DataBind()
        {

        }

        /// <summary>
        /// The ID of the Object
        /// </summary>
        public decimal ID { get; set; }
        /// <summary>
        /// The Name of the Object
        /// </summary>
        public string Name { get; set; }

        public string Type { get; set; }
        /// <summary>
        /// The Class of move
        /// </summary>
        public string Class { get; set; }

        public string EntryType { get; set; }

        public PokedexList_DataType DataType { get; set; }
        public object DataTag { get; set; }
    }
}
