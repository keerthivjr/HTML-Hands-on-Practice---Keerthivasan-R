using System;

namespace HandsOnDAY18
{
    class Vehicle
    {
        // Private fields
        private string brand;
        private double rentalRatePerDay;

        // Property for Brand
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        // Property for RentalRatePerDay with validation
        public double RentalRatePerDay
        {
            get { return rentalRatePerDay; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Rental rate cannot be negative");
                }
                else
                {
                    rentalRatePerDay = value;
                }
            }
        }

        // Virtual method
        public virtual double CalculateRental(int days)
        {
            return RentalRatePerDay * days;
        }
    }

    class Car : Vehicle
    {
        public override double CalculateRental(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("Invalid number of days");
                return 0;
            }

            double total = RentalRatePerDay * days;
            return total + 500;
        }
    }

    class Bike : Vehicle
    {
        public override double CalculateRental(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("Invalid number of days");
                return 0;
            }

            double total = RentalRatePerDay * days;
            return total - (total * 0.05);
        }
    }

    internal class Vehicle_Rental_System
    {
        static void Main(string[] args)
        {
            Vehicle car = new Car();
            car.Brand = "Toyota";
            car.RentalRatePerDay = 2000;

            int days = 3;

            double totalRental = car.CalculateRental(days);

            Console.WriteLine("Total Rental = " + totalRental);
        }
    }
}