using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class TransferFund
    {
        private decimal balance;
        private bool isValid;
        public decimal GetAccountBalance(string accountNumber)
        {
            //Get the account balance and other info


            // Return the account balance
            return balance;
        }
        public bool TransferFunds(string sourceAccountNumber, string destinationAccountNumber, decimal amount)
        {
            // Check if the source account has sufficient balance
            decimal sourceBalance = GetAccountBalance(sourceAccountNumber);
            if (sourceBalance < amount)
            {
                // Insufficient balance, transfer cannot be completed
                return false;
            }

            // Deduct the amount from the source account


            // Add the amount to the destination account


            // Transfer successful
            return true;
        }
        public bool ValidateAccount(string accountNumber)
        {
            // Check if the account number exists in the banking system


            // Return true if the account is valid, false otherwise
            return isValid;
        }
    }
}
