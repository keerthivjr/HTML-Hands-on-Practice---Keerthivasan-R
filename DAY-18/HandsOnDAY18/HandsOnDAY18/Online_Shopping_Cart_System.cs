using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDAY18
{
    class Product {
        //private fields
        private string name;
        private double price;

        //property for name
        public string Name {
            get { return name; }
            set { name = value; }
        }
        //property for price
        public double Price {
            get { return price; }
            set {
                if (value < 0)
                {
                    Console.WriteLine("Price cannot be negative");
                }
                else { 
                    price = value;
                }
            }
        }
        public virtual double CalculateDiscount() { 
            return price;        
        }
    }
    class Electronics : Product
    {
        public override double CalculateDiscount()
        {
            return Price - (Price * 0.05);
        }
    }

    class Clothing : Product
    {
        public override double CalculateDiscount()
        {
            return Price - (Price * 0.15);
        }
    }
    internal class Online_Shopping_Cart_System
    {
        static void Main(string[] args) { 
            
            //polymorphism
            Product electronicProduct = new Electronics();
            
            Console.Write("Enter the product Name: ");
            string name = Console.ReadLine();
            electronicProduct.Name = name;

            Console.Write("Enter the product Price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            electronicProduct.Price = price;

            double finalPrice = electronicProduct.CalculateDiscount();

            Console.WriteLine($"The final price of the electronic product {electronicProduct.Name} after discount is: {finalPrice}");

            Product clothingProduct = new Clothing();
            Console.Write("Enter the Clothing type: ");
            string type = Console.ReadLine();

            Console.Write("Enter the product Price: ");
            double clothingPrice = Convert.ToDouble(Console.ReadLine());
            clothingProduct.Price = clothingPrice;

            double finalClothingPrice = clothingProduct.CalculateDiscount();

            Console.WriteLine("The final price of the clothing product {0} after discount is: {1}", type, finalClothingPrice);
        }
    }
}
