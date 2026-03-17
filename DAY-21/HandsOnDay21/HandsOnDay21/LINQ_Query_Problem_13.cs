using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product13
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product13> GetProducts()
        {
            return new List<Product13>
            {
                new Product13{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product13{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product13{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product13{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product13{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_13
    {
        static void Main(string[] args) {

            Product13 product = new Product13();

            var products = product.GetProducts();

            var result13 = products.Min(p => p.ProMrp);
            Console.WriteLine("Min Price: " + result13);
        }
    }
}
