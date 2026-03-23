using System;

namespace HandsOnDAY26
{
    // 1. Singleton Class
    public class ConfigurationManager
    {
        // Static instance (only one object)
        private static ConfigurationManager instance;

        // Lock object for thread safety (optional basic level)
        private static readonly object lockObj = new object();

        // Properties
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public string DatabaseConnectionString { get; set; }

        // Private constructor (prevents new keyword)
        private ConfigurationManager()
        {
            // Default values
            ApplicationName = "Inventory System";
            Version = "1.0.0";
            DatabaseConnectionString = "Server=localhost;Database=InventoryDB;";
        }

        // GetInstance method
        public static ConfigurationManager GetInstance()
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new ConfigurationManager();
                    }
                }
            }
            return instance;
        }
    }

    // Main Class
    internal class Problem_05_Singleton_Implementation
    {
        static void Main(string[] args)
        {
            // First call
            ConfigurationManager config1 = ConfigurationManager.GetInstance();

            // Second call
            ConfigurationManager config2 = ConfigurationManager.GetInstance();

            // Print details from both instances
            Console.WriteLine("First Instance:");
            Console.WriteLine(config1.ApplicationName);
            Console.WriteLine(config1.Version);
            Console.WriteLine(config1.DatabaseConnectionString);

            Console.WriteLine();

            Console.WriteLine("Second Instance:");
            Console.WriteLine(config2.ApplicationName);
            Console.WriteLine(config2.Version);
            Console.WriteLine(config2.DatabaseConnectionString);

            Console.WriteLine();

            // Check if both instances are same
            Console.WriteLine("Are both instances same? " + (config1 == config2));
        }
    }
}