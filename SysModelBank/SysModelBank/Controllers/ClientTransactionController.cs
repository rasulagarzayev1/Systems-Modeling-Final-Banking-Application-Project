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
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SysModelBank.Extensions;
using SysModelBank.Models;
using SysModelBank.Services.Logger;

namespace SysModelBank.Controllers
{
    public class ClientTransactionController : Controller
    {
        private Random  random = new Random();
        private readonly IBankLogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionCodeRepository _transactionCodeRepository;
        private readonly UserManager<User> _userManager;

        public ClientTransactionController(IBankLogger logger, IUserRepository userRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository, ITransactionCodeRepository transactionCodeRepository, UserManager<User> userManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _transactionCodeRepository = transactionCodeRepository;
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
                CurrencyLocalization = currentUser.Currency.Name,
                Accounts = accountSelectionList,
                Transactions = pastTransactions
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _transactionRepository.GetAsync(id);

            return View(MapToDetails(transaction));
        }

        [HttpPost]
        public async Task<IActionResult> RequestUndo(int id, string description)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var transaction = await _transactionRepository.GetAsync(id);

            transaction.Status = TransactionStatus.PendingCancellation;
            await _transactionRepository.UpdateAsync(transaction);

            _logger.Log("ClientTransactionController", $"User {currentUser.UserName} submitted transaction {id} to be undone!");

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> CancelUndo(int id)
        {
            var transaction = await _transactionRepository.GetAsync(id);

            transaction.Status = TransactionStatus.Done;
            await _transactionRepository.UpdateAsync(transaction);

            return RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> New()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var availableAccounts = currentUser.Accounts.ToAccountsListModels(currentUser.Currency).ToList();
            if (!availableAccounts.Any())
            {
                return RedirectToAction("Index",
                    new NotificationModel("You don't have any accounts to make a transaction from!").asError());
            }

            var code = HttpContext.Request.Query["transaction_code"];
            var recipientAccount = 0;
            var selectedDescription = "";
            var selectedAmount = 0m;

            if (code != StringValues.Empty)
            {
                var transactionCode = await _transactionCodeRepository.GetAsync(HttpContext.Request.Query["transaction_code"]);
                if (transactionCode == null)
                {
                    code = StringValues.Empty;
                    ViewBag.Notification = new NotificationModel("The entered transaction code doesn't exist!").asError();
                }
                else
                {
                    recipientAccount = transactionCode.Id;
                    selectedDescription = transactionCode.Description;
                    selectedAmount = transactionCode.Amount;
                }
            }
            
            var model = new NewTransactionModel
            {
                TransactionCode = code,
                TransactionRecipient = recipientAccount,
                TransactionDescription = selectedDescription,
                TransactionAmount = selectedAmount * currentUser.Currency.RateFromEur,
                AvailableAccounts = new SelectList(currentUser.Accounts.Select(x => new SelectListItem($"{x.Id} ({x.Balance * x.User.Currency.RateFromEur} {x.User.Currency.Name})", x.Id.ToString())), "Value", "Text")
            };

            return View("New", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(NewTransactionModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            TransactionCode transactionCode = null;
            if (model.TransactionCode != null)
            {
                transactionCode = await _transactionCodeRepository.GetAsync(model.TransactionCode);
                if (transactionCode == null)
                {
                    return RedirectToAction("New",
                        new NotificationModel("The entered transaction code doesn't exist!").asError());
                }
                else
                {
                    model.TransactionAmount = transactionCode.Amount * currentUser.Currency.RateFromEur;
                    model.TransactionDescription = transactionCode.Description;
                    model.TransactionRecipient = transactionCode.RecipientId;
                }
            }

            if (string.IsNullOrEmpty(model.TransactionDescription))
            {
                return RedirectToAction("New",
                    new NotificationModel("None of the fields should be empty by the way!").asError());
            }

            if (model.TransactionAmount <= 0)
            {
                return RedirectToAction("New",
                    new NotificationModel("The entered amount must be bigger than 0!").asError());
            }

            var sourceAccount = currentUser.Accounts.FirstOrDefault(x => x.Id == model.TransactionSource);
            if (sourceAccount == null)
            {
                return RedirectToAction("New",
                    new NotificationModel("Thank you for using the Systems Modelling banking application, please continue to your nearest account!").asError());
            }

            if (sourceAccount.UserId != currentUser.Id)
            {
                return RedirectToAction("New",
                    new NotificationModel("Hey! That's not cool!").asError());
            }

            var recipientAccount = await _accountRepository.GetAsync(model.TransactionRecipient);
            if (recipientAccount == null)
            {
                return RedirectToAction("New",
                    new NotificationModel("The recipient account wasn't found!").asError());
            }

            if (sourceAccount.Id == recipientAccount.Id)
            {
                return RedirectToAction("New",
                    new NotificationModel("You cannot transfer money to the same account!").asError());
            }

            var normalizedAmount = model.TransactionAmount / currentUser.Currency.RateFromEur;
            if (sourceAccount.Balance < normalizedAmount)
            {
                return RedirectToAction("New",
                    new NotificationModel("The source account doesn't have enough money in the balance!").asError());
            }

            var transaction = new Transaction
            {
                Description = model.TransactionDescription,
                Amount = normalizedAmount,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUser.Id,
                SenderAccountId = sourceAccount.Id,
                RecipientAccountId = recipientAccount.Id
            };

            await _transactionRepository.CreateAsync(transaction);

            sourceAccount.Balance -= normalizedAmount;
            sourceAccount.SentTransactions.Add(transaction);
            await _accountRepository.UpdateAsync(sourceAccount);

            recipientAccount.Balance += normalizedAmount;
            recipientAccount.RecievedTransactions.Add(transaction);
            await _accountRepository.UpdateAsync(recipientAccount);

            if (transactionCode != null)
                await _transactionCodeRepository.RemoveAsync(transactionCode);

            _logger.Log("ClientTransactionController", $"User {currentUser.UserName} created a transaction from account {sourceAccount.Id} to account {recipientAccount.Id} with amount {normalizedAmount}€");


            return RedirectToAction("New", new NotificationModel("The transaction was successful!").asSuccess());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionCode()
        {
            Debug.WriteLine(JsonConvert.SerializeObject(Request.Form));
            if (!Request.Form.ContainsKey("recipient_account") || !Request.Form.ContainsKey("amount") || !Request.Form.ContainsKey("description"))
                return RedirectToAction("Index", new NotificationModel("Please fill out all the necessary fields! Thank you!").asError());

            if (!int.TryParse(Request.Form["recipient_account"].ToString(), out int recipientAccountId))
                return RedirectToAction("Index", new NotificationModel("Hey! The recipient account is supposed to be a number?!?!! How did you manage to change it up?").asError());

            if (!decimal.TryParse(Request.Form["amount"].ToString(), out decimal amount))
                return RedirectToAction("Index", new NotificationModel("Please enter an actual number as the amount!").asError());

            if (string.IsNullOrEmpty(Request.Form["description"].ToString()))
                return RedirectToAction("Index", new NotificationModel("Description really can't be empty!").asError());

            var currentUser = await _userManager.GetUserAsync(User);
            var recipientAccount = currentUser.Accounts.FirstOrDefault(x => x.Id == recipientAccountId);
            if (recipientAccount == null)
            {
                return RedirectToAction("New",
                    new NotificationModel("Thank you for using the Systems Modelling banking application, please continue to your nearest account!").asError());
            }

            if (recipientAccount.UserId != currentUser.Id)
            {
                return RedirectToAction("New",
                    new NotificationModel("Hey! That's not cool!").asError());
            }

            var normalizedAmount = amount / currentUser.Currency.RateFromEur;
            var generatedCode = random.Next(123456, 999999).ToString();
            while ((await _transactionCodeRepository.Exists(generatedCode)))
                generatedCode = random.Next(123456, 999999).ToString();

            var transactionCode = new TransactionCode
            {
                Amount = normalizedAmount,
                Code = generatedCode,
                CreationTime = DateTime.Now,
                Description = Request.Form["description"],
                RecipientId = recipientAccountId
            };

            await _transactionCodeRepository.CreateAsync(transactionCode);
            _logger.Log("ClientTransactionController", $"User {currentUser.UserName} created a transaction code for account {recipientAccountId} with amount {normalizedAmount}€");

            return RedirectToAction("Index", new NotificationModel($"The transaction code that was created is \"{generatedCode}\"!").asSuccess());
        }

        private async Task<TransactionListItem> MapToListItem(Transaction transaction)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            User SendingUser = (await _userRepository.GetAsync((await _accountRepository.GetAsync(transaction.SenderAccountId)).UserId));
            User RecivingUser = (await _userRepository.GetAsync((await _accountRepository.GetAsync(transaction.RecipientAccountId)).UserId));
            return new TransactionListItem
            {
                Id = transaction.Id,
                SenderName = SendingUser.Firstname + " " + SendingUser.Lastname,
                RecipientName = RecivingUser.Firstname + " " + RecivingUser.Lastname,
                Date = transaction.CreationTime,
                Amount = Math.Round(transaction.Amount * currentUser.Currency.RateFromEur, 2)
            };
        }

        private async Task<ClientTransactionDetails> MapToDetails(Transaction transaction)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return new ClientTransactionDetails
            {
                Id = transaction.Id,
                Amount = Math.Round(transaction.Amount * currentUser.Currency.RateFromEur, 2),
                CreationTime = transaction.CreationTime,
                Description = transaction.Description,
                RecipientName = transaction.RecipientAccount.User.Firstname + " " + transaction.RecipientAccount.User.Lastname,
                SenderName = transaction.SenderAccount.User.Firstname + " " + transaction.SenderAccount.User.Lastname,
                Status = transaction.Status
            };
        }
    }
}
