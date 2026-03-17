using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product12
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product12> GetProducts()
        {
            return new List<Product12>
            {
                new Product12{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product12{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product12{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product12{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product12{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_12
    {
        static void Main(string[] args) {

            Product12 product = new Product12();

            var products = product.GetProducts();

            var result12 = products.Max(p => p.ProMrp);
            Console.WriteLine("Max Price: " + result12);
        }
    }
}
