using System;
using System.Collections.Generic;
using System.Text;

namespace MetaFolderMaker
{
    public class Configuration
    {
        public string Source { get; set; }
        public string Output { get; set; }
        public string Tag { get; set; }
        public string FileAction { get; set; }

        public Configuration()
        {
            Source = "";
            Output = "";
            Tag = "Artist";
            FileAction = "Move Files";
        }

    }
}
