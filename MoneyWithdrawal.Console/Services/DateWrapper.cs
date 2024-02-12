namespace MoneyWithdrawal.Console
{
    public class DateWrapper : IDate
    {
        public int GetMonth()
        {
            return DateTime.Now.Month;
        }
    }
}
