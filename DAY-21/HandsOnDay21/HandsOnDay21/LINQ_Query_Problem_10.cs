using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product10
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product10> GetProducts()
        {
            return new List<Product10>
            {
                new Product10{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product10{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product10{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product10{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product10{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_10
    {
        static void Main(string[] args) { 
            
            Product10 product = new Product10();

            var products = product.GetProducts();

            var result10 = products.Count();
            Console.WriteLine("\nTotal Products: " + result10);
        }
    }
}
