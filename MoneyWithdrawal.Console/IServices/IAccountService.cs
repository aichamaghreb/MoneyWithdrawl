namespace MoneyWithdrawal.Console
{
    public interface IAccountService
    {
        public void Withdraw(string accountNumber, decimal amount);
    }
}
