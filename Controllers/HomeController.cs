using Microsoft.AspNetCore.Mvc;
using YourProjectNamespace.Models;
using YourProjectNamespace.ViewModels;

namespace YourProjectNamespace.Controllers
{
    public class HomeController : Controller
    {
        // 使用靜態集合做為資料暫存示範
        private static List<Transaction> _transactions = new List<Transaction>();

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new TransactionViewModel
            {
                Transactions = _transactions
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(TransactionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Transactions = _transactions;
                return View(vm);
            }

            // 設定 id 為現有數量 + 1，依序 1, 2, 3...
            vm.NewTransaction.Id = _transactions.Count + 1;
            _transactions.Add(vm.NewTransaction);

            return RedirectToAction("Index");
        }
    }
}
