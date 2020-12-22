using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ProjectLogging;
using TagLib;
using File = System.IO.File;

namespace MetaFolderMaker
{
    public class BLLProcessing
    {
        DebugLogging debug;
        Configuration config;
        List<FileItem> fileItemList = new List<FileItem>();
        string outputPath;
        string noArtistPath;

        public BLLProcessing(ref Configuration inConfig, ref DebugLogging inDebug)
        {
            config = inConfig;
            debug = inDebug;
            outputPath = config.Output;
            noArtistPath = $"{outputPath}\\_NOARTIST";
        }
        public void StartProcessing()
        {
            fileItemList.Clear();
            ScanDirectory();
            CreateFolders();
        }

        private void CopyOrMoveFiles(FileItem file, string whatPath)
        {
            try
            {
                switch (config.FileAction)
                {
                    case "Move Files":
                        File.Move(file.FilePath, whatPath);
                        break;
                    case "Copy Files":
                        File.Copy(file.FilePath, whatPath, true);
                        break;
                }
            }
            catch
            {
                throw;

            }
        }

        private void BuildFolderModifyFiles(FileItem file, string metaDataPulled, string filename)
        {
            try
            {
                string newPath = "";
                string newPathWithFile = "";

                if (metaDataPulled != null)
                {
                    newPath = $"{outputPath}\\{metaDataPulled.Trim()}";

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }//Creates a directory if it doesn't exist
                    newPathWithFile = $"{newPath}\\{filename}";
                }
                else
                {
                    newPathWithFile = $"{noArtistPath}\\{filename}";
                }
                CopyOrMoveFiles(file, newPathWithFile);
            }
            catch
            {
                throw;
            }
        }

        private void CreateFolders()
        {
            if (!Directory.Exists(noArtistPath)) { Directory.CreateDirectory(noArtistPath); }

            foreach (FileItem type in fileItemList)
            {
                try
                {
                    string filename = Path.GetFileName(type.FilePath);

                    switch (config.Tag)
                    {
                        case "Artist":
                            BuildFolderModifyFiles(type, type.MetaData.Artist, filename);
                            break;
                        case "Album":
                            BuildFolderModifyFiles(type, type.MetaData.Album, filename);
                            break;
                        case "Title":
                            BuildFolderModifyFiles(type, type.MetaData.Title, filename);
                            break;
                    }
                }
                catch (Exception e)
                {
                    debug.LogAction($"Error: {e}");
                }
            }

        }

        private void ScanDirectory()
        {
            debug.LogAction("Scanning Directory");
            try
            {
                //Gets a list of all directories except for hidden files/folders
                List<string> subDirs = Directory.GetDirectories(config.Source)
                    .Select(d => new { Attr = new DirectoryInfo(d).Attributes, Dir = d })
                      .Where(x => !x.Attr.HasFlag(FileAttributes.System))
                      .Where(x => !x.Attr.HasFlag(FileAttributes.Hidden))
                      .Select(x => x.Dir)
                      .ToList();

                if (subDirs.Count > 0)
                {
                    foreach (string dir in subDirs)
                    {
                        List<string> filesInDirectory = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories).ToList();
                        CreateObjectAndFillList(filesInDirectory);
                    }
                }//Gets all files in a list of directories
                else
                {
                    List<string> filesInPath = Directory.GetFiles(config.Source).ToList();
                    CreateObjectAndFillList(filesInPath);
                }//Gets all files in a directory that has no subdirectories

            }
            catch (Exception ex)
            {
                debug.LogAction($"Error: {ex}");
            }
        }

        //TODO: --1-- void function that checks config for switch case how it will create folders. then conduct null checks and grab something else?

        private void CreateObjectAndFillList(List<string> filesInDirectory)
        {
            foreach (string file in filesInDirectory)
            {
                TagLib.File tagData = TagLib.File.Create(file);

                FileMetaData FileData = new FileMetaData();

                FileData.Artist = tagData.Tag.FirstArtist;
                FileData.Album = tagData.Tag.Album;
                FileData.Title = tagData.Tag.Title;

                FileItem item = new FileItem();
                item.FilePath = file;
                item.MetaData = FileData;

                fileItemList.Add(item);
            }
        }//Builds Item Objects and adds them to a list

    }
}
