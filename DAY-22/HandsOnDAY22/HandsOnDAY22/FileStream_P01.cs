using System;
using System.Collections.Generic;
using System.Text;

namespace HandsOnDAY22
{
    internal class FileStream_P01
    {
        static void Main(string[] args) {

            try
            {
                Console.Write("Enter message: ");
                string message = Console.ReadLine();

                using (FileStream fs = new FileStream(@"C:\Users\keerthi\OneDrive\Desktop\upGrad\DAY-22\log.txt",
                    FileMode.Append,
                    FileAccess.Write))
                {
                    byte[] data = Encoding.UTF8.GetBytes(message + Environment.NewLine);
                    fs.Write(data, 0, data.Length);
                }

                Console.WriteLine("Message written successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
