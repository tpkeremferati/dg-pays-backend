namespace Dg.Pays.Application.Contracts.DTOs
{
    public class TransactionDto
    {
        public string TransactionId { get; set; } = string.Empty;
        public string MaskedCardNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string ResponseCode { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
