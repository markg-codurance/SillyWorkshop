using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankDomain;

namespace BankAccountReader
{
    public class BalanceReport : IReadBalance
    {
        private readonly ITransactionEventStore eventStore;

        public BalanceReport(ITransactionEventStore eventStore)
        {
            // Now we can see the eventStore is being used directly.
            // Just delete the exception so that it can be used in the methods below.
            throw new Exception("Clean this");
            this.eventStore = eventStore;
        }

        public static decimal GetBalance(IEnumerable<TransactionEvent> events)
        {
            
            return events
                // fold over the events, where 'balance' is clearly the accumulator.
                .Aggregate(0M, (balance, evt) => 
                {
                    return evt switch
                    {
                        Deposit d => 
                        // see if you can put the implementation in place...
                        throw new NotImplementedException("You need to implement the deposit calculation..."),
                        // see if you can put the implementation in place...
                        Withdrawal w => 
                        throw new NotImplementedException("You need to implement the withdraw calculation..."),
                        _ => balance
                    };
                });
        }
        
        // 'Pure Projection' : Lives inside an Aggregate
        public async Task<decimal> GetBalance(Guid accountId)
        {
            // Here you can see that the eventStore merely returns all events and passes
            // them on to be calculated. The idea is that the GetBalance method is 'pure',
            // it is idempotent because you will always get the same result if you give
            // it the same input, and there are no side effects, making the code very
            // 'reasonable'. You can be sure of the behaviour whereas in a mutation heavy
            // paradigm you can't look at a chunk of code and say with certainty that the
            // code will be in a particular state.
            throw new Exception("Implement the ReadAll capability."); 
        }
    }
}