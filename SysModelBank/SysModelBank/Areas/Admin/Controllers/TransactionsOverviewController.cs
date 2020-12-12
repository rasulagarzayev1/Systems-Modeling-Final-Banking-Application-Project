using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SysModelBank.Areas.Admin.Models.TransactionOverview;
using SysModelBank.Areas.Admin.Models.UserManagement;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Repositories;
using SysModelBank.Data.Repositories.Identity;
using SysModelBank.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysModelBank.Services.Logger;
using Microsoft.AspNetCore.Authorization;

namespace SysModelBank.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = PolicyConstants.Admin)]
    public class TransactionsOverviewController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBankLogger _logger;

        public TransactionsOverviewController(IUserRepository userRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository, IBankLogger logger)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAsync();

            var undoing = new List<TransactionListItem>();
            var pastTransactions = new List<TransactionListItem>();

            foreach (var transaction in transactions)
            {
                if (transaction.Status == TransactionStatus.PendingCancellation)
                {
                    undoing.Add(await MapToListItem(transaction));
                }
                else if (transaction.Status == TransactionStatus.Done)
                {
                    pastTransactions.Add(await MapToListItem(transaction));
                }
            }

            return View(new TransactionList
            {
                PendingUndoing = undoing,
                Transactions = pastTransactions
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _transactionRepository.GetAsync(id);

            return View(await MapToDetail(transaction));
        }

        [HttpPost]
        public async Task<IActionResult> Undo(int id)
        {
            if (!User.IsInRole(Role.Admin))
            {
                return RedirectToAction("Details", new { id });
            }

            var transaction = await _transactionRepository.GetAsync(id);

            transaction.Status = TransactionStatus.Cancelled;

            await _transactionRepository.UpdateAsync(transaction);

            var account = await _accountRepository.GetAsync(transaction.RecipientAccountId);

            account.Balance -= transaction.Amount;

            await _accountRepository.UpdateAsync(account);
            _logger.Log("TransactionsOverviewController", $"Transaction {transaction.Id} was undone by {HttpContext.User.Identity.Name}");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Seed()
        {
            return View();
        }

        public async Task<IActionResult> SaveSeed(SeedTransaction model)
        {
            var admin = await _userRepository.GetAsync(User.Id());

            var transaction = new Transaction
            {
                Description = model.Description,
                Amount = model.Amount,
                CreationTime = DateTime.Now,
                CreatorUserId = admin.Id,
                SenderAccountId = 999,
                RecipientAccountId = model.RecipientId
            };

            await _transactionRepository.CreateAsync(transaction);

            var account = await _accountRepository.GetAsync(model.RecipientId);

            account.Balance += model.Amount;

            await _accountRepository.UpdateAsync(account);
            _logger.Log("TransactionsOverviewController", $"Seed transaction with amount {model.Amount} was done by {HttpContext.User.Identity.Name} to {account.User.UserName}");

            return RedirectToAction("Index");
        }

        private async Task<TransactionListItem> MapToListItem(Transaction transaction)
        {
            User SendingUser = (await _userRepository.GetAsync((await _accountRepository.GetAsync(transaction.SenderAccountId)).UserId));
            User RecivingUser = (await _userRepository.GetAsync((await _accountRepository.GetAsync(transaction.RecipientAccountId)).UserId));
            return new TransactionListItem
            {
                Id = transaction.Id,
                SenderName = SendingUser.Firstname + " " + SendingUser.Lastname,
                RecipientName = RecivingUser.Firstname + " " + RecivingUser.Lastname,
                Date = transaction.CreationTime,
                Amount = transaction.Amount
            };
        }

        private async Task<TransactionDetail> MapToDetail(Transaction transaction)
        {
            User SendingUser = (await _userRepository.GetAsync((await _accountRepository.GetAsync(transaction.SenderAccountId)).UserId));
            User RecivingUser = (await _userRepository.GetAsync((await _accountRepository.GetAsync(transaction.RecipientAccountId)).UserId));

            return new TransactionDetail
            {
                Id = transaction.Id,
                Status = transaction.Status,
                Amount = transaction.Amount,
                CreationTime = transaction.CreationTime,
                SenderAccountId = transaction.SenderAccountId,
                SenderName = SendingUser.Firstname + " " + SendingUser.Lastname,
                RecipientAccountId = transaction.RecipientAccountId,
                RecipientName = RecivingUser.Firstname + " " + RecivingUser.Lastname,
                Description = transaction.Description
            };
        }

    }
}
