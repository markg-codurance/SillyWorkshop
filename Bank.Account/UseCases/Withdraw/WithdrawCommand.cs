using System;

namespace Bank.Account.UseCases.Withdraw
{
    public class WithdrawCommand
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}