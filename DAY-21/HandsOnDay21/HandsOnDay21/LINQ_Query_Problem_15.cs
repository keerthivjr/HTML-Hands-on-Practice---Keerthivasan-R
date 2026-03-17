using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product15
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product15> GetProducts()
        {
            return new List<Product15>
            {
                new Product15{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product15{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product15{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product15{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product15{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_15
    {
        static void Main(string[] args) { 
            
            Product15 product =new Product15();

            var products = product.GetProducts();

            var result15 = products.Any(p => p.ProMrp < 30);
            Console.WriteLine("Any product below 30: " + result15);

        }
    }
}
