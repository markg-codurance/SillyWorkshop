using System.Threading.Tasks;
using Bank.Infrastructure;

namespace Bank.Account.UseCases.Withdraw
{
    internal class WithdrawCommandHandler : ICommandHandler<WithdrawCommand>
    {
        public Task Execute(WithdrawCommand command) => throw new System.NotImplementedException();
    }
}