using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    public class Product4
    {
        public int ProCode { get; set; }
        public string ProName { get; set; }
        public string ProCategory { get; set; }
        public double ProMrp { get; set; }

        public List<Product4> GetProducts()
        {
            return new List<Product4>
            {
                new Product4{ ProCode=101, ProName="Soap", ProCategory="FMCG", ProMrp=25 },
                new Product4{ ProCode=102, ProName="Shampoo", ProCategory="FMCG", ProMrp=45 },
                new Product4{ ProCode=103, ProName="Rice", ProCategory="Grain", ProMrp=60 },
                new Product4{ ProCode=104, ProName="Wheat", ProCategory="Grain", ProMrp=40 },
                new Product4{ ProCode=105, ProName="Oil", ProCategory="FMCG", ProMrp=120 }
            };
        }
    }
    internal class LINQ_Query_Problem_04
    {
        static void Main(string[] args) { 
        
            Product4 product = new Product4();

            //sort products in ascending order by product Category

            var products = product.GetProducts();

            var result = products.OrderBy(p => p.ProCategory);

            foreach (var item in result) {

                Console.WriteLine($"{item.ProCategory}\t{item.ProName}");
            }

            Console.ReadLine();
        }
    }
}
