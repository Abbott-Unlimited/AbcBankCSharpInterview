using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank;
using abc_bank_tests;

namespace Application
{
	internal class Program
	{
		static void Main(string[] args)
		{
			CustomerTest customerTest = new CustomerTest();
			customerTest.TestApp();
			customerTest.TestOneAccount();
			customerTest.TestTwoAccount();
			customerTest.TestThreeAccounts();
			customerTest.TestTransferFunds();

			BankTest bankTest = new BankTest();
			bankTest.CustomerSummary();
			bankTest.CheckingAccount();
			bankTest.Maxi_savings_account();
			bankTest.Savings_account();
			bankTest.GetAllCustomers();

			TransactionTest transactionTest = new TransactionTest();
			transactionTest.Transaction();
		}
	}
}
