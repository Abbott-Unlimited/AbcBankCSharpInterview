using System.Text;
using abc_bank.Entities;
using abc_bank.Enums;

namespace abc_bank.Helpers
{
    public static class StatementCalculations
    {
        public static string GetStatement(Customer customer)
        {
            StringBuilder statement = new StringBuilder();
            statement .Append( "Statement for " + customer.Name + "\n");
            double total = 0.0;
            foreach (Account a in customer.Accounts)
            {
                statement.Append( "\n" + statementForAccount(a) + "\n");
                total += InterestCalculations.SumTransactions(a);
            }
            statement.Append("\nTotal In All Accounts " + Language.ToDollars(total));
            return statement.ToString();
        }

        private static string statementForAccount(Account a)
        {
            StringBuilder s = new StringBuilder();

            //Translate to pretty account type
            switch (a.AccountType)
            {
                case AccountTypeEnum.CHECKING:
                    s.Append( "Checking Account\n");
                    break;
                case AccountTypeEnum.SAVINGS:
                    s.Append("Savings Account\n");
                    break;
                case AccountTypeEnum.MAXI_SAVINGS:
                    s.Append("Maxi Savings Account\n");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.Transactions)
            {
                s.Append("  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + Language.ToDollars(t.amount) + "\n");
                total += t.amount;
            }
            s.Append("Total " + Language.ToDollars(total));
            return s.ToString();
        }
    }
}
