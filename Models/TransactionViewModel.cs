using YourProjectNamespace.Models;
using X.PagedList;

namespace YourProjectNamespace.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction NewTransaction { get; set; } = new Transaction();
        
        public IPagedList<Transaction>? Transactions { get; set; }

        //// �Ψӱ�������J���s���
        //public Transaction NewTransaction { get; set; } = new Transaction();

        //// �w�s����C��
        //public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        //public static implicit operator TransactionViewModel(TransactionViewModel v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
