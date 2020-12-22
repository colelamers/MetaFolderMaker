using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ProjectLogging;
using TagLib;

namespace MetaFolderMaker
{
    public class BLLProcessing
    {
        //TODO: --2-- may need to go back and revise the ProjectLogging dll to see if i can pass a reference variable through instead
        DebugLogging debug;
        Configuration config;
        List<FileItem> fileItemList = new List<FileItem>();
        //delete this this is a test

        public BLLProcessing(ref Configuration inConfig, ref DebugLogging inDebug)
        {
            config = inConfig;
            debug = inDebug;
        }
        public void StartProcessing()
        {
            fileItemList.Clear();
            ScanDirectory();


            //FileInfo n = new FileInfo(file);//TODO: --1-- need to use this to move/copy files!

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
                FileData.Artist = tagData.Tag.FirstAlbumArtist;
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
