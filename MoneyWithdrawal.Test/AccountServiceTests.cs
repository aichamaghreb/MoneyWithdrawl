using ENTRETIEN_TECHNIQUE.Console;
using MoneyWithdrawal.Console;
using Moq;
using Xunit;

namespace MoneyWithdrawal.Test
{
    public class AccountServiceTests
    {
        private Mock<IAccountBalanceDAO> _accountBalanceDAO;
        private Mock<IConsole> _console;
        private Mock<IDate> _date;
        private AccountService _accountService;

        public AccountServiceTests()
        {
            _console = new Mock<IConsole>();
            _accountBalanceDAO = new Mock<IAccountBalanceDAO>();
            _date = new Mock<IDate>();

            _accountService = new AccountService(_accountBalanceDAO.Object, _console.Object, _date.Object);
        }


        [Fact]
        public void Withdraw_Returns_SuccesMessage()
        {
            //ARRANGE
            var numAccount = "0000001";
            var amount = 50;
            AccountBalance account = new AccountBalance(numAccount, 120);
            _accountBalanceDAO.Setup(x => x.GetById(numAccount)).Returns(account);
            AccountBalance savedBalance = null;
            _accountBalanceDAO.Setup(x => x.Save(It.IsAny<AccountBalance>()))
                .Callback<AccountBalance>(balance => savedBalance = balance);

            //ACT
            _accountService.Withdraw(numAccount, amount);

            //ASSERT
            _accountBalanceDAO.Verify(x => x.Save(It.IsAny<AccountBalance>()), Times.Once());
            Assert.NotNull(savedBalance);
            Assert.Equal(account.AccountNumber, savedBalance.AccountNumber);
            Assert.Equal(account.Balance - amount, savedBalance.Balance);
            _console.Verify(x => x.WriteText($"Vous venez de retirer {amount} sur votre compte n° {numAccount}"), Times.Once());
        }

        //[Fact]
        //public void Withdraw_Returns_FailureMessage()
        //{
        //    //ARRANGE
        //    var numAccount = "0000001";
        //    var amount = 300;
        //    AccountBalance account = new AccountBalance(numAccount, 120);
        //    _accountBalanceDAO.Setup(x => x.GetById(numAccount)).Returns(account);

        //    //ACT
        //    _accountService.Withdraw(numAccount, amount);

        //    //ASSERT
        //    _accountBalanceDAO.Verify(x => x.Save(account), Times.Never());
        //    _console.Verify(x => x.WriteText("Le montant de la demande dépasse le solde du compte"), Times.Once());
        //}

        [Fact]
        public void Withdraw_Returns_FailureMessage_When_Month_Is_December()
        {
            //ARRANGE
            var numAccount = "0000001";
            var amount = 50;
            var monthValue = 12;
            AccountBalance account = new AccountBalance(numAccount, 120);
            _accountBalanceDAO.Setup(x => x.GetById(numAccount)).Returns(account);
            _date.Setup(x => x.GetMonth()).Returns(monthValue);

            //ACT
            _accountService.Withdraw(numAccount, amount);

            //ASSERT
            _accountBalanceDAO.Verify(x => x.Save(account), Times.Never());
            _console.Verify(x => x.WriteText("Aucun retrait n'est autorisé en Décembre"), Times.Once());
        }

        [Fact]
        public void Withdraw_Returns_SuccesMessage_When_ExpectedBalance_Is_Valid()
        {
            //ARRANGE
            var numAccount = "0000001";
            var amount = 50;
            var initBalance = 120;
            var monthValue = 10;
            var expectedBalance = initBalance - amount;
            AccountBalance account = new AccountBalance(numAccount, initBalance);
            _accountBalanceDAO.Setup(x => x.GetById(numAccount)).Returns(account);
            _date.Setup(x => x.GetMonth()).Returns(monthValue);

            //ACT
            _accountService.Withdraw(numAccount, amount);

            //ASSERT
            _accountBalanceDAO.Verify(x => x.Save(It.Is<AccountBalance>(b => b.Balance == expectedBalance)), Times.Once());
            _console.Verify(x => x.WriteText($"Vous venez de retirer {amount} sur votre compte n° {numAccount}"), Times.Once());
        }
    }
}