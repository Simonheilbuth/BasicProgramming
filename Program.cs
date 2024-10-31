using System;

namespace FinancialManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("Simon", 1000);
            const int monthsInYear = 12;

            while (true)
            {
                Console.WriteLine("\nFinancial Management System");
                Console.WriteLine("Choose a service:");
                Console.WriteLine("1. Bank Account Management");
                Console.WriteLine("2. Currency Converter");
                Console.WriteLine("3. Monthly Budget Calculator");
                Console.WriteLine("4. Exit");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunBankSimulator(account);
                        break;
                    case "2":
                        RunCurrencyConverter();
                        break;
                    case "3":
                        RunMonthlyBudget(monthsInYear);
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using the Financial Management System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RunBankSimulator(BankAccount account)
        {
            while (true)
            {
                Console.WriteLine("\nBank Account Management:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Return to Main Menu");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Current Balance: {account.GetBalance():C}");
                        break;
                    case "2":
                        Console.Write("Enter amount to deposit: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                        {
                            account.Deposit(depositAmount);
                            Console.WriteLine("Deposit successful.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter amount to withdraw: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                        {
                            if (account.Withdraw(withdrawAmount))
                            {
                                Console.WriteLine("Withdrawal successful.");
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void RunCurrencyConverter()
        {
            while (true)
            {
                Console.WriteLine("\nCurrency Converter:");
                Console.WriteLine("1. Convert USD to CAD");
                Console.WriteLine("2. Convert CAD to USD");
                Console.WriteLine("3. Return to Main Menu");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter amount in USD: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal usdInput))
                        {
                            decimal cadResult = CurrencyConverter.ConvertUsdToCad(usdInput);
                            Console.WriteLine($"CAD Amount: {cadResult:C}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter amount in CAD: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal cadInput))
                        {
                            decimal usdResult = CurrencyConverter.ConvertCadToUsd(cadInput);
                            Console.WriteLine($"USD Amount: {usdResult:C}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void RunMonthlyBudget(int monthsInYear)
        {
            double[] monthlyExpenses = GetMonthlyExpenses(monthsInYear);
            double averageExpense = CalculateAverage(monthlyExpenses);
            Console.WriteLine($"\nYour average monthly expense is: {averageExpense:C}");
        }

        static double[] GetMonthlyExpenses(int monthsInYear)
        {
            double[] expenses = new double[monthsInYear];
            Console.WriteLine("\nEnter your monthly expenses:");

            for (int i = 0; i < monthsInYear; i++)
            {
                while (true)
                {
                    Console.Write($"Month {i + 1}: ");
                    if (double.TryParse(Console.ReadLine(), out expenses[i]))
                    {
                        break;
                    }
                    Console.WriteLine("Invalid amount. Please enter a valid number.");
                }
            }

            return expenses;
        }

        static double CalculateAverage(double[] expenses)
        {
            double total = 0;
            foreach (double expense in expenses)
            {
                total += expense;
            }
            return total / expenses.Length;
        }
    }

    public class BankAccount
    {
        public string Owner { get; }
        private decimal balance;

        public BankAccount(string owner, decimal initialBalance)
        {
            Owner = owner;
            balance = initialBalance;
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                balance += amount;
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                return true;
            }
            return false;
        }
    }

    internal static class CurrencyConverter
    {
        private const decimal UsdToCadRate = 1.25m;
        private const decimal CadToUsdRate = 0.80m;

        public static decimal ConvertUsdToCad(decimal usdInput)
        {
            return usdInput * UsdToCadRate;
        }

        public static decimal ConvertCadToUsd(decimal cadInput)
        {
            return cadInput * CadToUsdRate;
        }
    }
}