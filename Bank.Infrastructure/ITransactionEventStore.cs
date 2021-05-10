using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Infrastructure
{
    public interface ITransactionEventStore
    {
        Task Append(int expectedVersion, DomainEvent transactionEvent);
        Task<IEnumerable<DomainEvent>> ReadAll(Guid accountId);
    }
}