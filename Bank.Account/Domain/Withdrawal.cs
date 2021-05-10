using System;

namespace Bank.Account.Domain
{
    internal class Withdrawal : TransactionEvent
    {
        internal Withdrawal(decimal amount, Guid accountId, DateTimeOffset timestamp, Guid correlationId)
            : base(amount, accountId, timestamp, correlationId)
        {
        }
    }
}