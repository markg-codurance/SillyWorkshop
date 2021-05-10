using System;
using System.Globalization;
using System.Threading.Tasks;
using Bank.Account.UseCases.Deposit;
using Bank.Account.UseCases.GetBalance;
using Bank.Account.UseCases.GetStatement;
using Bank.Account.UseCases.Withdraw;
using Bank.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bank.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        private readonly ILogger<BankController> logger;
        readonly ICommandHandler<WithdrawCommand> withdraw;
        readonly ICommandHandler<DepositCommand> deposit;
        readonly IQueryHandler<GetStatementQuery, Statement> getStatement;
        readonly IQueryHandler<GetBalanceQuery, decimal> getBalance;

        public BankController(ILogger<BankController> logger, 
                              ICommandHandler<WithdrawCommand> withdraw, 
                              ICommandHandler<DepositCommand> deposit,
                              IQueryHandler<GetStatementQuery, Statement> getStatement,
                              IQueryHandler<GetBalanceQuery, decimal> getBalance)
        {
            logger.LogInformation("Setting up the Bank Controller...");
            this.logger = logger;
            this.withdraw = withdraw;
            this.deposit = deposit;
            this.getStatement = getStatement;
            this.getBalance = getBalance;
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
        public async Task<Statement> AccountStatement(Guid accountId)
        {
            // A bank statement that covers all time, i.e., don't worry about date ranges
            throw new Exception("Implement retrieval of statement... are you bored yet?? :D");
        }

        [HttpGet("deposit/{accountId}/{amount}")]
        public async Task<decimal> Deposit(Guid accountId, decimal amount)
        {
            throw new NotImplementedException("Implement the 'Deposit' command, and pass it to execute.. God this is boring right :-O");
            await this.deposit.Execute(null); // Clearly, this is not right, tut tut Mark, do your job properly!
            logger.LogInformation($"Added amount £{amount:0.00} to the account {accountId}");

            return await getBalance.Execute(GetBalanceQuery.ForAccount(accountId));
        }

        [HttpGet("withdraw/{accountId}/{amount}")]
        public async Task<string> Withdraw(Guid accountId, decimal amount)
        {
            try
            {
                await this.withdraw.Execute(null); // Clearly, this is not right, tut tut Mark, do your job properly!
                logger.LogInformation($"Subtracted amount £{amount:0.00} from the account {accountId}");
            }
            catch (Exception e)
            {
                // See if you can make the program state failure for overdraft usage attempt...
                // Also use the logger to catch the exception so it can be seen in the console.
                Console.WriteLine(e);
                throw;
            }
            
            return Convert.ToString(await getBalance.Execute(GetBalanceQuery.ForAccount(accountId)), CultureInfo.InvariantCulture);
        }
    }
}