using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyWithdrawal.Console
{
    public class ConsoleWrapper : IConsole
    {
        public void WriteText(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
