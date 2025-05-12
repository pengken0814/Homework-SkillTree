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
        private const int PageSize = 10; // �C����ܵ���
        public HomeController(SkillTreeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;

            // ���d���`���ƥH�إߤ�����T
            var totalCount = await _context.AccountBooks.CountAsync();

            // �u�d�߷�e�����ݭn�����
            var pagedData = await _context.AccountBooks
                .OrderByDescending(x => x.Dateee)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .Select(x => new Transaction
                {
                    Id = x.Id,
                    Category = x.Categoryyy == 1 ? "��X" : "���J",
                    Money = x.Amounttt,
                    Date = x.Dateee,
                    Description = x.Remarkkk
                })
                .ToListAsync();

            // �إߤ����M��
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
                        // �N���~�T���[�J�� ViewData ���H�K�b View �����
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // �إ߷s�� AccountBook ����
                    var accountBook = new AccountBook
                    {
                        Id = Guid.NewGuid(),
                        Categoryyy = newTransaction.Category == "��X" ? 1 : 2,
                        Amounttt = (int)newTransaction.Money,
                        Dateee = newTransaction.Date,
                        Remarkkk = newTransaction.Description ?? string.Empty
                    };

                    // �s�W���Ʈw
                    await _context.AccountBooks.AddAsync(accountBook);

                    // �x�s�ܧ�
                    await _context.SaveChangesAsync();

                    // ���s�ɦV�� Index �����A��̷ܳs���
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"�x�s��Ʈɵo�Ϳ��~: {ex.Message}");
            }

            // �p�G���ҥ��ѩεo�ͨҥ~�A���s���J�Ĥ@�����
            int pageNumber = 1;
            var totalCount = await _context.AccountBooks.CountAsync();

            var pagedData = await _context.AccountBooks
                .OrderByDescending(x => x.Dateee)
                .Take(PageSize)
                .Select(x => new Transaction
                {
                    Id = x.Id,
                    Category = x.Categoryyy == 1 ? "��X" : "���J",
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

            // ��^��l�����A��ܿ��~�T��
            return View(newTransaction);
        }
    }
}
