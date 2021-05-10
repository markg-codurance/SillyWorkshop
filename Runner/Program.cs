using System;
using System.Linq;
using System.Threading.Tasks;
using Bank.API.Controllers;
using Bank.EventStore;
using Bank.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var hostBuilder = Bank.API.Program.CreateHostBuilder(args);
                // override dependencies on the hostBuilder
                // hostBuilder.ConfigureServices(another service collection)
                var host = hostBuilder.Build();

                var eventStore = host.Services.GetService<ITransactionEventStore>();
                var ctrl = ActivatorUtilities.CreateInstance<BankController>(host.Services);

                Console.WriteLine("All setup for the ports done! Continue to implement controller functionality...");
                
                Guid accountId = Guid.NewGuid();
                var viewBalance = await ctrl.ViewBalance(accountId);
                var accountStatement = await ctrl.AccountStatement(accountId);
                var deposit1 = await ctrl.Deposit(accountId, 100M);
                var deposit3 = await ctrl.Deposit(accountId, 63M);
                var withdraw1 = await ctrl.Withdraw(accountId, 76M);
                var withdraw2 = await ctrl.Withdraw(accountId, 64M);
                var deposit2 = await ctrl.Deposit(accountId, 50M);
                var withdraw3 = await ctrl.Withdraw(accountId, 20M);
                
                Console.WriteLine("The controllers build, wooohoooo???! Continue...");
                
                var overdraw = await ctrl.Withdraw(accountId, 1M);

                Console.WriteLine($"Balance is: {await ctrl.ViewBalance(accountId)}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public class ConsoleLoggerFactory : ILoggerFactory
    {
        public void Dispose()
        {
            
        }

        public void AddProvider(ILoggerProvider provider)
        {
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger();
        }
    }

    public class ConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) { return null; }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var normalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            var message = formatter(state, exception);
            Console.WriteLine(message);
            Console.ForegroundColor = normalColor;
        }
    }
}