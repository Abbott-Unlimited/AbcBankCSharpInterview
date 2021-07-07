using System.Collections.Generic;

namespace abc_bank.Entities
{
    public class Bank
    {
        public Bank()
        {
            this.Customers = new List<Customer>();
        }
        public List<Customer> Customers { get; set; }
    }
}
