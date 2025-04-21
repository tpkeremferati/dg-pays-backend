using Dg.Pays.Domain.Exceptions;

namespace Dg.Pays.Domain.ValueObjects
{
    public class CardSchemeType
    {
        public string Name { get; private set; }

        public CardSchemeType(string name)
        {
            Name = name;
        }

        public static CardSchemeType FromCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 6)
            {
                throw new InvalidCardNumberException("The card number is not long enough");
            }

            var firstNumbers = cardNumber[..6];

            // Visa: Starts with 4
            if (cardNumber.StartsWith("4"))
            {
                return new CardSchemeType("Visa");
            }

            // Mastercard: First Numbers ranges between 51-55 or 2221 or 2221-2720
            if (int.TryParse(firstNumbers.AsSpan(0, 2), out var firstTwoDigits) && (firstTwoDigits >= 51 && firstTwoDigits <= 55))
            {
                return new CardSchemeType("Mastercard");
            }

            if (int.TryParse(firstNumbers.AsSpan(0, 4), out var firstFourDigits) && (firstFourDigits >= 2221 && firstFourDigits <= 2720))
            {
                return new CardSchemeType("Mastercard");
            }

            // If card scheme is not identified
            return new CardSchemeType("Unknown Card Type");
        }
    }
}
