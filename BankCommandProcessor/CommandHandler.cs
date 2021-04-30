using System;
using System.Linq;
using System.Threading.Tasks;
using BankAccountReader;
using BankDomain;

namespace BankCommandProcessor
{
    public class CommandHandler : ICommandHandler
    {
        private readonly ITransactionEventStore bankTransactionEventStore;

        public CommandHandler(ITransactionEventStore bankTransactionEventStore)
        {
            this.bankTransactionEventStore = bankTransactionEventStore;
        }

        // TODO: In the future use correlation id
        public async Task Execute(TransactionCommand command)
        {
            var currentEvents = (await bankTransactionEventStore.ReadAll(command.AccountId)).ToArray();
            var currentVersion = currentEvents.Length;
            var balance = BalanceReport.GetBalance(currentEvents);

            TransactionEvent evt = command switch
            {
                DepositCommand deposit =>
                    new Deposit(deposit.Amount, deposit.AccountId, DateTimeOffset.Now, deposit.CorrelationId),
                WithdrawalCommand withdrawal when balance >= withdrawal.Amount =>
                    new Withdrawal(withdrawal.Amount, withdrawal.AccountId, DateTimeOffset.Now, withdrawal.CorrelationId),
                _ => 
                    throw new ArgumentException()
            };

            await bankTransactionEventStore.Append(currentVersion, evt);
        }
    }

    public interface ICommandHandler
    {
        Task Execute(TransactionCommand command);
    }
}