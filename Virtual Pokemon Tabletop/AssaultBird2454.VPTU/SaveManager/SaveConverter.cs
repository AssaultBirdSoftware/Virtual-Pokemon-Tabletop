using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AssaultBird2454.VPTU.SaveManager
{
    public static class SaveFileConverter
    {
        /// <summary>
        /// Extract the save data into a directory ready for migrating versions
        /// </summary>
        /// <param name="SaveFile_DIR">The Directory to the save file being extracted</param>
        /// <param name="Extraction_DIR">The Directory to where the save file data will be extracted to</param>
        public static void Extract_Save(string SaveFile_DIR, string Extraction_DIR)
        {
            using (FileStream FileStream = new FileStream(SaveFile_DIR, FileMode.Open))
            {
                using (ZipArchive Archive = new ZipArchive(FileStream, ZipArchiveMode.Read))
                {
                    Archive.ExtractToDirectory(Extraction_DIR);
                }
            }
        }
        /// <summary>
        /// Compress the save data into a readable savefile
        /// </summary>
        /// <param name="SaveFile_DIR">The Directory to the save file being written</param>
        /// <param name="SaveData_DIR">The Directory to the save data to be compressed into a save file</param>
        public static void Compress_Save(string SaveFile_DIR, string SaveData_DIR)
        {
            using (FileStream stream = new FileStream(SaveFile_DIR, FileMode.Create, FileAccess.ReadWrite))// Creates a stream to write to file
            {
                using (ZipArchive Archive = new ZipArchive(stream, ZipArchiveMode.Update))// Creates a ZipArchive to execute save file modifications inside the Archive
                {
                    foreach (string File in Directory.GetFiles(SaveData_DIR))
                    {
                        Archive.CreateEntryFromFile(File, File.Remove(0, SaveData_DIR.Length));
                    }
                }
            }
        }

        /// <summary>
        /// Used to validate that the datafiles in a directory are valid and dont have errors
        /// </summary>
        /// <param name="SaveData_DIR">The Directory where all the dave data files are kept (Files Must be in correct file structure)</param>
        private static void ValidateExtracted_Save(string SaveData_DIR)
        {

        }
        /// <summary>
        /// Used to validate that the savefile is valid and dont have errors
        /// </summary>
        /// <param name="SaveFile_DIR">The Directory to the save file to check</param>
        private static void ValidateCompressed_Save(string SaveFile_DIR)
        {

        }

        /// <summary>
        /// Used to construct the file system that is compatible with the save data. New or Existing Data will be Created or Organized
        /// </summary>
        /// <param name="SaveData_DIR">The Directory to build and create the file system</param>
        private static void StructureExtracted_Save(string SaveData_DIR)
        {

        }
        /// <summary>
        /// Used to convert and upgrade all the data in a save file to another version
        /// </summary>
        /// <param name="SaveFile_DIR">Save File to upgrade</param>
        private static void Upgrade_SaveFile(string SaveFile_DIR)
        {

        }
    }
}
