using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoneyWithdrawal.Console;

namespace ENTRETIEN_TECHNIQUE.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var accountService = services.GetRequiredService<IAccountService>();

                accountService.Withdraw("0000001", 150);
            }
            System.Console.WriteLine("Appuyez sur une touche pour terminer l'exécution");
            System.Console.ReadKey();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, services) =>
           {
               services.AddSingleton<IAccountBalanceDAO, AccountBalanceDAO>()
                       .AddSingleton<IConsole, ConsoleWrapper>()
                       .AddSingleton<IAccountService, AccountService>();
           });
    }
}