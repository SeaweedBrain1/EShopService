using EShop.Application.Services;
using EShop.Domain.Exceptions;

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
        [InlineData("3497 7965 8312 797", true)] // American Express - poprawny
        [InlineData("4532 2080 2150 4434", true)] // Visa - poprawny
        [InlineData("5551561443896215", true)] // MasterCard - poprawny
        [InlineData("1234 5678 9012 345", false)] // Niepoprawny numer
        [InlineData("4024-0071-6540-1778", true)] // Visa z myœlnikami
        [InlineData("0000 0000 0000 0000", false)] // Same zera
        [InlineData("", false)] // Pusty string
        [InlineData("   ", false)] // Tylko spacje
        [InlineData("123456789012", false)] // Za krótki (12 cyfr)
        [InlineData("12345678901234567890", false)] // Za d³ugi (20 cyfr)
        [InlineData("4012 8888 8888 1881", true)] // Poprawny numer Visa
        [InlineData("5105 1051 0510 5100", true)] // Poprawny numer MasterCard
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
        [InlineData("1234 5678 9012 3456", "Unknown")] // Nieznana karta
        [InlineData("0000 0000 0000 0000", "Unknown")] // Same zera
        [InlineData("", "Unknown")] // Pusty numer karty
        [InlineData("   ", "Unknown")] // Same spacje
        [InlineData("123456789012", "Unknown")] // Za krótki
        [InlineData("12345678901234567890", "Unknown")] // Za d³ugi
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
    }
}