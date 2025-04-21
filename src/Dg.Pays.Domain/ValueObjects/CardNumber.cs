using Dg.Pays.Domain.Exceptions;

namespace Dg.Pays.Domain.ValueObjects
{
    public class CardNumber
    {
        public string Value { get; private set; }

        public string Masked => MaskCardNumber(Value);

        public CardNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidCardNumberException("Card number cannot be empty.");

            Value = value;
        }

        // Luhn Algorithm to validate the card number
        public bool IsValidCard()
        {
            if (string.IsNullOrEmpty(Value) || Value.Length < 13 || Value.Length > 19 || Value.StartsWith("0"))
                return false;

            int sum = 0;
            bool alternate = false;

            for (int i = Value.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(Value[i])) return false;

                int n = Value[i] - '0';
                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                        n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }


        // Method to mask the card number (show only the first 6 and last 4 digits)
        private static string MaskCardNumber(string cardNumber)
        {
            if (cardNumber.Length < 10) return cardNumber;
            return string.Concat(cardNumber.AsSpan()[..6], new string('*', cardNumber.Length - 10), cardNumber.AsSpan(cardNumber.Length - 4));
        }
    }
}
