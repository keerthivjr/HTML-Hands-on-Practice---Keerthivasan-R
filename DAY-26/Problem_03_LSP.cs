using System;

namespace HandsOnDAY26
{
    // 1. Base Class
    public abstract class Shape
    {
        public abstract double CalculateArea();
    }

    // 2. Rectangle Class
    public class Rectangle : Shape
    {
        public double Length { get; set; }
        public double Width { get; set; }

        public Rectangle(double length, double width)
        {
            Length = length;
            Width = width;
        }

        public override double CalculateArea()
        {
            return Length * Width;
        }
    }

    // 3. Circle Class
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    // Main Class
    internal class Problem_03_LSP
    {
        // Method accepting Shape (LSP in action)
        public static void PrintArea(Shape shape)
        {
            Console.WriteLine("Area: " + shape.CalculateArea());
        }

        static void Main(string[] args)
        {
            // Rectangle object
            Shape rectangle = new Rectangle(10, 5);
            PrintArea(rectangle);

            // Circle object
            Shape circle = new Circle(7);
            PrintArea(circle);
        }
    }
}