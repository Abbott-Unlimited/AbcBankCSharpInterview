using System;
namespace abc_bank.InterestCalculators
{
    public class CalculateInterestFactory
    {
        public static CalculateInterestFactory Instance;
        static CalculateInterestFactory()
        {
            if(Instance == null)
            {
                Instance = new CalculateInterestFactory();
            }
        }
        public ICalculateInterest GetNew(Account.AccountType accountType)
        {
            switch(accountType)
            {
                case Account.AccountType.Savings:
                    return new CalculateSavingsInterest();
                case Account.AccountType.MaxiSavings:
                    return new CalculateMaxiSavingsInterest();
                case Account.AccountType.Checking:
                    return new CalculateCheckingInterest();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
