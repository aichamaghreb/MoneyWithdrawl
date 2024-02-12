using ENTRETIEN_TECHNIQUE.Console;
using MoneyWithdrawal.Console;
using Moq;

namespace MoneyWithdrawal.Test
{
    public class AccountServiceTests
    {
        private Mock<IAccountBalanceDAO> _accountBalanceDAO;
        private Mock<IConsole> _console;
        private AccountService _accountService;

        public AccountServiceTests()
        {
            _console = new Mock<IConsole>();
            _accountBalanceDAO = new Mock<IAccountBalanceDAO>();

            _accountService = new AccountService(_accountBalanceDAO.Object,_console.Object );
        }


        [Fact]
        public void Withdraw_Returns_SuccesMessage()
        {
            //ARRANGE
            var numAccount = "0000001";
            var amount = 50;
            AccountBalance account = new AccountBalance(numAccount, 120);
            _accountBalanceDAO.Setup(x => x.GetById(numAccount)).Returns(account);
            _accountBalanceDAO.Setup(x => x.Save(account));

            //ACT
            _accountService.Withdraw(numAccount, amount);

            //ASSERT
            _console.Verify(x => x.WriteText($"Vous venez de retirer {amount} sur votre compte n° {numAccount}"));

        }

        [Fact]
        public void Withdraw_Returns_FailureMessage()
        {

        }
    }
}