using System;

namespace HandsOnDAY18 {
    class BankAcc
    {
        // Private fields
        private string accountNumber;
        private double balance;

        // Property for Account Number
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        // Property for Balance (Read Only outside)
        public double Balance
        {
            get { return balance; }
            private set { balance = value; }
        }

        // Deposit Method
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount");
                return;
            }

            Balance += amount;
            Console.WriteLine("Amount Deposited: " + amount);
            Console.WriteLine("Current Balance = " + Balance);
        }

        // Withdraw Method
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount");
                return;
            }

            if (amount > Balance)
            {
                Console.WriteLine("Insufficient Balance");
                return;
            }

            Balance -= amount;
            Console.WriteLine("Amount Withdrawn: " + amount);
            Console.WriteLine("Current Balance = " + Balance);
        }
    }

    class Bank_Account_Management_System
    {
        static void Main()
        {
            BankAcc acc = new BankAcc();

            acc.AccountNumber = "ACC1001";

            acc.Deposit(5000);
            acc.Withdraw(2000);

            Console.WriteLine("Final Balance = " + acc.Balance);
        }
    }
}