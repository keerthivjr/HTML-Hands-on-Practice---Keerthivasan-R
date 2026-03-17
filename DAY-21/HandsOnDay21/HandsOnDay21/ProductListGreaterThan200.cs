using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDay21
{
    class Product0 { 
        public string Name { get; set; }
        public int Price { get; set; }
    }
    internal class ProductListGreaterThan200
    {
        static void Main(string[] args) {

            //create product list
            List<Product0> products = new List<Product0>()
            {
            new Product0 { Name = "Pen", Price = 50 },
            new Product0 { Name = "Notebook", Price = 120 },
            new Product0 { Name = "Headphones", Price = 500 },
            new Product0 { Name = "Keyboard", Price = 800 },
            new Product0 { Name = "Mouse", Price = 150 }
        };


            Console.WriteLine("Product with price > 200:");

            foreach (Product0 p in products)
            {
                if (p.Price > 200) {
                    Console.WriteLine(p.Name + " - " + p.Price);                
                }
            }
        }
    }
}
