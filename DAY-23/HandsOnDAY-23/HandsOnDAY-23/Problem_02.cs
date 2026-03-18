using System;
using System.Collections.Generic;
using System.Text;

namespace HandsOnDAY_23
{
    internal class Problem_02
    {
        static async Task WriteLogAsync(string message)
        {
            Console.WriteLine("\nWriting Log: " + message);
            await Task.Delay(1000);
            Console.WriteLine("\nLog Written: " + message);
        }

        static async Task Main(string[] args)
        {
            Task t1 = WriteLogAsync("User logged in");
            Task t2 = WriteLogAsync("File uploaded");
            Task t3 = WriteLogAsync("Error occured");

            await Task.WhenAll(t1, t2, t3);

            Console.WriteLine("\nAll logs written successfully");
        }
    }
}
