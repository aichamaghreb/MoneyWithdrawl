using MoneyWithdrawal.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ENTRETIEN_TECHNIQUE.Console
{
    public class AccountService : IAccountService
    {
        private readonly IAccountBalanceDAO _accountBalanceDAO;
        private readonly IConsole _console;
        public AccountService(IAccountBalanceDAO accountBalanceDAO, IConsole console)
        {
            _accountBalanceDAO = accountBalanceDAO;
            _console = console;
        }

        public void Withdraw(string accountNumber, decimal amount)
        {

            AccountBalance? account = _accountBalanceDAO.GetById(accountNumber);

            if (account != null)
            {
                if (amount > account.Balance)
                {
                    _console.WriteText("Le montant de la demande dépasse le solde du compte");
                }
                else
                {
                    AccountBalance newBalance = new(account.AccountNumber,
                                account.Balance - amount);

                    _accountBalanceDAO.Save(newBalance);

                    _console.WriteText($"Vous venez de retirer {amount} sur votre compte n° {accountNumber}");
                }
            }
        }
    }
}
