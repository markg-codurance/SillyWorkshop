using System;

namespace Bank.Account.Domain
{
    internal class Deposit : TransactionEvent
    {
        internal Deposit(decimal amount, Guid accountId, DateTimeOffset timestamp, Guid correlationId)
            : base(amount, accountId, timestamp, correlationId)
        {
            /*
             * Using inheritance here so I don't need
             * to define the same properties over and over.
             */
            throw new Exception("Read me... Your sanity depends on it.");
        }
    }
}