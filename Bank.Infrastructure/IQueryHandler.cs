using System.Threading.Tasks;

namespace Bank.Infrastructure
{
    public interface IQueryHandler<TQuery, TQueryResult> where TQuery : Query<TQueryResult> 
    {
        Task<TQueryResult> Execute(TQuery query);
    }
}
