using System.ComponentModel.DataAnnotations;

namespace YourProjectNamespace.Models
{
    public class Transaction
    {
        public Transaction()
        {
            Date = DateTime.Today;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "�п�����O")]
        public string Category { get; set; }

        [Required(ErrorMessage = "�п�J���B")]
        [Range(1, int.MaxValue, ErrorMessage = "���B�����j��0")]
        public decimal Money { get; set; }

        [Required(ErrorMessage = "�п�ܤ��")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Transaction), nameof(ValidateDate))]
        public DateTime Date { get; set; }

        [StringLength(100, ErrorMessage = "�Ƶ��̦h100�Ӧr��")]
        public string Description { get; set; }

        public static ValidationResult ValidateDate(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Today)
            {
                return new ValidationResult("������i�j�󤵤�");
            }
            return ValidationResult.Success;
        }
    }
}
