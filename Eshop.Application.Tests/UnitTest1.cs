using EShop.Application.Services;
using EShop.Domain.Exceptions;
using Xunit;

namespace EShop.Application.Tests
{
    public class CardValidatorServiceTests
    {
        private readonly CardValidatorService _cardValidator;

        public CardValidatorServiceTests()
        {
            _cardValidator = new CardValidatorService();
        }

        [Theory]
        [InlineData("3497 7965 8312 797", true)] // American Express - valid
        [InlineData("4532 2080 2150 4434", true)] // Visa - valid
        [InlineData("5551561443896215", true)] // MasterCard - valid
        [InlineData("1234 5678 9012 345", false)] // Invalid number
        [InlineData("4024-0071-6540-1778", true)] // Visa with hyphens
        [InlineData("0000 0000 0000 0000", false)] // All zeros
        [InlineData("", false)] // Empty string
        [InlineData("   ", false)] // Only spaces
        [InlineData("123456789012", false)] // Too short (12 digits)
        [InlineData("12345678901234567890", false)] // Too long (20 digits)
        [InlineData("4012 8888 8888 1881", true)] // Valid Visa number
        [InlineData("5105 1051 0510 5100", true)] // Valid MasterCard number
        public void ValidateCard_ShouldReturnExpectedResult(string cardNumber, bool expected)
        {
            var result = _cardValidator.ValidateCard(cardNumber);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("3497 7965 8312 797", "American Express")]
        [InlineData("345-470-784-783-010", "American Express")]
        [InlineData("378523393817437", "American Express")]
        [InlineData("4024-0071-6540-1778", "Visa")]
        [InlineData("4532 2080 2150 4434", "Visa")]
        [InlineData("4532289052809181", "Visa")]
        [InlineData("5530016454538418", "MasterCard")]
        [InlineData("5551561443896215", "MasterCard")]
        [InlineData("5131208517986691", "MasterCard")]
        [InlineData("6011 2345 6789 0123", "Discover")]
        [InlineData("3528 1234 5678 9012", "JCB")]
        [InlineData("3056 1234 5678 90", "Diners Club")]
        [InlineData("5018 1234 5678 9012", "Maestro")]
        [InlineData("5020-1234-5678-9012", "Maestro")]
        [InlineData("1234 5678 9012 3456", "Unknown")] // Unknown card
        [InlineData("0000 0000 0000 0000", "Unknown")] // All zeros
        [InlineData("", "Unknown")] // Empty card number
        [InlineData("   ", "Unknown")] // Only spaces
        [InlineData("123456789012", "Unknown")] // Too short
        [InlineData("12345678901234567890", "Unknown")] // Too long
        public void GetCardType_ShouldReturnCorrectType(string cardNumber, string expected)
        {
            var result = _cardValidator.GetCardType(cardNumber);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CreditCardValidator_ThrowsTooShortException()
        {
            // Arrange
            var cardValidatorService = new CardValidatorService();

            // Act & Assert
            Assert.Throws<CardNumberTooShortException>(() => cardValidatorService.ValidateCard("123123"));
        }

        [Fact]
        public void CreditCardValidator_ThrowsTooLongException()
        {
            // Arrange
            var cardValidatorService = new CardValidatorService();

            // Act & Assert
            Assert.Throws<CardNumberTooLongException>(() => cardValidatorService.ValidateCard("123456789012345678901"));
        }

        [Fact]
        public void CreditCardValidator_ThrowsInvalidException()
        {
            // Arrange
            var cardValidatorService = new CardValidatorService();

            // Act & Assert
            Assert.Throws<CardNumberInvalidException>(() => cardValidatorService.ValidateCard("1234 5678 9012 3456"));
        }
    }
}