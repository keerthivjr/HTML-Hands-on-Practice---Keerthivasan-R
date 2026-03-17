using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product5
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product5> GetProducts()
        {
            return new List<Product5>
            {
                new Product5{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product5{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product5{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product5{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product5{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_05
    {
        static void Main(string[] args) { 
        
            Product5 product = new Product5();

            //sort products in ascending order by product Mrp

            var products = product.GetProducts();

            var result = products.OrderBy(p=> p.ProMrp).ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"{item.ProMrp}\t{item.ProName}");
            }

            Console.ReadLine();
        }
    }
}
