using YourProjectNamespace.Models;
using X.PagedList;

namespace YourProjectNamespace.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction NewTransaction { get; set; } = new Transaction();
        
        public IPagedList<Transaction>? Transactions { get; set; }

        //// 用來接收表單輸入的新資料
        //public Transaction NewTransaction { get; set; } = new Transaction();

        //// 預存交易列表
        //public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        //public static implicit operator TransactionViewModel(TransactionViewModel v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
