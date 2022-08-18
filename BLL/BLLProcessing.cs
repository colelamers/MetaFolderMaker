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
     * 20220304    I fixed my stupid way of doing things before and made them not only easier
     *             to follow but also got rid of that dumb linq call.
     *              
     */

    /*
     * TODO: -- parse for the first regex of text before a "-" assuming that is the artist name. if (artistname != exist), then search that. if "-" does not exist, then grab whole folder name
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

        /**
         * Constructor
         */
        public BLLProcessing(ref Configuration inConfig, ref DebugLogging inDebug)
        {
            config = inConfig;
            debug = inDebug;
            outputPath = config.Output;
            noArtistPath = $"{outputPath}\\_NOARTIST";
        } // BLLProcessing

        /**
         * App processing
         */
        public void StartProcessing()
        {
            outputPath = config.Output;
            fileItemList.Clear();
            ScanDirectory();
            CreateFolders();
        }

        /**
         * Determines if folders will be moved or copied
         * 
         * @param | FileItem | file |
         * FileItem object.
         * 
         * @param | string | whatPath | "C:\\Path\\To\\Dir |
         * Path to the directory.
         */
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
                    }
                    newPathWithFile = $"{newPath}\\{filename}";
                }
                else
                {
                    // TODO: --1-- need to have a fully specified pathname here. Otherwise it goes to a default parent.
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

        /**
         * This function dives through a directory given, then greates an object of each kind
         * when it finds all files within that tail directory.
         * 
         * @param | string | filePath | "C:\\Users\\MyUser\\Desktop\\Dir\\" |
         * This parameter takes explicitly a directory.
         */
        private void RecursiveScan(string filePath)
        {
            try
            {
                // If directory has children, dive; else get files
                var n = Directory.GetDirectories(filePath);
                if (n.Length > 0)
                {
                    foreach (string dir in n)
                    {
                        RecursiveScan(dir);
                    }
                }

                // Get files in Dir
                var filesInChildDir = Directory.GetFiles(filePath);
                if (filesInChildDir.Length > 0)
                {
                    foreach (string file in filesInChildDir)
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
                }
            }
            catch(Exception e)
            {
                debug.LogAction($"Error when retrieving files: {e}");
            }
        }

        private void ScanDirectory()
        {
            debug.LogAction("Scanning Directory");
            try
            {
                RecursiveScan(config.Source);
            }
            catch (Exception ex)
            {
                debug.LogAction($"Error at recursive scan: {ex}");
            }
        }
    }
}