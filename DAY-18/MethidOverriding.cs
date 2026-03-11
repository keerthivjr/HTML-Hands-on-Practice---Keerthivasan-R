using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsOnDAY18
{
    class Parent
    {
        public void Display()
        {
            Console.WriteLine("Parent class method");
        }
    }
    class Child : Parent
    {
        public void Display()
        {
            Console.WriteLine("Child class method");
        }
    }
    internal class MethidOverriding
    {
        static void Main(string[] args)
        {
            Parent p = new Parent();
            p.Display();
            Child c = new Child();
            c.Display();
            Console.ReadLine();
        }
    }
}
