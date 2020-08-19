using System.ComponentModel;

namespace abc_bank
{
    public enum AccountType
    {
        [Description("Checking")]
        CHECKING,
        [Description("Savings")]
        SAVINGS,
        [Description("Maxi Savings")]
        MAXI_SAVINGS
    }
}