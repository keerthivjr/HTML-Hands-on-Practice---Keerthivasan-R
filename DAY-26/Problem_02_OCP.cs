using System;
using System.Collections.Generic;

namespace HandsOnDAY26
{
    // 1. Interface
    public interface IDiscountStrategy
    {
        double CalculateDiscount(double amount);
    }

    // 2. Discount Classes

    public class RegularCustomerDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount)
        {
            return amount * 0.05; // 5% discount
        }
    }

    public class PremiumCustomerDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount)
        {
            return amount * 0.10; // 10% discount
        }
    }

    public class VipCustomerDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount)
        {
            return amount * 0.20; // 20% discount
        }
    }

    // 3. Price Calculator (uses strategy)
    public class PriceCalculator
    {
        private IDiscountStrategy _discountStrategy;

        public PriceCalculator(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }

        public double GetFinalPrice(double amount)
        {
            double discount = _discountStrategy.CalculateDiscount(amount);
            return amount - discount;
        }
    }

    // Main Class
    internal class Problem_02_OCP
    {
        static void Main(string[] args)
        {
            double amount = 1000;

            // Regular Customer
            PriceCalculator regular = new PriceCalculator(new RegularCustomerDiscount());
            Console.WriteLine("Regular Customer Final Price: " + regular.GetFinalPrice(amount));

            // Premium Customer
            PriceCalculator premium = new PriceCalculator(new PremiumCustomerDiscount());
            Console.WriteLine("Premium Customer Final Price: " + premium.GetFinalPrice(amount));

            // VIP Customer
            PriceCalculator vip = new PriceCalculator(new VipCustomerDiscount());
            Console.WriteLine("VIP Customer Final Price: " + vip.GetFinalPrice(amount));
        }
    }
}