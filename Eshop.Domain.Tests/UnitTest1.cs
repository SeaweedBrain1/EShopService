using EShop.Domain.Enums;
using Xunit;

namespace EShop.Domain.Tests
{
    public class CreditCardProviderTests
    {
        [Fact]
        public void CreditCardProvider_EnumValues_ShouldBeCorrect()
        {
            Assert.Equal(0, (int)CreditCardProvider.Visa);
            Assert.Equal(1, (int)CreditCardProvider.MasterCard);
            Assert.Equal(2, (int)CreditCardProvider.AmericanExpress);
        }
    }
}