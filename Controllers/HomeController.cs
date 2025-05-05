using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework_SkillTree.Data;
using Homework_SkillTree.Models.DB;
using YourProjectNamespace.Models;
using YourProjectNamespace.ViewModels;

namespace YourProjectNamespace.Controllers
{
    public class HomeController : Controller
    {
        private readonly SkillTreeContext _context;
        public HomeController(SkillTreeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new TransactionViewModel
            {
                NewTransaction = new Transaction { Date = DateTime.Today },
                Transactions = await _context.AccountBooks
                    .OrderByDescending(x => x.Dateee)
                    .Select(x => new Transaction
                    {
                        Id = x.Id,
                        Category = x.Categoryyy == 1 ? "支出" : "收入",
                        Money = x.Amounttt,
                        Date = x.Dateee,
                        Description = x.Remarkkk
                    })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var accountBook = new AccountBook
                {
                    Id = Guid.NewGuid(),
                    Categoryyy = viewModel.NewTransaction.Category == "支出" ? 1 : 2,
                    Amounttt = (int)viewModel.NewTransaction.Money,
                    Dateee = viewModel.NewTransaction.Date,
                    Remarkkk = viewModel.NewTransaction.Description ?? string.Empty
                };

                _context.AccountBooks.Add(accountBook);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            viewModel.Transactions = await _context.AccountBooks
                .OrderByDescending(x => x.Dateee)
                .Select(x => new Transaction
                {
                    Category = x.Categoryyy == 1 ? "支出" : "收入",
                    Money = x.Amounttt,
                    Date = x.Dateee,
                    Description = x.Remarkkk
                })
                .ToListAsync();

            return View(viewModel);
        }
    }
}
