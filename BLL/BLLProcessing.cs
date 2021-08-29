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
    /*
     * Created by Cole Lamers 
     * Date: 2020-12-22
     * 
     * == Purpose ==
     * This code is to make folders based off file metadata and either copy or move them there 
     * 
     * Changes: (date,  comment)
     * 
     *              
     */

    /*
     * == Global Task List ==
     * TODO: --g-- add each folder name to a dictionary and verify if the full value exists. if it does, do not make a new folder and associate that specific file with the other key-pair. ex: usher - yeah; usher ft. akon - bartender. usher would be in the dictionary, and usher ft. akon would contain that value therefore it would be ignored making a new folder.
     * TODO: --g-- parse for the first regex of text before a "-" assuming that is the artist name. if (artistname != exist), then search that. if "-" does not exist, then grab whole folder name
     * TODO: --g-- need to add something in so that if no output path specified, it defaults to the parent path or immediate path
     * TODO: --g-- need to provide error catching and explaining what is happening
     * TODO: --g-- add those xml summaries to functions to explain what each bit of the code is doing.
     * 
     */
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
        } // BLLProcessing

        public void StartProcessing()
        {
            fileItemList.Clear();
            ScanDirectory();
            CreateFolders();
        } // function StartProcessing

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
                } // switch
            } // try
            catch
            {
                throw;
            } // catch
        } // function CopyOrMoveFiles

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
                } // if
                else
                {
                    // TODO: --1-- need to have a fully specified pathname here. Otherwise it goes to a default parent.
                    newPathWithFile = $"{noArtistPath}\\{filename}";
                } // else
                CopyOrMoveFiles(file, newPathWithFile);
            } // try
            catch
            {
                throw;
            } // catch
        } // function BuildFolderModifyFiles

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
                } // try
                catch (Exception e)
                {
                    debug.LogAction($"Error: {e}");
                } // cathc
            } // foreache
        } // function CreateFolders

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
                    } // foreach
                } // if; Gets all files in a list of directories
                else
                {
                    List<string> filesInPath = Directory.GetFiles(config.Source).ToList();
                    CreateObjectAndFillList(filesInPath);
                } // else; Gets all files in a directory that has no subdirectories
            } // try
            catch (Exception ex)
            {
                debug.LogAction($"Error: {ex}");
            } // catch
        } // function ScanDirectory

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
            } // foreach
        } // function CreateObjectAndFillList
    } // BLLProcessing
} // namespace
