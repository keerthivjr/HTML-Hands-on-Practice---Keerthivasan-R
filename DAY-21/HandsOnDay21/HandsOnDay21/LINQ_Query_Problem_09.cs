using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product9
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product9> GetProducts()
        {
            return new List<Product9>
            {
                new Product9{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product9{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product9{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product9{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product9{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_09
    {
        static void Main(string[] args) {

            Product9 product9 = new Product9();
            var products = product9.GetProducts();

            // 1. Query 'products' (the List), not 'product9'
            // 2. Remove the extra dot at the end
            var result9 = products
                .Where(p => p.ProCategory == "FMCG")
                .OrderByDescending(p => p.ProMrp)
                .FirstOrDefault();

            Console.WriteLine("\nHighest FMCG Product:");

            // 3. Simple null check to prevent a NullReferenceException
            if (result9 != null)
            {
                Console.WriteLine($"{result9.ProName} {result9.ProMrp}");
            }
            else
            {
                Console.WriteLine("No FMCG products found.");
            }
        }
    }
}
