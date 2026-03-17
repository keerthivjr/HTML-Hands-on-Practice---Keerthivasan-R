using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product3
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product3> GetProducts()
        {
            return new List<Product3>
            {
                new Product3{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product3{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product3{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product3{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product3{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_03
    {
        static void Main(string[] args) {

            Product3 product = new Product3();

            // 1. Get the list from the method
            var productList = product.GetProducts();

            // 2. Sort the list, not the single 'product' object
            var result3 = productList.OrderBy(p => p.ProCode);

            Console.WriteLine("\nSorted by Product Code:");
            foreach (var item in result3)
                Console.WriteLine($"{item.ProCode}\t{item.ProName}");
        }
    }
}
