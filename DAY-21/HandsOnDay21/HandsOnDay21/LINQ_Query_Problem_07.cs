using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product7
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product7> GetProducts()
        {
            return new List<Product7>
            {
                new Product7{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product7{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product7{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product7{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product7{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_07
    {
        static void Main(string[] args)
        {
            Product7 product7 = new Product7();
            var products = product7.GetProducts();

            // Perform the grouping
            var result = products.GroupBy(p => p.ProCategory).ToList();

            // Iterate over 'result', NOT 'products'
            foreach (var group in result)
            {
                // group.Key is the Category Name (e.g., "FMCG" or "Grain")
                Console.WriteLine($"\nCategory: {group.Key}");

                // Each 'group' acts like a list of products within that category
                foreach (var item in group)
                {
                    Console.WriteLine($" - {item.ProName}: {item.ProMrp}");
                }
            }
            Console.ReadLine();
        }
    }
}
