using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product14
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product14> GetProducts()
        {
            return new List<Product14>
            {
                new Product14{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product14{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product14{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product14{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product14{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_14
    {
        static void Main(string[] args) { 
            
            Product14 product = new Product14();

            var products = product.GetProducts();

            var result14 = products.All(p => p.ProMrp < 30);
            Console.WriteLine("All products below 30: " + result14);
        }
    }
}
