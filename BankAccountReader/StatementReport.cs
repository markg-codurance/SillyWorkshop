using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankDomain;

namespace BankAccountReader
{
    public class StatementReport
    {
        public StatementReport(ITransactionEventStore eventStore)
        {
            /*
             * We should have this class implement the port defined
             * in the domain, make the changes required. if you get
             * stuck just let me know and I will give you a clue.
             */
            throw new Exception("Implement the port fully... WTF! What does that mean!?!?!?");
        }

        public async Task<IEnumerable<TransactionEvent>> GetStatement(Guid accountId)
        {
            /*
             * Here you need to simply return all the events back to the caller.
             * This should be a one liner, see if you can figure it out.
             */
            throw new Exception("Implement me... Mmmmwwwahahaha");
        }
    }
}