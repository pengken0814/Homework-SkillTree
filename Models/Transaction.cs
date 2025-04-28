namespace YourProjectNamespace.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public decimal Money { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
