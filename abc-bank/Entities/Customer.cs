using System;
using System.Collections.Generic;


namespace abc_bank.Entities
{
    public class Customer
    { 
        public Customer(String name)
        {   
            this.Name = name;
            this.Accounts = new List<Account>();
        }

        public string Name { get; }

        public List<Account> Accounts { get; set; } 
    }
}
