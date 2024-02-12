using MoneyWithdrawal.Console;

namespace ENTRETIEN_TECHNIQUE.Console
{
    public class AccountService : IAccountService
    {
        private readonly IAccountBalanceDAO _accountBalanceDAO;
        private readonly IConsole _console;
        private readonly IDate _date;
        public AccountService(IAccountBalanceDAO accountBalanceDAO, IConsole console, IDate date)
        {
            _accountBalanceDAO = accountBalanceDAO;
            _console = console;
            _date = date;
        }

        public void Withdrawl(string accountNumber, decimal amount)
        {

            AccountBalance? account = _accountBalanceDAO.GetById(accountNumber);

            if (account != null)
            {
                if (_date.GetMonth() == 12)
                {
                    _console.WriteText("Aucun retrait n'est autorisé en Décembre");
                    return;
                }

                //if (amount > account.Balance)
                //{
                //    _console.WriteText("Le montant de la demande dépasse le solde du compte");
                //}
                decimal limitAmount = -20;
                decimal expectedBalance = account.Balance - amount;

                if (expectedBalance < limitAmount)
                {
                    _console.WriteText("Le montant de la demande dépasse votre autorisation de découvert");
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
