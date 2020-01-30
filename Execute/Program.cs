using System;
using abc_bank;
using abc_bank_tests;

namespace testing_my_features
{
    class Program
    {
        static void Main(string[] args)
        {
            //BankTest t = new BankTest();
            //t.Maxi_savings_account();
            //double d = 8.8977;
            //string df = String.Format("{0:C2}", d);

            // Dummy Account/Customer Setup
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            Customer bronson = new Customer("Bronson").OpenAccount(checkingAccount).OpenAccount(savingsAccount).OpenAccount(maxiSavingsAccount);
            checkingAccount.Deposit(10000.0);
            savingsAccount.Deposit(40000.0);
            savingsAccount.Withdraw(200.0);
            maxiSavingsAccount.Deposit(90000);

            // Output Customer Statement
            Console.WriteLine(bronson.GetStatement());

            // Test; Maxi Savings Interest
            foreach (Account acc in bronson.GetAccounts()) {
                if (acc.GetAccountType() == 2) {
                    double interest = acc.InterestEarned(acc);
                    string interestStr = bronson.ToDollars(interest);
                    Console.WriteLine("Interest earned on Maxi Savings: " + interestStr);
                } 
            }
            
            // Test; Transfer
            checkingAccount.Transfer(checkingAccount, savingsAccount, 5000);
        }
    }
}





