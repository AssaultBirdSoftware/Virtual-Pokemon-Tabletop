using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using AssaultBird2454.VPTU.EntitiesManager;
using AssaultBird2454.VPTU.EntitiesManager.Pokemon;
using AssaultBird2454.VPTU.RNG.Generators;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Entities;
using AssaultBird2454.VPTU.Server.Instances.CommandData.Resources;
using Color = System.Windows.Media.Color;

namespace AssaultBird2454.VPTU.Client.UI.Entities
{
    /// <summary>
    ///     Interaction logic for EntitiesList.xaml
    /// </summary>
    public partial class EntitiesList : UserControl
    {
        private ContextMenu ctxm_Root;
        private List<Entry_Data> entrys;
        private List<Folder> folders;
        private List<Authentication_Manager.Data.User> users;

        public EntitiesList()
        {
            InitializeComponent();
            Create_ContextMenu();
        }

        private void ToolBar_Reload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Program.ClientInstance.Client.SendData(new Entities_All_GetList());
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You need to connect to a running server first before you can load this list!");
            }
        }

        #region Entities Manager Code

        private readonly List<TreeViewItem> EntitiesManager_Folders = new List<TreeViewItem>();
        private readonly List<TreeViewItem> EntitiesManager_Entrys = new List<TreeViewItem>();

        private ContextMenu EntitiesManager_Root;

        public void UpdateImage(string ID, Bitmap Image)
        {
            var ELI = (EntitiesListItem)EntitiesManager_Entrys.Find(x => ((Entry_Data)x.Tag).ID == ID).Header;

            ELI.Update(Image);
        }

        public void EntitiesManager_ReloadList(List<Folder> _folders, List<Entry_Data> _entrys, List<Authentication_Manager.Data.User> _users)
        {
            folders = _folders;
            entrys = _entrys;
            users = _users;

            EntitiesManager_Folders.Clear();
            EntitiesManager_Entrys.Clear();
            Tree.Items.Clear();

            EntitiesManager_Display();
        }

        public void Create_ContextMenu()
        {
            #region Root

            EntitiesManager_Root = new ContextMenu();
            var ctxm_Root_CreateFolder = new MenuItem();
            ctxm_Root_CreateFolder.Header = "Create Folder in Root";
            ctxm_Root_CreateFolder.Click += Ctxm_Root_CreateFolder_Click;
            var ctxm_Root_CreatePokemonEntities = new MenuItem();
            ctxm_Root_CreatePokemonEntities.Header = "Create Pokemon Entities in Root";
            ctxm_Root_CreatePokemonEntities.Click += Ctxm_Root_CreatePokemonEntities_Click;
            var ctxm_Root_CreateTrainerEntities = new MenuItem();
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
            var ctxm = (ContextMenu)((MenuItem)sender).Parent;

            EntitiesManager_DeleteEntry(((Entry_Data)ctxm.Tag).ID);
        }

        private void Ctxm_Entities_Duplicate_Click(object sender, RoutedEventArgs e)
        {
            var ctxm = (ContextMenu)((MenuItem)sender).Parent;
        }

        private void Ctxm_Entities_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ctxm = (ContextMenu)((MenuItem)sender).Parent;
                var data = entrys.Find(x => x.ID == ((Entry_Data)ctxm.Tag).ID);
                if (data.Entities_Type == Entities_Type.Pokemon)
                    Program.ClientInstance.Client.SendData(new Entities_Pokemon_Get // Gets the Pokemon Selected
                    {
                        ID = data.ID // Sets the Pokemon ID to get
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Ctxm_Folder_Delete_Click(object sender, RoutedEventArgs e)
        {
            var ctxm = (ContextMenu)((MenuItem)sender).Parent;

            EntitiesManager_DeleteDir(((Folder)ctxm.Tag).ID);
        }

        private void Ctxm_Folder_CreatePokemonEntities_Click(object sender, RoutedEventArgs e)
        {
            var ctxm = (ContextMenu)((MenuItem)sender).Parent;

            EntitiesManager_CreatePokemonEntities(((Folder)ctxm.Tag).ID);
        }

        private void Ctxm_Folder_CreateTrainerEntities_Click(object sender, RoutedEventArgs e)
        {
            var ctxm = (ContextMenu)((MenuItem)sender).Parent;
        }

        private void Ctxm_Folder_CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            var ctxm = (ContextMenu)((MenuItem)sender).Parent;

            var SP = new String_Prompt("Folder Name");
            var Pass = SP.ShowDialog();

            if (Pass == true)
                EntitiesManager_CreateDir(SP.Input, ((Folder)ctxm.Tag).ID);
        }

        private void Ctxm_Root_CreatePokemonEntities_Click(object sender, RoutedEventArgs e)
        {
            var ctxm = (ContextMenu)((MenuItem)sender).Parent;

            EntitiesManager_CreatePokemonEntities();
        }

        private void Ctxm_Root_CreateTrainerEntities_Click(object sender, RoutedEventArgs e)
        {
            var ctxm = (ContextMenu)((MenuItem)sender).Parent;
        }

        private void Ctxm_Root_CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            var SP = new String_Prompt("Folder Name");
            var Pass = SP.ShowDialog();

            if (Pass == true)
                EntitiesManager_CreateDir(SP.Input);
        }

        #endregion

        public void EntitiesManager_CreateDir(string Name, string Parent = null)
        {
            var folder = new Folder
            {
                ID = RSG.GenerateString(16),
                Name = Name,
                Parent = Parent
            };

            // Send Command

            #region Context Menu

            var EntitiesManager_Folder = new ContextMenu();
            EntitiesManager_Folder.Tag = folder;
            var ctxm_Folder_CreateFolder = new MenuItem();
            ctxm_Folder_CreateFolder.Header = "Create Folder";
            ctxm_Folder_CreateFolder.Click += Ctxm_Folder_CreateFolder_Click;
            var ctxm_Folder_CreatePokemonEntities = new MenuItem();
            ctxm_Folder_CreatePokemonEntities.Header = "Create Pokemon Entities";
            ctxm_Folder_CreatePokemonEntities.Click += Ctxm_Folder_CreatePokemonEntities_Click;
            var ctxm_Folder_CreateTrainerEntities = new MenuItem();
            ctxm_Folder_CreateTrainerEntities.Header = "Create Trainer Entities";
            ctxm_Folder_CreateTrainerEntities.Click += Ctxm_Folder_CreateTrainerEntities_Click;
            ctxm_Folder_CreateTrainerEntities.IsEnabled = false;
            var ctxm_Folder_S1 = new Separator();
            var ctxm_Folder_Delete = new MenuItem();
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
                var ParentTVI = EntitiesManager_Folders.Find(x => ((Folder)x.Tag).ID == Parent);
                var TVI = new TreeViewItem
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
                var TVI = new TreeViewItem
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
            foreach (var ChildFolder in folders.FindAll(x => x.Parent == ID))
                EntitiesManager_DeleteDir(ChildFolder.ID);
            foreach (var entry in entrys.FindAll(x => x.Parent_Folder == ID &&
                                                      x.Entities_Type == Entities_Type.Trainer))
                EntitiesManager_DeleteEntry(entry.ID);
            foreach (var entry in entrys.FindAll(x => x.Parent_Folder == ID &&
                                                      x.Entities_Type == Entities_Type.Pokemon))
                EntitiesManager_DeleteEntry(entry.ID);

            var Folder = folders.Find(x => x.ID == ID);

            var TVI = EntitiesManager_Folders.Find(x => ((Folder)x.Tag).ID == Folder.ID);
            if (Folder.Parent == null)
            {
                Tree.Items.Remove(TVI);
            }
            else
            {
                var ParentTVI = EntitiesManager_Folders.Find(x => ((Folder)x.Tag).ID == Folder.Parent);
                ParentTVI.Items.Remove(TVI);
            }

            EntitiesManager_Folders.Remove(TVI);
            folders.Remove(Folder);
        }

        public void EntitiesManager_DeleteEntry(string ID)
        {
            var Entry = (Entry_Data)EntitiesManager_Entrys.Find(x => ((Entry_Data)x.Tag).ID == ID).Tag;

            entrys.Remove(Entry);
            if (Entry.Entities_Type == Entities_Type.Pokemon)
                try
                {
                    // Send Command
                }
                catch
                {
                    /* Dont Care */
                }
            else if (Entry.Entities_Type == Entities_Type.Trainer)
                try
                {
                    // Send Command
                }
                catch
                {
                    /* Dont Care */
                }

            var TVI = EntitiesManager_Entrys.Find(x => ((Entry_Data)x.Tag).ID == ID);
            if (Entry.Parent_Folder == null)
            {
                Tree.Items.Remove(TVI);
            }
            else
            {
                var Parent = EntitiesManager_Folders.Find(x => ((Folder)x.Tag).ID == Entry.Parent_Folder);
                Parent.Items.Remove(TVI);
            }

            EntitiesManager_Entrys.Remove(TVI);
        }

        private void EntitiesManager_DisplayFolder(Folder folder)
        {
            #region Context Menu

            var EntitiesManager_Folder = new ContextMenu();
            EntitiesManager_Folder.Tag = folder;
            var ctxm_Folder_CreateFolder = new MenuItem();
            ctxm_Folder_CreateFolder.Header = "Create Folder";
            ctxm_Folder_CreateFolder.Click += Ctxm_Folder_CreateFolder_Click;
            var ctxm_Folder_CreatePokemonEntities = new MenuItem();
            ctxm_Folder_CreatePokemonEntities.Header = "Create Pokemon Entities";
            ctxm_Folder_CreatePokemonEntities.Click += Ctxm_Folder_CreatePokemonEntities_Click;
            var ctxm_Folder_CreateTrainerEntities = new MenuItem();
            ctxm_Folder_CreateTrainerEntities.Header = "Create Trainer Entities";
            ctxm_Folder_CreateTrainerEntities.Click += Ctxm_Folder_CreateTrainerEntities_Click;
            ctxm_Folder_CreateTrainerEntities.IsEnabled = false;
            var ctxm_Folder_S1 = new Separator();
            var ctxm_Folder_Delete = new MenuItem();
            ctxm_Folder_Delete.Header = "Delete";
            ctxm_Folder_Delete.Click += Ctxm_Folder_Delete_Click;

            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreateFolder);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreatePokemonEntities);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_CreateTrainerEntities);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_S1);
            EntitiesManager_Folder.Items.Add(ctxm_Folder_Delete);

            #endregion

            var Parent = EntitiesManager_Folders.Find(x => ((Folder)x.Tag).ID == folder.Parent);

            var Child = new TreeViewItem
            {
                Header = folder.Name,
                Tag = folder,
                ContextMenu = EntitiesManager_Folder
            };

            if (Parent == null)
                Tree.Items.Add(Child);
            else
                Parent.Items.Add(Child);
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

        public void EntitiesManager_EditPokemonEntities(PokemonCharacter Pokemon)
        {
            //UI.Entities.Pokemon_Character pc = new UI.Entities.Pokemon_Character(SaveManager, Pokemon);
            //pc.ShowDialog();

            //TreeViewItem TVI = EntitiesManager_Entrys.Find(x => ((EntitiesManager.Entry_Data)x.Tag).ID == Pokemon.ID);
            //UI.Entities.EntitiesListItem ELI = (UI.Entities.EntitiesListItem)TVI.Header;

            //ELI.Update(SaveManager.LoadImage(Pokemon.Token_ResourceID), Pokemon.Name, new List<KeyValuePair<System.Windows.Media.Color, string>>());
        }

        private void EntitiesManager_DisplayEntry(Entry_Data entry)
        {
            var ELI = new EntitiesListItem();
            var PermissionIndicators = new List<KeyValuePair<Color, string>>();

            foreach (string ID in entry.View)
            {
                var user = users.Find(x => x.UserID == ID);
                PermissionIndicators.Add(new KeyValuePair<Color, string>(user.UserColor, user.IC_Name));
            }

            ELI.Update(entry.Name, PermissionIndicators);

            #region Context Menu

            var EntitiesManager_Entities = new ContextMenu();
            EntitiesManager_Entities.Tag = entry;
            var ctxm_Entities_Edit = new MenuItem();
            ctxm_Entities_Edit.Header = "View / Edit";
            ctxm_Entities_Edit.Click += Ctxm_Entities_Edit_Click;
            var ctxm_Entities_Duplicate = new MenuItem();
            ctxm_Entities_Duplicate.Header = "Duplicate";
            ctxm_Entities_Duplicate.Click += Ctxm_Entities_Duplicate_Click;
            ctxm_Entities_Duplicate.IsEnabled = false;
            var ctxm_Entities_S1 = new Separator();
            var ctxm_Entities_Delete = new MenuItem();
            ctxm_Entities_Delete.Header = "Delete";
            ctxm_Entities_Delete.Click += Ctxm_Entities_Delete_Click;

            EntitiesManager_Entities.Items.Add(ctxm_Entities_Edit);
            EntitiesManager_Entities.Items.Add(ctxm_Entities_Duplicate);
            EntitiesManager_Entities.Items.Add(ctxm_Entities_S1);
            EntitiesManager_Entities.Items.Add(ctxm_Entities_Delete);

            #endregion

            var TVI = new TreeViewItem
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
                var Parent = EntitiesManager_Folders.Find(x => ((Folder)x.Tag).ID == entry.Parent_Folder);
                Parent.Items.Add(TVI);
            }
            EntitiesManager_Entrys.Add(TVI);

            Program.ClientInstance.Client.SendData(new ImageResource
            {
                UseCommand = "Entities_List",
                UseID = entry.ID,
                Resource_ID = entry.Token_ResourceID
            }, new Networking.Client.TCP.AwaitingCallbacks_Invoke((data) =>
            {
                var IRD = (ImageResource)data;
                UpdateImage(IRD.UseID, IRD.Image);
            })); // Retrieves the Image
        }

        private void EntitiesManager_Display(string ParentID = null)
        {
            TreeViewItem Child;

            foreach (var entry in entrys.FindAll(x => x.Parent_Folder == ParentID))
                EntitiesManager_DisplayEntry(entry);

            foreach (var folder in folders.FindAll(x => x.Parent == ParentID))
                EntitiesManager_DisplayFolder(folder);
        }

        #endregion
    }
}