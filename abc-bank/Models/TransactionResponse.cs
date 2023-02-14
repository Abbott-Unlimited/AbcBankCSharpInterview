using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Models
{
    public class TransactionResponse
    {
        public string Message;
        public bool Success;

        public TransactionResponse(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}
