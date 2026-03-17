using System;
using System.Collections.Generic;
using System.Text;

namespace HandsOnDAY22
{
    internal class FileInfo_P02
    {
        static void Main(string[] args) {

            try
            {

                Console.Write("Enter folder path: ");
                string path = Console.ReadLine();
                Console.WriteLine();

                DirectoryInfo dir = new DirectoryInfo(path);

                if (!dir.Exists)
                {
                    Console.WriteLine("Directory not found");
                    return;
                }

                FileInfo[] files = dir.GetFiles();
                int count = 0;

                foreach (FileInfo file in files)
                {
                    Console.WriteLine($"Name: {file.Name}");
                    Console.WriteLine($"Size: {file.Length}bytes");
                    Console.WriteLine($"Created: {file.CreationTime}");
                    Console.WriteLine("------------------------------");
                    count++;
                }
                Console.WriteLine($"Total Files: {count}");
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
