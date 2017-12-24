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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssaultBird2454.VPTU.Client.UI.Entities
{
    /// <summary>
    /// Interaction logic for EntitiesList.xaml
    /// </summary>
    public partial class EntitiesList : UserControl
    {
        ContextMenu ctxm_Root;
        List<EntitiesManager.Folder> folders;
        List<EntitiesManager.Entry_Data> entrys;

        public EntitiesList()
        {
            InitializeComponent();
            Create_ContextMenu();
        }

        #region Entities Manager Code
        List<TreeViewItem> EntitiesManager_Folders = new List<TreeViewItem>();
        List<TreeViewItem> EntitiesManager_Entrys = new List<TreeViewItem>();
        ContextMenu EntitiesManager_Root;

        public void UpdateImage(string ID, Bitmap Image)
        {
            EntitiesListItem ELI = (EntitiesListItem)(EntitiesManager_Entrys.Find(x => ((EntitiesManager.Entry_Data)x.Tag).ID == ID).Header);

            ELI.Update(Image);
        }

        public void EntitiesManager_ReloadList(List<EntitiesManager.Folder> _folders, List<EntitiesManager.Entry_Data> _entrys)
        {
            folders = _folders;
            entrys = _entrys;

            EntitiesManager_Folders.Clear();
            EntitiesManager_Entrys.Clear();
            Tree.Items.Clear();

            EntitiesManager_Display();
        }
        public void Create_ContextMenu()
        {
            #region Root
            EntitiesManager_Root = new ContextMenu();
            MenuItem ctxm_Root_CreateFolder = new MenuItem();
            ctxm_Root_CreateFolder.Header = "Create Folder in Root";
            ctxm_Root_CreateFolder.Click += Ctxm_Root_CreateFolder_Click;
            MenuItem ctxm_Root_CreatePokemonEntities = new MenuItem();
            ctxm_Root_CreatePokemonEntities.Header = "Create Pokemon Entities in Root";
            ctxm_Root_CreatePokemonEntities.Click += Ctxm_Root_CreatePokemonEntities_Click;
            MenuItem ctxm_Root_CreateTrainerEntities = new MenuItem();
            ctxm_Root_CreateTrainerEntities.Header = "Create Trainer Entities in Root";
            ctxm_Root_CreateTrainerEntities.Click += Ctxm_Root_CreateTrainerEntities_Click;
            ctxm_Root_CreateTrainerEntities.IsEnabled = false;

            EntitiesManager_Root.Items.Add(ctxm_Root_CreateFolder);
            EntitiesManager_Root.Items.Add(ctxm_Root_CreatePokemonEntities);
            EntitiesManager_Root.Items.Add(ctxm_Root_CreateTrainerEntities);
            Tree.ContextMenu = EntitiesManager_Root;
            #endregion
        }

        #region Context Menu Events
        private void Ctxm_Entities_Delete_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntitiesManager_DeleteEntry(((EntitiesManager.Entry_Data)ctxm.Tag).ID);
        }
        private void Ctxm_Entities_Duplicate_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);
        }
        private void Ctxm_Entities_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);
                EntitiesManager.Entry_Data data = entrys.Find(x => x.ID == ((EntitiesManager.Entry_Data)ctxm.Tag).ID);
                if (data.Entities_Type == EntitiesManager.Entities_Type.Pokemon)
                {
                    Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Entities.Entities_Pokemon_Get()// Gets the Pokemon Selected
                    {
                        ID = data.ID// Sets the Pokemon ID to get
                    });
                    //Program.MainWindow.CharacterSheet_List("");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Ctxm_Folder_Delete_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntitiesManager_DeleteDir(((EntitiesManager.Folder)ctxm.Tag).ID);
        }
        private void Ctxm_Folder_CreatePokemonEntities_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntitiesManager_CreatePokemonEntities(((EntitiesManager.Folder)ctxm.Tag).ID);
        }
        private void Ctxm_Folder_CreateTrainerEntities_Click(object sender, RoutedEventArgs e)
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
                EntitiesManager_CreateDir(SP.Input, ((EntitiesManager.Folder)ctxm.Tag).ID);
            }
        }
        private void Ctxm_Root_CreatePokemonEntities_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);

            EntitiesManager_CreatePokemonEntities();
        }
        private void Ctxm_Root_CreateTrainerEntities_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);
        }
        private void Ctxm_Root_CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            UI.String_Prompt SP = new UI.String_Prompt("Folder Name");
            bool? Pass = SP.ShowDialog();

            if (Pass == true)
            {
                EntitiesManager_CreateDir(SP.Input);
            }
        }
        #endregion

        public void EntitiesManager_CreateDir(string Name, string Parent = null)
        {
            EntitiesManager.Folder folder = new EntitiesManager.Folder()
            {
                ID = RNG.Generators.RSG.GenerateString(16),
                Name = Name,
                Parent = Parent
            };

            // Send Command

            #region Context Menu
            ContextMenu EntitiesManager_Folder = new ContextMenu();
            EntitiesManager_Folder.Tag = folder;
            MenuItem ctxm_Folder_CreateFolder = new MenuItem();
            ctxm_Folder_CreateFolder.Header = "Create Folder";
            ctxm_Folder_CreateFolder.Click += Ctxm_Folder_CreateFolder_Click;
            MenuItem ctxm_Folder_CreatePokemonEntities = new MenuItem();
            ctxm_Folder_CreatePokemonEntities.Header = "Create Pokemon Entities";
            ctxm_Folder_CreatePokemonEntities.Click += Ctxm_Folder_CreatePokemonEntities_Click;
            MenuItem ctxm_Folder_CreateTrainerEntities = new MenuItem();
            ctxm_Folder_CreateTrainerEntities.Header = "Create Trainer Entities";
            ctxm_Folder_CreateTrainerEntities.Click += Ctxm_Folder_CreateTrainerEntities_Click;
            ctxm_Folder_CreateTrainerEntities.IsEnabled = false;
            Separator ctxm_Folder_S1 = new Separator();
            MenuItem ctxm_Folder_Delete = new MenuItem();
            ctxm_Folder_Delete.Header = "Delete";
            ctxm_Folder_Delete.Click += Ctxm_Folder_Delete_Click;

            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreateFolder);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreatePokemonEntities);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreateTrainerEntities);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_S1);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_Delete);
            #endregion

            if (Parent != null)
            {
                TreeViewItem ParentTVI = EntitiesManager_Folders.Find(x => ((EntitiesManager.Folder)x.Tag).ID == Parent);
                TreeViewItem TVI = new TreeViewItem()
                {
                    Header = folder.Name,
                    Tag = folder,
                    ContextMenu = EntitiesManager_Folder
                };

                folders.Add(folder);
                ParentTVI.Items.Add(TVI);
                EntitiesManager_Folders.Add(TVI);
            }
            else
            {
                TreeViewItem TVI = new TreeViewItem()
                {
                    Header = folder.Name,
                    Tag = folder.ID,
                    ContextMenu = EntitiesManager_Folder
                };

                folders.Add(folder);
                Tree.Items.Add(TVI);
                EntitiesManager_Folders.Add(TVI);
            }
        }
        public void EntitiesManager_DeleteDir(string ID)
        {
            foreach (EntitiesManager.Folder ChildFolder in folders.FindAll(x => x.Parent == ID))
            {
                EntitiesManager_DeleteDir(ChildFolder.ID);
            }
            foreach (EntitiesManager.Entry_Data entry in entrys.FindAll(x => x.Parent_Folder == ID && x.Entities_Type == EntitiesManager.Entities_Type.Trainer))
            {
                EntitiesManager_DeleteEntry(entry.ID);
            }
            foreach (EntitiesManager.Entry_Data entry in entrys.FindAll(x => x.Parent_Folder == ID && x.Entities_Type == EntitiesManager.Entities_Type.Pokemon))
            {
                EntitiesManager_DeleteEntry(entry.ID);
            }

            EntitiesManager.Folder Folder = folders.Find(x => x.ID == ID);

            TreeViewItem TVI = EntitiesManager_Folders.Find(x => ((EntitiesManager.Folder)x.Tag).ID == Folder.ID);
            if (Folder.Parent == null)
            {
                Tree.Items.Remove(TVI);
            }
            else
            {
                TreeViewItem ParentTVI = EntitiesManager_Folders.Find(x => ((EntitiesManager.Folder)x.Tag).ID == Folder.Parent);
                ParentTVI.Items.Remove(TVI);
            }

            EntitiesManager_Folders.Remove(TVI);
            folders.Remove(Folder);
        }
        public void EntitiesManager_DeleteEntry(string ID)
        {
            EntitiesManager.Entry_Data Entry = (EntitiesManager.Entry_Data)EntitiesManager_Entrys.Find(x => ((EntitiesManager.Entry_Data)x.Tag).ID == ID).Tag;

            entrys.Remove(Entry);
            if (Entry.Entities_Type == EntitiesManager.Entities_Type.Pokemon)
            {
                try
                {
                    // Send Command
                }
                catch { /* Dont Care */ }
            }
            else if (Entry.Entities_Type == EntitiesManager.Entities_Type.Trainer)
            {
                try
                {
                    // Send Command
                }
                catch { /* Dont Care */ }
            }

            TreeViewItem TVI = EntitiesManager_Entrys.Find(x => ((EntitiesManager.Entry_Data)x.Tag).ID == ID);
            if (Entry.Parent_Folder == null)
            {
                Tree.Items.Remove(TVI);
            }
            else
            {
                TreeViewItem Parent = EntitiesManager_Folders.Find(x => ((EntitiesManager.Folder)x.Tag).ID == Entry.Parent_Folder);
                Parent.Items.Remove(TVI);
            }

            EntitiesManager_Entrys.Remove(TVI);
        }
        private void EntitiesManager_DisplayFolder(EntitiesManager.Folder folder)
        {
            #region Context Menu
            ContextMenu EntitiesManager_Folder = new ContextMenu();
            EntitiesManager_Folder.Tag = folder;
            MenuItem ctxm_Folder_CreateFolder = new MenuItem();
            ctxm_Folder_CreateFolder.Header = "Create Folder";
            ctxm_Folder_CreateFolder.Click += Ctxm_Folder_CreateFolder_Click;
            MenuItem ctxm_Folder_CreatePokemonEntities = new MenuItem();
            ctxm_Folder_CreatePokemonEntities.Header = "Create Pokemon Entities";
            ctxm_Folder_CreatePokemonEntities.Click += Ctxm_Folder_CreatePokemonEntities_Click;
            MenuItem ctxm_Folder_CreateTrainerEntities = new MenuItem();
            ctxm_Folder_CreateTrainerEntities.Header = "Create Trainer Entities";
            ctxm_Folder_CreateTrainerEntities.Click += Ctxm_Folder_CreateTrainerEntities_Click;
            ctxm_Folder_CreateTrainerEntities.IsEnabled = false;
            Separator ctxm_Folder_S1 = new Separator();
            MenuItem ctxm_Folder_Delete = new MenuItem();
            ctxm_Folder_Delete.Header = "Delete";
            ctxm_Folder_Delete.Click += Ctxm_Folder_Delete_Click;

            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreateFolder);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreatePokemonEntities);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreateTrainerEntities);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_S1);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_Delete);
            #endregion

            TreeViewItem Parent = EntitiesManager_Folders.Find(x => ((EntitiesManager.Folder)x.Tag).ID == folder.Parent);

            TreeViewItem Child = new TreeViewItem()
            {
                Header = folder.Name,
                Tag = folder,
                ContextMenu = EntitiesManager_Folder
            };

            if (Parent == null)
            {
                Tree.Items.Add(Child);
            }
            else
            {
                Parent.Items.Add(Child);
            }
            EntitiesManager_Folders.Add(Child);

            EntitiesManager_Display(folder.ID);
        }

        public void EntitiesManager_CreatePokemonEntities(string Folder = null)
        {
            //UI.Entities.Pokemon_Character pc = new UI.Entities.Pokemon_Character(SaveManager);
            //pc.PokemonData.Parent_Folder = Folder;
            //pc.ShowDialog();

            //SaveManager.SaveData.Pokemon.Add(pc.PokemonData);

            //EntitiesManager_DisplayEntry(pc.PokemonData.EntryData);
        }
        public void EntitiesManager_EditPokemonEntities(EntitiesManager.Pokemon.PokemonCharacter Pokemon)
        {
            //UI.Entities.Pokemon_Character pc = new UI.Entities.Pokemon_Character(SaveManager, Pokemon);
            //pc.ShowDialog();

            //TreeViewItem TVI = EntitiesManager_Entrys.Find(x => ((EntitiesManager.Entry_Data)x.Tag).ID == Pokemon.ID);
            //UI.Entities.EntitiesListItem ELI = (UI.Entities.EntitiesListItem)TVI.Header;

            //ELI.Update(SaveManager.LoadImage(Pokemon.Token_ResourceID), Pokemon.Name, new List<KeyValuePair<System.Windows.Media.Color, string>>());
        }
        private void EntitiesManager_DisplayEntry(EntitiesManager.Entry_Data entry)
        {
            UI.Entities.EntitiesListItem ELI = new UI.Entities.EntitiesListItem();
            ELI.Update(entry.Name, new List<KeyValuePair<System.Windows.Media.Color, string>>());

            #region Context Menu
            ContextMenu EntitiesManager_Entities = new ContextMenu();
            EntitiesManager_Entities.Tag = entry;
            MenuItem ctxm_Entities_Edit = new MenuItem();
            ctxm_Entities_Edit.Header = "View / Edit";
            ctxm_Entities_Edit.Click += Ctxm_Entities_Edit_Click;
            MenuItem ctxm_Entities_Duplicate = new MenuItem();
            ctxm_Entities_Duplicate.Header = "Duplicate";
            ctxm_Entities_Duplicate.Click += Ctxm_Entities_Duplicate_Click;
            ctxm_Entities_Duplicate.IsEnabled = false;
            Separator ctxm_Entities_S1 = new Separator();
            MenuItem ctxm_Entities_Delete = new MenuItem();
            ctxm_Entities_Delete.Header = "Delete";
            ctxm_Entities_Delete.Click += Ctxm_Entities_Delete_Click;

            EntitiesManager_Entities.Items.Add(ctxm_Entities_Edit);
            EntitiesManager_Entities.Items.Add(ctxm_Entities_Duplicate);
            EntitiesManager_Entities.Items.Add(ctxm_Entities_S1);
            EntitiesManager_Entities.Items.Add(ctxm_Entities_Delete);
            #endregion

            TreeViewItem TVI = new TreeViewItem()
            {
                Header = ELI,
                Tag = entry,
                ContextMenu = EntitiesManager_Entities
            };

            if (entry.Parent_Folder == null)
            {
                Tree.Items.Add(TVI);
            }
            else
            {
                TreeViewItem Parent = EntitiesManager_Folders.Find(x => ((EntitiesManager.Folder)x.Tag).ID == entry.Parent_Folder);
                Parent.Items.Add(TVI);
            }
            EntitiesManager_Entrys.Add(TVI);

            Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Resources.ImageResource
            {
                UseCommand = "Entities_List",
                UseID = entry.ID,
                Resource_ID = entry.Token_ResourceID
            });// Retrieves the Image
        }

        private void EntitiesManager_Display(string ParentID = null)
        {
            TreeViewItem Child;

            foreach (EntitiesManager.Entry_Data entry in entrys.FindAll(x => x.Parent_Folder == ParentID))
            {
                EntitiesManager_DisplayEntry(entry);
            }

            foreach (EntitiesManager.Folder folder in folders.FindAll(x => x.Parent == ParentID))
            {
                EntitiesManager_DisplayFolder(folder);
            }
        }
        #endregion

        private void ToolBar_Reload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Program.ClientInstance.Client.SendData(new Server.Instances.CommandData.Entities.Entities_All_GetList());
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You need to connect to a running server first before you can load this list!");
            }
        }
    }
}
