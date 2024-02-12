namespace ENTRETIEN_TECHNIQUE.Console
{
    public class AccountBalance
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        public AccountBalance(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }
    }
}
