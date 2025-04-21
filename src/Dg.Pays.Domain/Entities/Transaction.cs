using Dg.Pays.Domain.ValueObjects;

namespace Dg.Pays.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; private set; }

        public CardNumber CardNumber { get; private set; }
        public CardValidExpiryDate ExpiryDate { get; private set; }
        public CardSchemeType CardScheme { get; private set; }

        public string TransactionId { get; private set; } = string.Empty;
        public string CardHolderName { get; private set; } = string.Empty;
        public string MaskedCardNumber { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string ResponseCode { get; set; } = string.Empty;

        private Transaction()
        {
            CardNumber = new CardNumber("0000000000");
            ExpiryDate = new CardValidExpiryDate("1226");
            CardScheme = new CardSchemeType("400000000000000000");
        }

        public Transaction(string cardHolderName, CardNumber cardNumber, decimal amount, CardValidExpiryDate expiryDate)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            Amount = amount;
            TransactionId = GenerateTransactionCode();
            MaskedCardNumber = CardNumber.Masked;
            Timestamp = DateTime.UtcNow;

            CardScheme = CardSchemeType.FromCardNumber(cardNumber.Value);

            ExpiryDate = expiryDate;

            if (CardNumber.IsValidCard())
            {
                ResponseCode = "00";
            }
            else
            {
                ResponseCode = "-1";
            }
        }

        private static string GenerateTransactionCode()
        {
            return Guid.
                NewGuid().
                ToString("N")[..12];
        }
    }
}
