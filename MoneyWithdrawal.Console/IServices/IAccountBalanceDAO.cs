using ENTRETIEN_TECHNIQUE.Console;

public interface IAccountBalanceDAO
{
    public AccountBalance? GetById(string accountNumber);

    public void Save(AccountBalance balance);

}
