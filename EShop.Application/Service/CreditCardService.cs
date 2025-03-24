using EShop.Domain.Enums;
using EShop.Domain.Exceptions;

namespace EShop.Application.Service
{
    public class CreditCardService
    {
        public bool ValidateCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new CardNumberInvalidException();

            var cleanedCardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (cleanedCardNumber.Length < 13)
                throw new CardNumberTooShortException();

            if (cleanedCardNumber.Length > 19)
                throw new CardNumberTooLongException();

            if (!IsValidLuhn(cleanedCardNumber))
                throw new CardNumberInvalidException();

            return true;
        }

        public string GetCardType(string cardNumber)
        {
            var cleanedCardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (cleanedCardNumber.StartsWith("4"))
                return CreditCardProvider.Visa.ToString();
            if (cleanedCardNumber.StartsWith("5"))
                return CreditCardProvider.MasterCard.ToString();
            if (cleanedCardNumber.StartsWith("3"))
                return CreditCardProvider.AmericanExpress.ToString();

            throw new CardNumberInvalidException();
        }

        private bool IsValidLuhn(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                        n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }
            return sum % 10 == 0;
        }
    }
}