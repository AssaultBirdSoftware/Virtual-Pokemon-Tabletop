using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssaultBird2454.VPTU.SaveManager
{
    public enum SaveData_Dir { Pokedex_Pokemon, Pokedex_Moves, Pokedex_Abilitys, Pokedex_Items, Resource_Image, Entity_Pokemon, Entity_Trainers, Basic_CampaignSettings, Auth_Users, Auth_Groups, Entity_Folder }

    public class No_Data_Found_In_Save_Exception : Exception { }

    public class SaveManager
    {
        #region Variables
        public string SaveFileDir { get; }
        /// <summary>
        /// A Save Data Object for use inside the software, This is Saved to the file when Save_SaveData() is called
        /// </summary>
        public Data.SaveFile.PTUSaveData SaveData { get; set; }
        #endregion

        /// <summary>
        /// Creates a new instance of a Save File Manager
        /// </summary>
        /// <param name="SelectedSaveFile">The Directory of the save file that will be used</param>
        public SaveManager(string SelectedSaveFile)
        {
            SaveFileDir = SelectedSaveFile;// Sets the property containing the Save File Directory
        }

        /// <summary>
        /// Load save data to save data class ready for use
        /// </summary>
        /// <returns>Save Data</returns>
        public void Load_SaveData()
        {
            try
            {
                SaveData = new Data.SaveFile.PTUSaveData(true);

                try { SaveData.Campaign_Data = LoadData_FromSave<Data.Campaign_Data>(GetSaveFile_DataDir(SaveData_Dir.Basic_CampaignSettings)); } catch (No_Data_Found_In_Save_Exception) { SaveData.Campaign_Data = new Data.Campaign_Data(true); }// Basic Campaign Settings

                try { SaveData.Users = LoadData_FromSave<List<Authentication_Manager.Data.User>>(GetSaveFile_DataDir(SaveData_Dir.Auth_Users)); } catch (No_Data_Found_In_Save_Exception) { SaveData.Users = new List<Authentication_Manager.Data.User>(); }
                try { SaveData.Groups = LoadData_FromSave<List<Authentication_Manager.Data.Group>>(GetSaveFile_DataDir(SaveData_Dir.Auth_Groups)); } catch (No_Data_Found_In_Save_Exception) { SaveData.Groups = new List<Authentication_Manager.Data.Group>(); }

                try { SaveData.PokedexData.Pokemon = LoadData_FromSave<List<Pokedex.Pokemon.PokemonData>>(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Pokemon)); } catch (No_Data_Found_In_Save_Exception) { SaveData.PokedexData.Pokemon = new List<Pokedex.Pokemon.PokemonData>(); }
                try { SaveData.PokedexData.Moves = LoadData_FromSave<List<Pokedex.Moves.MoveData>>(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Moves)); } catch (No_Data_Found_In_Save_Exception) { SaveData.PokedexData.Moves = new List<Pokedex.Moves.MoveData>(); }
                try { SaveData.PokedexData.Abilitys = LoadData_FromSave<List<Pokedex.Abilitys.AbilityData>>(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Abilitys)); } catch (No_Data_Found_In_Save_Exception) { SaveData.PokedexData.Abilitys = new List<Pokedex.Abilitys.AbilityData>(); }
                try { SaveData.PokedexData.Items = LoadData_FromSave<List<Pokedex.Items.ItemData>>(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Items)); } catch (No_Data_Found_In_Save_Exception) { SaveData.PokedexData.Items = new List<Pokedex.Items.ItemData>(); }

                try { SaveData.ImageResources = LoadData_FromSave<List<Resource_Data.Resources>>(GetSaveFile_DataDir(SaveData_Dir.Resource_Image)); } catch (No_Data_Found_In_Save_Exception) { SaveData.ImageResources = new List<Resource_Data.Resources>(); }

                try { SaveData.Folders = LoadData_FromSave<List<EntityManager.Folder>>(GetSaveFile_DataDir(SaveData_Dir.Entity_Folder)); } catch (No_Data_Found_In_Save_Exception) { SaveData.Folders = new List<EntityManager.Folder>(); }
                try { SaveData.Pokemon = LoadData_FromSave<List<EntityManager.Pokemon.PokemonCharacter>>(GetSaveFile_DataDir(SaveData_Dir.Entity_Pokemon)); } catch (No_Data_Found_In_Save_Exception) { SaveData.Pokemon = new List<EntityManager.Pokemon.PokemonCharacter>(); }

                SaveData.InitNullObjects();
            }
            catch (Exception e)
            {
                MessageBox.Show("There was an error while loading the save file...\nPlease confirm that the savefile has no errors...", "Save file loading error");
                MessageBox.Show(e.ToString());
            }
        }
        /// <summary>
        /// Saves save data to save file
        /// </summary>
        public void Save_SaveData()
        {
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Basic_CampaignSettings), SaveData.Campaign_Data);

            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Auth_Users), SaveData.Users);
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Auth_Groups), SaveData.Groups);

            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Pokemon), SaveData.PokedexData.Pokemon);
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Moves), SaveData.PokedexData.Moves);
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Abilitys), SaveData.PokedexData.Abilitys);
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Items), SaveData.PokedexData.Items);

            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Resource_Image), SaveData.ImageResources);

            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Entity_Folder), SaveData.Folders);
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Entity_Pokemon), SaveData.Pokemon);
        }

        #region Load and Save
        /// <summary>
        /// Loads Data from the save file
        /// </summary>
        /// <typeparam name="T">Type of data being loaded</typeparam>
        /// <param name="SaveFile_DataDir">The Directory where the data should be loaded from in the save file</param>
        /// <returns>Loaded Object</returns>
        public T LoadData_FromSave<T>(string SaveFile_DataDir)
        {
            //Creates a stream to read the save file from
            using (FileStream Reader = new FileStream(SaveFileDir, FileMode.OpenOrCreate))
            {
                //Creates an object to read the archive data from
                using (ZipArchive archive = new ZipArchive(Reader, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry entry = archive.GetEntry(SaveFile_DataDir);// Gets the entry to be read from
                    if (entry == null)
                    {
                        throw new No_Data_Found_In_Save_Exception();
                    }
                    //Creates a stream to read the data from
                    using (StreamReader DataReader = new StreamReader(entry.Open()))
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(DataReader.ReadToEnd());// Returns the save file data
                    }
                }
            }
        }
        /// <summary>
        /// Saves Data to the save file
        /// </summary>
        /// <param name="SaveFile_DataDir">The Directory where the data should be saved in the file</param>
        /// <param name="Object">Object to save</param>
        public void SaveData_ToSave(string SaveFile_DataDir, object Object)
        {
            //Creates a stream to write any save data to a file
            using (FileStream Writer = new FileStream(SaveFileDir, FileMode.OpenOrCreate))
            {
                //Creates an object to write archive data to
                using (ZipArchive archive = new ZipArchive(Writer, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry entry = archive.GetEntry(SaveFile_DataDir);// Gets the specified entry if it exists ready to write to
                    if (entry != null)
                    {
                        entry.Delete();// Removes the entry. Delete then create a new entry to save
                    }

                    entry = archive.CreateEntry(SaveFile_DataDir);

                    //Creates a stream to Write data to the entry
                    using (StreamWriter DataWriter = new StreamWriter(entry.Open()))
                    {
                        DataWriter.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Object, new Newtonsoft.Json.JsonSerializerSettings()
                        {
                            Formatting = Newtonsoft.Json.Formatting.Indented,
                            NullValueHandling = Newtonsoft.Json.NullValueHandling.Include
                        }));// Serialises and Writes to file
                    }
                }
            }
        }
        /// <summary>
        /// Gets the Path to a specific file in the Save Data Structure (Excluding Resources or Save Files from Plugins)
        /// </summary>
        /// <param name="DirType">File requested</param>
        /// <returns>Path to file</returns>
        public static string GetSaveFile_DataDir(SaveData_Dir DirType)
        {
            switch (DirType)
            {
                case SaveData_Dir.Pokedex_Pokemon:
                    return "Pokedex/Pokemon.json";
                case SaveData_Dir.Pokedex_Moves:
                    return "Pokedex/Moves.json";
                case SaveData_Dir.Pokedex_Abilitys:
                    return "Pokedex/Abilitys.json";
                case SaveData_Dir.Pokedex_Items:
                    return "Pokedex/Items.json";
                case SaveData_Dir.Resource_Image:
                    return "Resource/Data.json";
                case SaveData_Dir.Entity_Pokemon:
                    return "Entity/Pokemon.json";
                case SaveData_Dir.Entity_Trainers:
                    return "Entity/Trainers.json";
                case SaveData_Dir.Entity_Folder:
                    return "Entity/Folders.json";
                case SaveData_Dir.Basic_CampaignSettings:
                    return "CampaignInfo.json";
                case SaveData_Dir.Auth_Users:
                    return "Auth/Users.json";
                case SaveData_Dir.Auth_Groups:
                    return "Auth/Groups.json";

                default:
                    return null;
            }
        }
        #endregion

        #region Resource
        #region Import & Export into SaveFile
        /// <summary>
        /// Adds a file to the internal save file
        /// </summary>
        /// <param name="FilePath">Path to the file</param>
        /// <param name="Name">The name of the file inside the save file.</param>
        public void ImportFile(string FilePath, string Name)
        {
            using (FileStream stream = new FileStream(SaveFileDir, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Update))
                {
                    archive.CreateEntryFromFile(FilePath, Name);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ExportFile()
        {

        }

        /// <summary>
        /// Checks if the file exists in the save file
        /// </summary>
        /// <param name="FileName">The Path and File to check</param>
        /// <returns>If it exists or not</returns>
        public bool FileExists(string FileName)
        {
            using (FileStream stream = new FileStream(SaveFileDir, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Update))
                {
                    try
                    {
                        ZipArchiveEntry entry = archive.GetEntry(FileName);
                        if (entry != null)
                        {
                            return true;
                        }
                    }
                    catch { /* Dont Care... May Throw an exception if a file does not exist */ }
                }
            }
            return false;
        }
        #endregion
        #region Load File
        public Bitmap LoadImage(string ID)
        {
            try
            {
                string FilePath = SaveData.ImageResources.Find(x => x.ID == ID).Path;

                if (FilePath.ToLower().StartsWith("save:"))
                {
                    //Creates a stream to read the save file from
                    using (FileStream Reader = new FileStream(SaveFileDir, FileMode.OpenOrCreate))
                    {
                        //Creates an object to read the archive data from
                        using (ZipArchive archive = new ZipArchive(Reader, ZipArchiveMode.Update))
                        {
                            Stream str = (archive.GetEntry(FilePath.Remove(0, 5))).Open();// Opens a stream & Removes prefix ID
                            Bitmap bmp = new Bitmap(str);// Loads Image

                            return bmp;// Return Image
                        }
                    }
                }
                else if (FilePath.ToLower().StartsWith("path:"))
                {
                    FileStream str = new FileStream(FilePath.Remove(0, 5), FileMode.Open);// Opens a stream & Removes prefix ID
                    Bitmap bmp = new Bitmap(str);// Loads Image

                    return bmp;// Return Image
                }
            }
            catch { }
            return null;
        }
        #endregion
        #endregion
    }
}
