using System;
using Bank.Account.UseCases.GetStatement;
using Bank.Infrastructure;

namespace Bank.Account.UseCases.GetBalance
{
    public class GetBalanceQuery : Query<decimal>
    {
        public Guid AccountId { get; set; }
        
        public static GetBalanceQuery ForAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }
    }
}