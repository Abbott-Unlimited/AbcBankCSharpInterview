using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    /// <summary>
    ///   Account Class
    /// </summary>
    public class Account
    {
        // Account types
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        // Checking Account Rate
        private const double CheckingInterestRate = 0.001;

        // Savings Account Rates
        private const double SavingsInterestRateFirstThousand = 0.001;
        private const double SavingsInterestRateAboveThousand = 0.002;

        // Maxi Savings Account Rates
        private const double MaxiInterestRateFirstThousdand = 0.02;
        private const double MaxiInterestRateAboveThousand = 0.05;
        private const double MaxiInterestRateAboveTwoThousand = 0.01;
        private const double MaxiInterestRateNoWD10Days = 0.05;
        private const double MaxiInterestRateWDPast10Days = 0.001;


        private readonly int accountType;
        public List<Transaction> transactions;

        /// <summary>Initializes a new instance of the <see cref="Account" /> class.</summary>
        /// <param name="accountType">Type of the account.</param>
        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        /// <summary>Deposits the specified amount.</summary>
        /// <param name="amount">The amount.</param>
        /// <exception cref="System.ArgumentException">amount must be greater than zero</exception>
        public void Deposit(double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        /// <summary>Withdraws the specified amount.</summary>
        /// <param name="amount">The amount.</param>
        /// <exception cref="System.ArgumentException">amount must be greater than zero</exception>
        public void Withdraw(double amount)
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        /// <summary>Calculates the Interest earned.</summary>
        /// <returns>
        ///   The Interest calculated.
        /// </returns>
        public double InterestEarned()
        {
            double amount = sumTransactions();
            switch(accountType){
                case CHECKING:
                    return amount * CheckingInterestRate;
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * SavingsInterestRateFirstThousand;
                    else
                        return (SavingsInterestRateFirstThousand * 1000) + (amount-1000) * SavingsInterestRateAboveThousand;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                        //if (amount <= 1000)
                            //return amount * MaxiInterestRateFirstThousdand;
                        //if (amount <= 2000)
                            //return (MaxiInterestRateFirstThousdand * 1000) + (amount - 1000) * MaxiInterestRateAboveThousand;
                        //return (((MaxiInterestRateFirstThousdand * 1000) + (amount - 1000) * MaxiInterestRateAboveThousand) + (amount - 2000) * MaxiInterestRateAboveTwoThousand);


                    if (MaxiDaysGreaterThan(10)) // if there are no withdrawals are more than 10 days ago then calculate the Interested at 5%.
                        return amount * MaxiInterestRateNoWD10Days;
                    else
                        return amount * MaxiInterestRateWDPast10Days;  // if the withdrawals are within the last 10 days then calculate the Interest at 0.1%.
                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        /// <summary>Checks if transactions exist.</summary>
        /// <param name="checkAll">if set to <c>true</c> [check all].</param>
        /// <returns>
        ///   The amount
        /// </returns>
        private double CheckIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType()
        {
            return accountType;
        }

        /// <summary>Checks the transactions to verify the days since the transaction was entered.</summary>
        /// <param name="maxDays">The maximum days.</param>
        /// <returns>
        ///   True or False
        /// </returns>
        public bool MaxiDaysGreaterThan(int maxDays = 10)
        {
            int days = 0;
            bool over10Days = false;

            foreach (Transaction t in transactions)
            {
                days = t.DaysSinceTransaction();

                if (t.amount < 0)
                {
                    if (days >= maxDays)
                    {
                        over10Days = true;
                    }
                    else
                    {
                        over10Days = false;
                        break;
                    }
                }
                else
                {
                    // this is a deposit and we aren't concerned with the transaction day being over/under the max days.
                    over10Days = true;
                }

            }
            return (over10Days);
        }

    }
}
