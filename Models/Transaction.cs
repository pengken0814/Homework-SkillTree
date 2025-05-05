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
        public string Category { get; set; }
        public decimal Money { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
