using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ENTRETIEN_TECHNIQUE.Console
{
    public class AccountBalanceDAO : IAccountBalanceDAO
    {
        public static List<AccountBalance> AccountBalances = new(){
                new("0000001", 120),
                new("0000002", 50),
                new("0000003", 0)
            };
        
        public AccountBalance? GetById(string accountNumber)
        {
            return AccountBalances.FirstOrDefault(e => e.AccountNumber.Equals(accountNumber));
        }

        public void Save(AccountBalance balance)
        {
            AccountBalances.RemoveAll(e => e.AccountNumber == balance!.AccountNumber);
            AccountBalances.Add(balance);
        }
    }
}
