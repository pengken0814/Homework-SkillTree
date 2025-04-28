using YourProjectNamespace.Models;

namespace YourProjectNamespace.ViewModels
{
    public class TransactionViewModel
    {
        // 用來接收表單輸入的新資料
        public Transaction NewTransaction { get; set; } = new Transaction();

        // 預存交易列表
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
