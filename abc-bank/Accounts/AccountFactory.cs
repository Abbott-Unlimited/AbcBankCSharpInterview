using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class AccountFactory
    {
        public static Account OpenAccount(AccountTypes accountType)
        {
            Account account;

            switch (accountType)
            {
                case AccountTypes.CHECKING:
                    account =  new CheckingAccount();
                    break;
                case AccountTypes.SAVINGS:
                    account =  new SavingsAccount();
                    break;
                case AccountTypes.MAXI_SAVINGS:
                    account =  new MaxiSavingsAccount();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Account type {accountType} not available.");
            }

            return account;
        }
    }
}
