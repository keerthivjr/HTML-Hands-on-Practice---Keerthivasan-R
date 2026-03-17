using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product2
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product2> GetProducts()
        {
            return new List<Product2>
            {
                new Product2{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product2{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product2{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product2{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product2{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_02
    {
        static void Main(string[] args) {

            Product2 product2 = new Product2();

            //products with category GRAIN
            var products = product2.GetProducts();

            var result = products.Where(p=> p.ProCategory=="Grain").ToList();

            foreach (var item in result) {
                Console.WriteLine($"{item.ProCode}\t{item.ProName}\t{item.ProMrp}");
            }

            Console.ReadLine();

        }
    }
}
