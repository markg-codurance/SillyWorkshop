using System.Threading.Tasks;

namespace Bank.Infrastructure
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task Execute(TCommand command);
    }
}