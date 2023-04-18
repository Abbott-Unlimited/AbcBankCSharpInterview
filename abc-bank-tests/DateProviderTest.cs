using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace abc_bank_tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DateProviderTest
    {
        [TestMethod]
        public void DateProvider()
        {
            // Arrange
            var dateProvider = abc_bank.DateProvider.getInstance();

            // Act

            // Assert
            Assert.IsTrue(dateProvider.GetType() == typeof(DateProvider));
        }
    }
}
