using System.Threading.Tasks;
using Bank.Infrastructure;

namespace Bank.Account.UseCases.GetStatement
{
    internal class GetStatementQueryHandler : IQueryHandler<GetStatementQuery, Statement>
    {
        public Task<Statement> Execute(GetStatementQuery query) => throw new System.NotImplementedException();
    }
}