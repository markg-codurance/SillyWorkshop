using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankDomain;

namespace BankStore
{
    public class BankTransactionEventStore : ITransactionEventStore
    {
        private readonly ConcurrentDictionary<Guid, List<TransactionEvent>> events = new ConcurrentDictionary<Guid, List<TransactionEvent>>();

        public BankTransactionEventStore()
        {
            /*
             * Strictly speaking, this constructor isn't necessary. I included
             * it here so I could draw your attention to the 'events' member above.
             * The horrible looking signature tells you that the it is a dictionary
             * that has a globally unique id for the key and a List of TransactionEvent
             * as the value. The GUID represents an identifier for a bank account.
             * Obviously it does not adhere to international standards in terms of the
             * account number format, but this is not important when considering an
             * event sourcing implementation.
             *
             * You can delete this comment and the constructor now I have pointed it out.
             */
            throw new Exception("Read here, some relatively informative informational info detected...");
        }
        
        public async Task Append(int expectedVersion, TransactionEvent transactionEvent)
        {
            var evts = (await ReadAll(transactionEvent.AccountId)).ToArray();
            var version = evts.Length;

            /*
             * Using the length of the evts array as the version number.
             * In event sourcing you need to know what the version is to make
             * sure someone else hasn't updated the store before you.
             * Remember we are in an eventually consistent environment so
             * things are going to change. Historically, database philosophy
             * was all pessimistic, you can even do Google search and read
             * about adlock pessimistic and what it means. Essentially,
             * consistency was king, you made certain as far as possible that
             * updates were serialised one after the other in order. We are now
             * in the 2020's that likes of Amazon have turned that paradigm
             * pretty much completely up-side-down. Consistency is important
             * but instantaneous is no longer required (most of the time).
             */
            if (version != expectedVersion)
            {
                throw new Exception("Versions don't match");
            }


            // In the real world of course, we would require customers to perform
            // all manner of checks to create an account. Here we just create one 
            // as it is not part of the current demonstration.
            if (!events.ContainsKey(transactionEvent.AccountId))
            {
                events.TryAdd(transactionEvent.AccountId, new List<TransactionEvent>());
            }

            /*
             * Here we can perform a check to make sure we don't add the same
             * event to the store twice for a particular account. Use the
             * correlation id here, but add by the account id.
             */
            throw new NotImplementedException("You must implement persisting of an event...");
            if (false)
            {
                // append the event here:
                
                
                
                // Now you can trigger other actions, if necessary (there aren't any required in our program):
            }

        }

        public Task<IEnumerable<TransactionEvent>> ReadAll(Guid accountId)
        {
            /*
             * Here the event store needs to return all the events for a particular
             * account id. If you know how to use Linq then go ahead, otherwise
             * you may have to resort to an imperative set of loops of some kind.
             */
            throw new NotImplementedException("You must implement a full read for a particular account id...");
        }
    }
}