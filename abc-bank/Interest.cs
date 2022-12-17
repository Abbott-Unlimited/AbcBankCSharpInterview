using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    /// <summary>
    /// Interest class is used to Calculate Interest for provided account.
    /// Class is made for maintaining Seperate of Concerns principle.
    /// </summary>
    public static class Interest
    {
        #region | InterestEarned |
        public static double InterestEarned(Account account)
        {
            //double amount = account.SumTransactions();
            double amount = account.balance;

            switch (account.GetAccountType())
            {
                case Account.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;

                //case SUPER_SAVINGS:
                //    if (amount <= 4000)
                //        return 20;

                case Account.MAXI_SAVINGS:

                    #region | Commented Exiting Functionality |
                    //if (amount <= 1000)
                    //    return amount * 0.02;
                    //if (amount <= 2000)
                    //    return 20 + (amount - 1000) * 0.05;
                    //return 70 + (amount - 2000) * 0.1;
                    #endregion

                    //  Integrated: interest rate of 5% assuming 
                    //  no withdrawals in the past 10 days 
                    //  otherwise 0.1%.
                    if (account.CheckIfLastWithdrawalExistsAfterDays(10))
                        return amount * 0.001;
                    else
                        return amount * 0.05;

                default:
                    return amount * 0.001;
            }
        }
        #endregion
    }
}
