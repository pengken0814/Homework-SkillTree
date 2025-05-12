using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework_SkillTree.Data;
using Homework_SkillTree.Models.DB;
using YourProjectNamespace.Models;
using YourProjectNamespace.ViewModels;
using X.PagedList;

namespace YourProjectNamespace.Controllers
{
    public class HomeController : Controller
    {
        private readonly SkillTreeContext _context;
        private const int PageSize = 10; // 每頁顯示筆數
        public HomeController(SkillTreeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;

            // 先查詢總筆數以建立分頁資訊
            var totalCount = await _context.AccountBooks.CountAsync();

            // 只查詢當前頁面需要的資料
            var pagedData = await _context.AccountBooks
                .OrderByDescending(x => x.Dateee)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .Select(x => new Transaction
                {
                    Id = x.Id,
                    Category = x.Categoryyy == 1 ? "支出" : "收入",
                    Money = x.Amounttt,
                    Date = x.Dateee,
                    Description = x.Remarkkk
                })
                .ToListAsync();

            // 建立分頁清單
            var pagedList = new StaticPagedList<Transaction>(
                pagedData,
                pageNumber,
                PageSize,
                totalCount
            );

            var viewModel = new TransactionViewModel
            {
                NewTransaction = new Transaction { Date = DateTime.Today },
                Transactions = pagedList
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Transaction newTransaction)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        // 將錯誤訊息加入到 ViewData 中以便在 View 中顯示
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // 建立新的 AccountBook 實體
                    var accountBook = new AccountBook
                    {
                        Id = Guid.NewGuid(),
                        Categoryyy = newTransaction.Category == "支出" ? 1 : 2,
                        Amounttt = (int)newTransaction.Money,
                        Dateee = newTransaction.Date,
                        Remarkkk = newTransaction.Description ?? string.Empty
                    };

                    // 新增到資料庫
                    await _context.AccountBooks.AddAsync(accountBook);

                    // 儲存變更
                    await _context.SaveChangesAsync();

                    // 重新導向到 Index 頁面，顯示最新資料
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"儲存資料時發生錯誤: {ex.Message}");
            }

            // 如果驗證失敗或發生例外，重新載入第一頁資料
            int pageNumber = 1;
            var totalCount = await _context.AccountBooks.CountAsync();

            var pagedData = await _context.AccountBooks
                .OrderByDescending(x => x.Dateee)
                .Take(PageSize)
                .Select(x => new Transaction
                {
                    Id = x.Id,
                    Category = x.Categoryyy == 1 ? "支出" : "收入",
                    Money = x.Amounttt,
                    Date = x.Dateee,
                    Description = x.Remarkkk
                })
                .ToListAsync();
            TransactionViewModel VM = new TransactionViewModel();
            VM.Transactions = new StaticPagedList<Transaction>(
                pagedData,
                pageNumber,
                PageSize,
                totalCount
            );

            // 返回原始頁面，顯示錯誤訊息
            return View(newTransaction);
        }
    }
}
