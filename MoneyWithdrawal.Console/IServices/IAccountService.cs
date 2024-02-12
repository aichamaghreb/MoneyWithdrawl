namespace MoneyWithdrawal.Console
{
    public interface IAccountService
    {
        public void Withdrawl(string accountNumber, decimal amount);
    }
}
