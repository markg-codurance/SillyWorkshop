using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BankCommandProcessor;
using BankDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankDesk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly ILogger<BankController> logger;
        private readonly ICommandHandler handler;
        private readonly IReadBalance balanceReader;
        private readonly IReadStatement statementReader;

        public BankController(ILogger<BankController> logger, ICommandHandler handler, IReadBalance balanceReader, IReadStatement statementReader)
        {
            logger.LogInformation("Setting up the Bank Controller...");
            this.logger = logger;
            this.handler = handler;
            this.balanceReader = balanceReader;
            this.statementReader = statementReader;
        }

        [HttpGet("{accountId}")]
        public async Task<decimal> ViewBalance(Guid accountId)
        {
            /*
             * What needs to be utilised to get the balance to be returned???
             */
            throw new Exception("Implement retrieval of balance to continue... Mark forgot to do it! Typical.");
        }

        [HttpGet("statement/{accountId}")]
        public async Task<List<TransactionEvent>> AccountStatement(Guid accountId)
        {
            // A bank statement that covers all time, i.e., don't worry about date ranges
            throw new Exception("Implement retrieval of statement... are you bored yet?? :D");
        }

        [HttpGet("deposit/{accountId}/{amount}")]
        public async Task<decimal> Deposit(Guid accountId, decimal amount)
        {
            throw new NotImplementedException("Implement the 'Deposit' command, and pass it to execute.. God this is boring right :-O");
            await handler.Execute(
                
                null // Clearly, this is not right, tut tut Mark, do your job properly!
                
                );
            logger.LogInformation($"Added amount £{amount:0.00} to the account {accountId}");

            return await balanceReader.GetBalance(accountId);
        }

        [HttpGet("withdraw/{accountId}/{amount}")]
        public async Task<string> Withdraw(Guid accountId, decimal amount)
        {
            try
            {
                await handler.Execute(new WithdrawalCommand
                    {AccountId = accountId, Amount = amount, CorrelationId = Guid.NewGuid()});
                logger.LogInformation($"Subtracted amount £{amount:0.00} from the account {accountId}");
            }
            catch (Exception e)
            {
                // See if you can make the program state failure for overdraft usage attempt...
                // Also use the logger to catch the exception so it can be seen in the console.
                Console.WriteLine(e);
                throw;
            }
            
            return Convert.ToString(await balanceReader.GetBalance(accountId), CultureInfo.InvariantCulture);
        }
    }
}