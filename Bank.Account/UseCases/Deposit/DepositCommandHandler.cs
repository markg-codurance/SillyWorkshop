using System;
using System.Linq;
using System.Threading.Tasks;
using Bank.Infrastructure;

namespace Bank.Account.UseCases.Deposit
{
    internal class DepositCommandHandler : ICommandHandler<DepositCommand>
    {
        readonly ITransactionEventStore store;
        
        public DepositCommandHandler(ITransactionEventStore store)
        {
            this.store = store;
        }

        public async Task Execute(DepositCommand command)
        {
            var currentEvents = (await store.ReadAll(command.AccountId)).ToArray();
            var currentVersion = currentEvents.Length;

            var deposit = new Domain.Deposit(command.Amount, command.AccountId, DateTimeOffset.Now,
                command.CorrelationId);
            await store.Append(currentVersion, deposit);
        }
    }
}