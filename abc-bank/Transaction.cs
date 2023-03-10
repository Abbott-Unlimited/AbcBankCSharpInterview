using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Transaction
    {
        public double Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Type { get { return Amount > 0 ? "deposit" : "withdrawal"; } }
        public Transaction(double amount) 
        {
            Amount = amount;
            Date = DateProvider.Now;
        }
    }
}
