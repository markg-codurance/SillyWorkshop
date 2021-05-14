using System;
using Bank.Account.UseCases.Deposit;
using Bank.Account.UseCases.GetBalance;
using Bank.Account.UseCases.GetStatement;
using Bank.Account.UseCases.Withdraw;
using Bank.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Account
{
    public static class Registry
    {
        public static void RegisterBankAccountUseCases(this IServiceCollection services)
        {
            // TODO: register the command handlers
            // register the queries
            services.AddScoped<IQueryHandler<GetBalanceQuery, decimal>, GetBalanceQueryHandler>();
            services.AddScoped<IQueryHandler<GetStatementQuery, Statement>, GetStatementQueryHandler>();
        }
    }
}