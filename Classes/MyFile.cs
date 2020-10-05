using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_sharepoint_download_samuel.Classes
{
    class MyFile
    {
        public string name { get; set; }
        string type;
        public string parentFolderName { get; set; }
        public MyFile()
        {
            this.type = "file";
        }

        public MyFile(string name, string parentname)
        {
            this.name = name;
            this.type = "file";
            this.parentFolderName = parentname;
        }
    }
}
