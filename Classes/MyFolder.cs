using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_sharepoint_download_samuel.Classes
{
    class MyFolder
    {
        public string name { get; set; }
        public string staticName { get; set; }
        public string displayName { get; set; }

        public float count { get; set; }

        private string type;
        public List<MyFolder> subFolders { get; set; }
        public List<MyFile> files { get; set; }

        public MyFolder()
        {
            this.type = "folder";
            this.files = new List<MyFile>();
            this.subFolders = new List<MyFolder>();
        }

        public MyFolder(string name, string staticName, string displayName)
        {
            this.name = name;
            this.type = "folder";
            this.staticName = staticName;
            this.displayName = displayName;
            this.files =  new List<MyFile>();
            this.subFolders = new List<MyFolder>();
        }

        public MyFolder(string name)
        {
            this.name = name;
            this.type = "folder";
            this.files = new List<MyFile>();
            this.subFolders = new List<MyFolder>();
        }
    }
}
