using YourProjectNamespace.Models;

namespace YourProjectNamespace.ViewModels
{
    public class TransactionViewModel
    {
        // �Ψӱ�������J���s���
        public Transaction NewTransaction { get; set; } = new Transaction();

        // �w�s����C��
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
