using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class TransactionResult
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }

        public bool Failure { get { return !Success; } }

        protected TransactionResult(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static TransactionResult Ok()
        {
            return new TransactionResult(true, String.Empty);
        }

        public static TransactionResult Fail(string message)
        {
            return new TransactionResult(false, message);
        }
    }
}
