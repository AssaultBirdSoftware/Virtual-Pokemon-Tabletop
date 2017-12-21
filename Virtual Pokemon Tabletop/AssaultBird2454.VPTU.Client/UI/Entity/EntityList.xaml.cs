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

namespace AssaultBird2454.VPTU.Client.UI.Entity
{
    /// <summary>
    /// Interaction logic for EntityList.xaml
    /// </summary>
    public partial class EntityList : UserControl
    {
        ContextMenu ctxm_Root;
        List<EntityManager.Folder> folders;
        List<EntityManager.Entry_Data> entrys;

        public EntityList()
        {
            InitializeComponent();
            Create_ContextMenu();
        }

        #region Entity Manager Code
        List<TreeViewItem> EntityManager_Folders = new List<TreeViewItem>();
        List<TreeViewItem> EntityManager_Entrys = new List<TreeViewItem>();
        ContextMenu EntityManager_Root;

        public void UpdateImage(string ID, Bitmap Image)
        {
            EntityListItem ELI = (EntityListItem)(EntityManager_Entrys.Find(x => ((EntityManager.Entry_Data)x.Tag).ID == ID).Header);

            ELI.Update(Image);
        }

        public void EntityManager_ReloadList(List<EntityManager.Folder> _folders, List<EntityManager.Entry_Data> _entrys)
        {
            folders = _folders;
            entrys = _entrys;

            EntityManager_Folders.Clear();
            EntityManager_Entrys.Clear();
            Tree.Items.Clear();

            EntityManager_Display();
        }
        public void Create_ContextMenu()
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
            Tree.ContextMenu = EntityManager_Root;
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
            try
            {
                ContextMenu ctxm = ((ContextMenu)((MenuItem)sender).Parent);
                EntityManager.Entry_Data data = entrys.Find(x => x.ID == ((EntityManager.Entry_Data)ctxm.Tag).ID);
                if (data.Entity_Type == EntityManager.Entity_Type.Pokemon)
                {
                    Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Entity.Entity_Pokemon_Get()// Gets the Pokemon Selected
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

            // Send Command

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

                folders.Add(folder);
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

                folders.Add(folder);
                Tree.Items.Add(TVI);
                EntityManager_Folders.Add(TVI);
            }
        }
        public void EntityManager_DeleteDir(string ID)
        {
            foreach (EntityManager.Folder ChildFolder in folders.FindAll(x => x.Parent == ID))
            {
                EntityManager_DeleteDir(ChildFolder.ID);
            }
            foreach (EntityManager.Entry_Data entry in entrys.FindAll(x => x.Parent_Folder == ID && x.Entity_Type == EntityManager.Entity_Type.Trainer))
            {
                EntityManager_DeleteEntry(entry.ID);
            }
            foreach (EntityManager.Entry_Data entry in entrys.FindAll(x => x.Parent_Folder == ID && x.Entity_Type == EntityManager.Entity_Type.Pokemon))
            {
                EntityManager_DeleteEntry(entry.ID);
            }

            EntityManager.Folder Folder = folders.Find(x => x.ID == ID);

            TreeViewItem TVI = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == Folder.ID);
            if (Folder.Parent == null)
            {
                Tree.Items.Remove(TVI);
            }
            else
            {
                TreeViewItem ParentTVI = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == Folder.Parent);
                ParentTVI.Items.Remove(TVI);
            }

            EntityManager_Folders.Remove(TVI);
            folders.Remove(Folder);
        }
        public void EntityManager_DeleteEntry(string ID)
        {
            EntityManager.Entry_Data Entry = (EntityManager.Entry_Data)EntityManager_Entrys.Find(x => ((EntityManager.Entry_Data)x.Tag).ID == ID).Tag;

            entrys.Remove(Entry);
            if (Entry.Entity_Type == EntityManager.Entity_Type.Pokemon)
            {
                try
                {
                    // Send Command
                }
                catch { /* Dont Care */ }
            }
            else if (Entry.Entity_Type == EntityManager.Entity_Type.Trainer)
            {
                try
                {
                    // Send Command
                }
                catch { /* Dont Care */ }
            }

            TreeViewItem TVI = EntityManager_Entrys.Find(x => ((EntityManager.Entry_Data)x.Tag).ID == ID);
            if (Entry.Parent_Folder == null)
            {
                Tree.Items.Remove(TVI);
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
                Tree.Items.Add(Child);
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
            //UI.Entity.Pokemon_Character pc = new UI.Entity.Pokemon_Character(SaveManager);
            //pc.PokemonData.Parent_Folder = Folder;
            //pc.ShowDialog();

            //SaveManager.SaveData.Pokemon.Add(pc.PokemonData);

            //EntityManager_DisplayEntry(pc.PokemonData.EntryData);
        }
        public void EntityManager_EditPokemonEntity(EntityManager.Pokemon.PokemonCharacter Pokemon)
        {
            //UI.Entity.Pokemon_Character pc = new UI.Entity.Pokemon_Character(SaveManager, Pokemon);
            //pc.ShowDialog();

            //TreeViewItem TVI = EntityManager_Entrys.Find(x => ((EntityManager.Entry_Data)x.Tag).ID == Pokemon.ID);
            //UI.Entity.EntityListItem ELI = (UI.Entity.EntityListItem)TVI.Header;

            //ELI.Update(SaveManager.LoadImage(Pokemon.Token_ResourceID), Pokemon.Name, new List<KeyValuePair<System.Windows.Media.Color, string>>());
        }
        private void EntityManager_DisplayEntry(EntityManager.Entry_Data entry)
        {
            UI.Entity.EntityListItem ELI = new UI.Entity.EntityListItem();
            ELI.Update(entry.Name, new List<KeyValuePair<System.Windows.Media.Color, string>>());

            #region Context Menu
            ContextMenu EntityManager_Entity = new ContextMenu();
            EntityManager_Entity.Tag = entry;
            MenuItem ctxm_Entity_Edit = new MenuItem();
            ctxm_Entity_Edit.Header = "View / Edit";
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
                Tree.Items.Add(TVI);
            }
            else
            {
                TreeViewItem Parent = EntityManager_Folders.Find(x => ((EntityManager.Folder)x.Tag).ID == entry.Parent_Folder);
                Parent.Items.Add(TVI);
            }
            EntityManager_Entrys.Add(TVI);

            Program.ClientInstance.Client.SendData(new VPTU.Server.Instances.CommandData.Resources.ImageResource
            {
                UseCommand = "Entity_List",
                UseID = entry.ID,
                Resource_ID = entry.Token_ResourceID
            });// Retrieves the Image
        }

        private void EntityManager_Display(string ParentID = null)
        {
            TreeViewItem Child;

            foreach (EntityManager.Entry_Data entry in entrys.FindAll(x => x.Parent_Folder == ParentID))
            {
                EntityManager_DisplayEntry(entry);
            }

            foreach (EntityManager.Folder folder in folders.FindAll(x => x.Parent == ParentID))
            {
                EntityManager_DisplayFolder(folder);
            }
        }
        #endregion

        private void ToolBar_Reload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Program.ClientInstance.Client.SendData(new Server.Instances.CommandData.Entity.Entity_All_GetList());
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You need to connect to a running server first before you can load this list!");
            }
        }
    }
}
