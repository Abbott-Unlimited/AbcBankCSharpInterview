using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abc_bank
{
	public class Bank
	{
		private List<Customer> Customers = new List<Customer>();
		public void AddCustomer(Customer customer) => Customers.Add(customer);
		public string CustomerSummary()
		{
			StringBuilder summary = new StringBuilder()
				.AppendLine("Customer Summary");
			foreach (Customer c in Customers)
				summary.AppendLine(" - " + c.Name + " (" + Format(c.GetNumberOfAccounts(), "account") + ")");
			return summary.ToString();
		}
		/// <summary>
		/// Make sure correct plural of word is created based on the number passed in
		/// </summary>
		/// <returns>If number passed in is 1 just return the word otherwise add an 's' at the end</returns>
		public static string Format(int number, string word) => number + " " + (number == 1 ? word : word + "s");
		public decimal TotalInterestPaid() => Customers.Sum(c => c.TotalInterestEarned());
		public string GetFirstCustomer() => Customers?.FirstOrDefault()?.Name ?? "Error";
	}
}
