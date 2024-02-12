using ENTRETIEN_TECHNIQUE.Console;
using System;

public interface IAccountBalanceDAO
{
    public AccountBalance? GetById(string accountNumber);

    public void Save(AccountBalance balance);

}
