using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAccountReader;
using BankCommandProcessor;
using BankDesk;
using BankDesk.Controllers;
using BankDomain;
using BankStore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var webapi = new Startup();
                webapi.ConfigureServices(new ServiceCollection());

                Console.WriteLine("All setup for the ports done! Continue to wire up controller functionality...");

                var eventStore = new BankTransactionEventStore();
                var ctrl = new BankController(
                    new Logger<BankController>(new ConsoleLoggerFactory()),
                    new CommandHandler(eventStore),
                    new BalanceReport(eventStore), 
                    new DummyStatementReport(eventStore));

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

    internal class DummyStatementReport : IReadStatement
    {
        StatementReport report;

        public DummyStatementReport(BankTransactionEventStore eventStore)
        {
            /*
             * For now you could remove the exception and come back to this later
             * although it may not allow you much progress at some point depending
             * on how you tackle the problems }:-}
             */
            throw new Exception("Read me! :-D");
            report = new StatementReport(eventStore);
        }

        public Task<IEnumerable<TransactionEvent>> GetStatement(Guid accountId)
        {
            return report.GetStatement(accountId);
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