using System;
using System.Runtime.Serialization;

namespace abc_bank
{
    public class BankAppException : Exception
    {
        protected BankAppException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        public BankAppException(string message, Exception innerException)
            : base(message, innerException) { }
        public BankAppException(string message)
            : base(message) { }
        public BankAppException()
            : base() { }
    }
}