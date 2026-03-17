using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product1
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product1> GetProducts()
        {
            return new List<Product1>
            {
                new Product1{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product1{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product1{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product1{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product1{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_01
    {
        static void Main(string[] args) {

            Product1 product = new Product1();

            var products = product.GetProducts();

            var result = products.Where(p => p.ProCategory == "FMCG").ToList();

            foreach (var item in result) { 
                
                Console.WriteLine($"{item.ProCode}\t{item.ProName}\t{item.ProMrp}");
            }
            Console.ReadLine();
        }
    }
}
