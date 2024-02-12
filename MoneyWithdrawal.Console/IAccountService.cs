using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyWithdrawal.Console
{
    public interface IAccountService
    {
        public void Withdraw(string accountNumber, decimal amount);
    }
}
