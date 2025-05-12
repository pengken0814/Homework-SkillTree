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

        [Required(ErrorMessage = "請選擇類別")]
        public string Category { get; set; }

        [Required(ErrorMessage = "請輸入金額")]
        [Range(1, int.MaxValue, ErrorMessage = "金額必須大於0")]
        public decimal Money { get; set; }

        [Required(ErrorMessage = "請選擇日期")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Transaction), nameof(ValidateDate))]
        public DateTime Date { get; set; }

        [StringLength(100, ErrorMessage = "備註最多100個字元")]
        public string Description { get; set; }

        public static ValidationResult ValidateDate(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Today)
            {
                return new ValidationResult("日期不可大於今天");
            }
            return ValidationResult.Success;
        }
    }
}
