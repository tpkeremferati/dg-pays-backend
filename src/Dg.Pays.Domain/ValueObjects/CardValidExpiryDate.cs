using Dg.Pays.Domain.Exceptions;

namespace Dg.Pays.Domain.ValueObjects
{
    public class CardValidExpiryDate
    {
        public string ExpiryDate { get; } = string.Empty;

        private CardValidExpiryDate() {  }

        public CardValidExpiryDate(string expiryDate)
        {
            if (!IsValidExpiryDate(expiryDate))
            {
                throw new InvalidCardExpirationException("The expiry date is invalid or the card has expired.");
            }

            ExpiryDate = expiryDate;
        }

        private static bool IsValidExpiryDate(string expiryDate)
        {
            if (expiryDate.Length != 4)
            {
                return false;
            }

            int month = int.Parse(expiryDate[..2]);
            int year = int.Parse(expiryDate.Substring(2, 2)) + 2000;

            if (month < 1 || month > 12)
            {
                return false;
            }

            var cardExpiryDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return cardExpiryDate >= DateTime.UtcNow;
        }
    }
}
