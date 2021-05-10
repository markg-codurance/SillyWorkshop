using System;
using Bank.Infrastructure;

namespace Bank.Account.UseCases.GetStatement
{
    public class GetStatementQuery : Query<Statement>
    {
        public Guid AccountId { get; set; }
    }
}