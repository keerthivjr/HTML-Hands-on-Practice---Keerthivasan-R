using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace HandsOnDAY22
{
    internal class DirectoryInfo_P_4
    {
        static void Main(string[] args) {

            try {
                Console.Write("Enter root directory path: ");
                string path = Console.ReadLine();

                DirectoryInfo dir = new DirectoryInfo(path);

                if (!dir.Exists) {
                    Console.WriteLine("Directory not found!");
                    return;
                }

                DirectoryInfo[] subDirs = dir.GetDirectories();

                foreach (DirectoryInfo subDir in subDirs)
                {
                    int fileCount = subDir.GetFiles().Length;

                    Console.WriteLine($"Folder: {subDir.Name}");
                    Console.WriteLine($"Files Count: {fileCount}");
                    Console.WriteLine("---------------------------");

                }
            } 
            catch(Exception e) {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
