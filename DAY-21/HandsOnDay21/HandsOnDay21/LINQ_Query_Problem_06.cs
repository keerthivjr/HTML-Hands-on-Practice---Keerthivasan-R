using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product6
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product6> GetProducts()
        {
            return new List<Product6>
            {
                new Product6{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product6{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product6{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product6{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product6{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_06
    {
        static void Main(string[] args) { 
        
            Product6 product6 = new Product6();

            //sort products in descending order by product Mrp

            var products = product6.GetProducts();

            var result = products.OrderByDescending(p=> p.ProMrp).ToList();

            foreach (var item in products)
            {
                Console.WriteLine($"{item.ProName}\t{item.ProMrp}");
            }

            Console.ReadLine();
        }
    }
}
