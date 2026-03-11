using System;

namespace HandsOnDAY18
{
    class BankAccount
    {
        private double balance;

        // Method to deposit money
        public void Deposit(double amount)
        {
            balance += amount;
        }

        // Method to withdraw money
        public void Withdraw(double amount)
        {
            if (amount > balance)
            {
                Console.WriteLine("Insufficient Balance");
            }
            else
            {
                balance -= amount;
            }
        }

        // Method to get balance
        public double GetBalance()
        {
            return balance;
        }
    }

    class Bank_Account_with_Encapsulation
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount();

            // Sample Input
            Console.Write("Enter the amt to deposit: ");
            double deposit = Convert.ToDouble(Console.ReadLine());
            account.Deposit(deposit);
            Console.Write("Enter the amt to Withdraw: ");
            double withdraw = Convert.ToDouble(Console.ReadLine());
            account.Withdraw(withdraw);

            // Output
            Console.WriteLine("Current Balance = " + account.GetBalance());
        }
    }
}