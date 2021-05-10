using System;
using System.Threading.Tasks;
using Bank.Infrastructure;

namespace Bank.Account.UseCases.GetBalance
{
    internal class GetBalanceQueryHandler : IQueryHandler<GetBalanceQuery, decimal>
    {
        public Task<decimal> Execute(GetBalanceQuery query) => throw new NotImplementedException();
    }
}