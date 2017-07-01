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
using System.Windows.Media.Imaging;

namespace AssaultBird2454.VPTU.SaveManager
{
    public enum SaveData_Dir { Pokedex_Pokemon, Pokedex_Moves, Pokedex_Abilitys, Pokedex_Items, Resource_Image }

    public class SaveManager
    {
        #region Variables
        public ProjectInfo VersioningInfo { get; }
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
            //Load Versioning information
            #region Versioning Info
            using (Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("AssaultBird2454.VPTU.SaveManager.ProjectVariables.json"))
            {
                using (StreamReader read = new StreamReader(str))
                {
                    VersioningInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(read.ReadToEnd());
                }
            }
            #endregion

            SaveFileDir = SelectedSaveFile;// Sets the property containing the Save File Directory
        }

        /// <summary>
        /// Load save data to save data class ready for use
        /// </summary>
        /// <returns>Save Data</returns>
        public void Load_SaveData()
        {
            SaveData = new Data.SaveFile.PTUSaveData(true);
            SaveData.PokedexData.Pokemon = LoadData_FromSave<List<Pokedex.Pokemon.PokemonData>>(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Pokemon));
            SaveData.PokedexData.Moves = LoadData_FromSave<List<Pokedex.Moves.MoveData>>(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Moves));
            SaveData.PokedexData.Abilitys = LoadData_FromSave<List<Pokedex.Abilitys.AbilityData>>(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Abilitys));
            SaveData.PokedexData.Items = LoadData_FromSave<List<Pokedex.Items.ItemData>>(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Items));

            SaveData.ImageResources = LoadData_FromSave<List<Resource_Data.Resources>>(GetSaveFile_DataDir(SaveData_Dir.Resource_Image));

            SaveData.InitNullObjects();
        }
        /// <summary>
        /// Saves save data to save file
        /// </summary>
        public void Save_SaveData()
        {
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Pokemon), SaveData.PokedexData.Pokemon);
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Moves), SaveData.PokedexData.Moves);
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Abilitys), SaveData.PokedexData.Abilitys);
            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Pokedex_Items), SaveData.PokedexData.Items);

            SaveData_ToSave(GetSaveFile_DataDir(SaveData_Dir.Resource_Image), SaveData.ImageResources);
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
                        return default(T);
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
                        DataWriter.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Object));// Serialises and Writes to file
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
        public BitmapImage LoadImage(string FilePath)
        {
            if (FilePath.ToLower().StartsWith("save:"))
            {
                //Creates a stream to read the save file from
                using (FileStream Reader = new FileStream(SaveFileDir, FileMode.OpenOrCreate))
                {
                    //Creates an object to read the archive data from
                    using (ZipArchive archive = new ZipArchive(Reader, ZipArchiveMode.Update))
                    {
                        Stream str = (archive.GetEntry(FilePath.Remove(0, 5))).Open();

                        BitmapImage bmp = new BitmapImage();
                        bmp.BeginInit();

                        MemoryStream ms = new MemoryStream();
                        Image.FromStream(str).Save(ms, ImageFormat.Bmp);
                        ms.Seek(0, SeekOrigin.Begin);

                        bmp.StreamSource = ms;
                        bmp.EndInit();

                        return bmp;
                    }
                }
            }
            else if (FilePath.ToLower().StartsWith("path:"))
            {
                FileStream str = new FileStream(FilePath.Remove(0, 5), FileMode.Open);

                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();

                MemoryStream ms = new MemoryStream();
                Image.FromStream(str).Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                bmp.StreamSource = ms;
                bmp.EndInit();

                return bmp;
            }
            return null;
        }
        #endregion
        #endregion
    }
}
