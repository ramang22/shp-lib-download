using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using test_sharepoint_download_samuel.Debug;
using test_sharepoint_download_samuel.Classes;
using Newtonsoft.Json;
using System.IO;

namespace test_sharepoint_download_samuel
{



    class Program
    {
        //todo progressbar
        public static Visual.ProgressBar bar;
        public static List<string> notDowloaded = new List<string>();
        public static string rootName;
        static void Main(string[] args)
        {
            //todo site


            using (var site = new SPSite(DebugConstants.SITEURL))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connected to site...");
                using (var web = site.OpenWeb())
                {
                    Console.WriteLine("Connected to web...");

                    var list = web.GetList(DebugConstants.SITEURL);

                    rootName = list.RootFolder.Name + @"\";

                    if (Directory.Exists((DebugConstants.DOWNLOADPATH) + rootName))
                    {
                        Directory.Delete((DebugConstants.DOWNLOADPATH) + rootName, true);
                    }

                    MyFolder folder = new MyFolder(list.RootFolder.Name, list.RootFolder.Name ,list.Title);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Downloading files to: " + (DebugConstants.DOWNLOADPATH) + rootName);
                    Console.WriteLine();

                    try
                    {
                        bar = new Visual.ProgressBar(list.ItemCount);
                    }
                    catch
                    {
                        bar = new Visual.ProgressBar();
                    }

                    folder.count = bar.total;
                    //root
                    StructureView(list.RootFolder, folder, web);

                    string json = JsonConvert.SerializeObject(folder);
                    System.IO.File.WriteAllText((DebugConstants.DOWNLOADPATH) + rootName + "struct.json", json);


                    if (notDowloaded.Count > 0)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unsuccesesful downloads: " + notDowloaded.Count);
                        foreach (string name in notDowloaded)
                        {
                            Console.WriteLine(string.Format("Error in dowloading file : {0}", name));
                        }
                    }
                }

                bar.printEndMessage(notDowloaded.Count);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press <ENTER> to Continue...");
            Console.ReadLine();
        }
        private static void StructureView(SPFolder root, MyFolder folder, SPWeb web)
        {
            if (folder.name.Equals("Forms"))
            {
                return;
            }
            //todo files
            foreach (SPFile file in root.Files)
            {
                //name
                //parent folder name
                MyFile myFile = new MyFile(file.Name, file.ParentFolder.Name);
                folder.files.Add(myFile);
                bar.progress++;
                //bar.drawTextProgressBar(file.Name);
                bar.drawProgress(file.Name);

                if (!DownloadFile(web, file, myFile))
                {
                    notDowloaded.Add(myFile.name);
                }

            }

            foreach (SPFolder subfolder in root.SubFolders)
            {
                if (!subfolder.Name.Equals("Forms"))
                {
                    bar.progress++;
                }
                MyFolder newFolder = new MyFolder(subfolder.Name);
                folder.subFolders.Add(newFolder);
                StructureView(subfolder, newFolder, web);
            }
        }

        private static bool DownloadFile(SPWeb web, SPFile file, MyFile myFile)
        {
            
            if (!Directory.Exists((DebugConstants.DOWNLOADPATH) + rootName))
            {
                Directory.CreateDirectory((DebugConstants.DOWNLOADPATH) + rootName);
            }
            try
            {
                using (var stream = file.OpenBinaryStream())
                {
                    string fileName = (DebugConstants.DOWNLOADPATH) + rootName + myFile.parentFolderName + "/" + myFile.name;
                    if (!Directory.Exists((DebugConstants.DOWNLOADPATH) + rootName + myFile.parentFolderName + "/"))
                    {
                        Directory.CreateDirectory((DebugConstants.DOWNLOADPATH) + rootName + myFile.parentFolderName + "/");
                    }
                    using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
