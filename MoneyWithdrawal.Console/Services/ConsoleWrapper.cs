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
