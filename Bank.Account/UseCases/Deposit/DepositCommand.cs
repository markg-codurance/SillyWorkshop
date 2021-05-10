using System;

namespace Bank.Account.UseCases.Deposit
{
    public class DepositCommand
    {
        public Guid CorrelationId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; } 
    }
}