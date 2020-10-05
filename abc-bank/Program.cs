using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    class Program
    {
        //creates bank
        static Bank bank = new Bank();

        static Customer customer;
        //makes a dictionary to hold all the accountTypes
        static Dictionary<int, Account> accountType = new Dictionary<int, Account>(){
            { 1, new Account(Account.CHECKING) },
            { 2, new Account(Account.SAVINGS) },
            { 3, new Account(Account.MAXI_SAVINGS) } };
        static void Main()
        {


            //asks for the user name and makes the first customer
            Console.Write("Welcome please enter your name: ");
            string name = Console.ReadLine();
            customer = new Customer(name);

            //starts a loop of selections
            bool stillChoosing = true;
            bool isAddedToBank = false;

            //is account created or not
            bool checkingsCreated = false,
                 savingsCreated = false,
                 maxiSavingsCreated = false;
            do
            {
                Console.WriteLine("What would you like to open:\n" +
                                  (!checkingsCreated?"1)Create Checkings account\n": "1)Make changes to Checkings Account\n") +
                                  (!savingsCreated ? "2)Create Savings account\n": "2)Make changes to Savings account\n") +
                                  (!maxiSavingsCreated ? "3)Create Maxi Savings account\n": "3)Make changes to Maxi Savings account\n") +
                                  (!isAddedToBank ? "4)Add {0} to bank\n" : "4)Apply Changes\n") +
                                  "5)Get Customer Summary\n" +
                                  "6)Get {0}'s Statement\n" +
                                  "9)Close Program"
                                  , name);

                if (int.TryParse(Console.ReadLine(), out int selection) && selection <= 9)
                {
                    switch (selection) {
                        case (1):
                            if (!checkingsCreated)
                            {
                                CreateAccount(selection);
                                checkingsCreated = true;
                                break;
                            }
                            CheckingsShinanagions();
                            break;
                        case (2):
                            if (!savingsCreated)
                            {
                                CreateAccount(selection);
                                savingsCreated = true;
                                break;
                            }


                            break;
                        case (3):
                            if (!maxiSavingsCreated)
                            {
                                CreateAccount(selection);
                                maxiSavingsCreated = true;
                                break;
                            }
                            break;
                        case 4:
                            if (!isAddedToBank)
                            {
                                bank.AddCustomer(customer);
                                isAddedToBank = true;
                            }
                            else
                            {
                                bank.AddChanges(customer, customer.OpenAccount(accountType[selection]));
                            }
                            break;
                        case 5:
                            Console.WriteLine(bank.CustomerSummary());
                            break;
                        case 6:
                            Console.WriteLine(customer.GetStatement());
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Choice please try again");
                }


            } while (stillChoosing);



            Console.ReadLine();

        }
        //will create an account with the selected type
        public static void CreateAccount(int selection)
        {
            customer.OpenAccount(accountType[selection]);
        }

        public static void CheckingsShinanagions()
        {                                
            Account checkings = customer.GetAccount(Account.CHECKING);
            Console.WriteLine("\n\n1)Deposit\n2)Withdraw\n3)Transfer");
            bool error = false;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int action))
                {
                    switch (action)
                    {
                        case (1):
                            Console.Write("How much would you like to deposit: ");
                            if (double.TryParse(Console.ReadLine(), out double amountToDeposit))
                            {
                                checkings.Deposit(amountToDeposit);
                            }
                            else error = true;
                            break;
                        case (2):
                            Console.WriteLine("How much would you like to Withdraw");
                            if (double.TryParse(Console.ReadLine(), out double amountToWithdraw))
                            {
                                checkings.Withdraw(amountToWithdraw);
                            }
                            else error = true;
                            break;
                        case 3:
                            Console.Write("Where would you like to transfer this money to?");
                            break;
                        default:
                            error = true;
                            break;
                    }
                }
                else error = true;

                if (error)
                    Console.WriteLine("Invalid choice");
            } while (!error);
        }
    }
}
