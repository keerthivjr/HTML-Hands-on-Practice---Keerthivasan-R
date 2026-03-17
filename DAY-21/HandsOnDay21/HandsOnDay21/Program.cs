using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    class Product
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product{ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25},
                new Product{ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45},
                new Product{ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60},
                new Product{ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40},
                new Product{ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120}
            };
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product();
            var products = product.GetProducts();

            // 1. FMCG products
            var q1 = products.Where(p => p.ProCategory == "FMCG");
            Console.WriteLine("FMCG Products");
            foreach (var p in q1)
                Console.WriteLine($"{p.ProCode}\t{p.ProName}\t{p.ProMrp}");

            // 2. Grain products
            var q2 = products.Where(p => p.ProCategory == "Grain");
            Console.WriteLine("\nGrain Products");
            foreach (var p in q2)
                Console.WriteLine($"{p.ProCode}\t{p.ProName}\t{p.ProMrp}");

            // 3. Sort by Product Code
            var q3 = products.OrderBy(p => p.ProCode);
            Console.WriteLine("\nSorted by Product Code");
            foreach (var p in q3)
                Console.WriteLine($"{p.ProCode}\t{p.ProName}");

            // 4. Sort by Category
            var q4 = products.OrderBy(p => p.ProCategory);
            Console.WriteLine("\nSorted by Category");
            foreach (var p in q4)
                Console.WriteLine($"{p.ProCategory}\t{p.ProName}");

            // 5. Sort by MRP Ascending
            var q5 = products.OrderBy(p => p.ProMrp);
            Console.WriteLine("\nMRP Ascending");
            foreach (var p in q5)
                Console.WriteLine($"{p.ProName}\t{p.ProMrp}");

            // 6. Sort by MRP Descending
            var q6 = products.OrderByDescending(p => p.ProMrp);
            Console.WriteLine("\nMRP Descending");
            foreach (var p in q6)
                Console.WriteLine($"{p.ProName}\t{p.ProMrp}");

            // 7. Group by Category
            var q7 = products.GroupBy(p => p.ProCategory);
            Console.WriteLine("\nGroup by Category");
            foreach (var g in q7)
            {
                Console.WriteLine(g.Key);
                foreach (var p in g)
                    Console.WriteLine(p.ProName);
            }

            // 8. Group by MRP
            var q8 = products.GroupBy(p => p.ProMrp);
            Console.WriteLine("\nGroup by MRP");
            foreach (var g in q8)
            {
                Console.WriteLine("MRP: " + g.Key);
                foreach (var p in g)
                    Console.WriteLine(p.ProName);
            }

            // 9. Highest price in FMCG
            var q9 = products.Where(p => p.ProCategory == "FMCG")
                             .OrderByDescending(p => p.ProMrp)
                             .FirstOrDefault();
            Console.WriteLine("\nHighest FMCG Product: " + q9.ProName + " " + q9.ProMrp);

            // 10. Total products count
            Console.WriteLine("\nTotal Products: " + products.Count());

            // 11. FMCG count
            Console.WriteLine("FMCG Count: " + products.Count(p => p.ProCategory == "FMCG"));

            // 12. Max price
            Console.WriteLine("Max Price: " + products.Max(p => p.ProMrp));

            // 13. Min price
            Console.WriteLine("Min Price: " + products.Min(p => p.ProMrp));

            // 14. All below 30
            Console.WriteLine("All products below 30: " + products.All(p => p.ProMrp < 30));

            // 15. Any below 30
            Console.WriteLine("Any product below 30: " + products.Any(p => p.ProMrp < 30));

            Console.ReadLine();
        }
    }
}