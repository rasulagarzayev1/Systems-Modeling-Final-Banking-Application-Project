using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SysModelBank.Areas.Admin.Models.TransactionOverview;
using SysModelBank.Data.Enums;
using SysModelBank.Data.Models;
using SysModelBank.Data.Models.Identity;
using SysModelBank.Data.Repositories;
using SysModelBank.Data.Repositories.Identity;
using SysModelBank.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SysModelBank.Controllers
{
    public class ClientTransactionController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly UserManager<User> _userManager;

        public ClientTransactionController(IUserRepository userRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(ClientTransactionList model)
        {
            var selectedAccountId = model.SelectedAccountId;
            var accounts = await _accountRepository.GetAsync();
            var accountIds = new List<int>();
            var currentUser = await _userManager.GetUserAsync(User);

            foreach (var account in accounts)
            {
                if (account.UserId == currentUser.Id)
                {
                    accountIds.Add(account.Id);
                }
            }

            if (selectedAccountId == 0)
            {
                selectedAccountId = accountIds.FirstOrDefault();
            }

            var transactions = await _transactionRepository.GetAsync();
            var pastTransactions = new List<TransactionListItem>();

            if (selectedAccountId != 0)
            {
                foreach (var transaction in transactions)
                {
                    if ((transaction.SenderAccountId == selectedAccountId || transaction.RecipientAccountId == selectedAccountId) && (transaction.Status == TransactionStatus.Done || transaction.Status == TransactionStatus.PendingCancellation))
                    {
                        pastTransactions.Add(await MapToListItem(transaction));
                    }
                }
            }

            var accountSelectionList = new List<SelectListItem>();
            foreach(var accountId in accountIds)
            {
                accountSelectionList.Add(new SelectListItem(accountId.ToString(), accountId.ToString()));
            }

            return View(new ClientTransactionList
            {
                SelectedAccountId = selectedAccountId,
                Accounts = accountSelectionList,
                Transactions = pastTransactions
            });
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
    }
}
