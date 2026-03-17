using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product8
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product8> GetProducts()
        {
            return new List<Product8>
            {
                new Product8{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product8{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product8{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product8{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product8{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_08
    {

        static void Main(string[] args) {

            Product8 product = new Product8();
            var products = product.GetProducts();

            // 1. Group the products by Mrp
            var result = products.GroupBy(p => p.ProMrp).ToList();

            Console.WriteLine("\nGroup by MRP:");

            // 2. Loop through 'result' (the groups), NOT 'products' (the raw list)
            foreach (var group in result)
            {
                // group.Key is the actual MRP value (e.g., 25, 45, 60...)
                Console.WriteLine("\nMRP: " + group.Key);

                // Loop through all products that have this specific MRP
                foreach (var item in group)
                {
                    Console.WriteLine($" - {item.ProName}");
                }
            }
        }
    }
}
