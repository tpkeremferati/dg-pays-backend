using System.ComponentModel.DataAnnotations;

namespace Dg.Pays.Application.Contracts.Requests
{
    public class TransactionRequest
    {
        [Required(ErrorMessage = "Cardholder name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Cardholder name must be between 2 and 100 characters")]
        public string CardHolderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card number is required")]
        [RegularExpression(@"^\d{13,19}$", ErrorMessage = "Card number must be between 13 and 19 digits.")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be greater than zero and less than 1,000,000.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Expiry date is required.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])([0-9]{2})$", ErrorMessage = "Expiry date must be in MMYY format.")]
        public string ExpiryDate { get; set; } = string.Empty;
    }
}
