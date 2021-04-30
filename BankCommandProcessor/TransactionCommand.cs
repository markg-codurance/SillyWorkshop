using System;
using System.Diagnostics;
using System.Transactions;

namespace BankCommandProcessor
{
    public class TransactionCommand
    {
        public Guid CorrelationId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }

    public class WithdrawalCommand : TransactionCommand
    {
    }

    public class DepositCommand : TransactionCommand
    {
    }
}