using System;
using Bank.Infrastructure;

namespace Bank.Account.Domain
{
    internal class TransactionEvent : DomainEvent
    {
        protected TransactionEvent(decimal amount, Guid accountId, DateTimeOffset timestamp, Guid correlationId)
        {
            Amount = amount;
            AccountId = accountId;
            Timestamp = timestamp;
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; }
        public DateTimeOffset Timestamp { get; }
        public decimal Amount { get; }
    }
}