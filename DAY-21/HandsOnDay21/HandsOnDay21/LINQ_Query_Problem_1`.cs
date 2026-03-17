using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product11
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product11> GetProducts()
        {
            return new List<Product11>
            {
                new Product11{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product11{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product11{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product11{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product11{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_11
    {
        static void Main(string[] args) { 
            
            Product11 product = new Product11();

            var products = product.GetProducts();

            var result11 = products.Count(p => p.ProCategory == "FMCG");
            Console.WriteLine("FMCG Product Count: " + result11);
        }
    }
}
