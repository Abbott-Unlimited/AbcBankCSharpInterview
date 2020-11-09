using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer : ICustomer
    {

        #region local variable declaration

        private String name;
        private List<Account> accounts;


        #endregion
        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }


        #region GetName Method
        public String GetName()
        {
            return name;
        }
        #endregion

        #region OpenAccount Method
        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }
        #endregion
        #region GetNumberOfAccounts Method
        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }
        #endregion

        #region TotalInterestEarned Method
        public double TotalInterestEarned()
        {
            double total = 0;
            foreach (Account a in accounts)
                total = total + a.InterestEarned();
            return total;
        }

        #endregion


        #region GetStatement Method
        public String GetStatement()
        {

            StringBuilder sb = new StringBuilder();
            String statement = null;

            sb.Append("Statement for " + name + "\n");
            double total = 0.0;
            foreach (Account a in accounts)
            {
                sb.Append("\n" + statementForAccount(a) + "\n");
                total = total + a.sumTransactions();
            }
            sb.Append("\n" + "Total In All Accounts " + ToDollars(total));
            statement = sb.ToString();


            return statement;
        }

        #endregion


        #region statementForAccount Method
        private String statementForAccount(Account a)
        {
            string statement = string.Empty;
            StringBuilder sb = new StringBuilder();

            //Translate to pretty account type
            switch (a.GetAccountType())
            {
                case Account.CHECKING:
                    sb.Append("Checking Account\n");
                    break;
                case Account.SAVINGS:
                    sb.Append("Savings Account\n");
                    break;
                case Account.MAXI_SAVINGS:
                    sb.Append("Maxi Savings Account\n");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions)
            {
                string tamount = string.Empty;
                tamount = ToDollars(t.amount);
                char[] charsToTrim = { '(', ')' };
              
                string famount = tamount.Trim(charsToTrim);


                sb.Append("  ");
                if (t.amount < 0)
                {
                    sb.Append("withdrawal");

                }
                else
                {
                    sb.Append("deposit");


                }
                sb.Append(" ");
                sb.Append(famount + "\n");
           
                total = total + t.amount;


            }
            sb.Append("Total " + ToDollars(total));

            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                statement = sb.ToString();
            }
            return statement;
        }
        #endregion

        #region ToDollars
        private String ToDollars(double d)
        {
            string formattedBalance = String.Empty;
            formattedBalance = d.ToString("C", CultureInfo.CurrentCulture);
            return formattedBalance;

        }

        #endregion
    }
}
