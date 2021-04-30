using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDomain
{
    public class InsufficientFundsAvailableException : Exception
    {
        public InsufficientFundsAvailableException(string msg) : base(msg)
        {
            throw new Exception("Added this exception to show that the pattern match can be used to direct usage... But how?");
        }
    }
    
    public class TransactionEvent
    {
        protected TransactionEvent(decimal amount, Guid accountId, DateTimeOffset timestamp, Guid correlationId)
        {
            Amount = amount;
            AccountId = accountId;
            Timestamp = timestamp;
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; }
        public DateTimeOffset Timestamp { get; }
        public Guid AccountId { get; }
        public decimal Amount { get; }
    }

    public class Deposit : TransactionEvent
    {
        public Deposit(decimal amount, Guid accountId, DateTimeOffset timestamp, Guid correlationId)
            : base(amount, accountId, timestamp, correlationId)
        {
            /*
             * Using inheritance here so I don't need
             * to define the same properties over and over.
             */
            throw new Exception("Read me... Your sanity depends on it.");
        }
    }

    public class Withdrawal : TransactionEvent
    {
        public Withdrawal(decimal amount, Guid accountId, DateTimeOffset timestamp, Guid correlationId)
            : base(amount, accountId, timestamp, correlationId)
        {
        }
    }

/* Ports
 * These interfaces are going to need to be satified... Somewhere...
 */
    public interface ITransactionEventStore
    {
        Task Append(int expectedVersion, TransactionEvent transactionEvent);
        Task<IEnumerable<TransactionEvent>> ReadAll(Guid accountId);
    }

    // Contrived port just to show it can be done this way
    // but I would have prefered to use a method signature
    // as the port instead of an interface as it seems, to
    // me at least, a little heavy handed to require a 
    // whole new class just to implement one menthod.
    public interface IReadBalance
    {
        Task<decimal> GetBalance(Guid accountId);
    }

    public interface IReadStatement
    {
        Task<IEnumerable<TransactionEvent>> GetStatement(Guid accountId);
    }


/*
// Basic core of the Hexagon for an online shop in pseudocode.
// Using the concept of a module to group related things together.
module Domain =

  type Basket        = { List of Item }
  type Customer      = { Single Basket, List of CardDetail, List of Address }
  type CardDetail    = { Single CardNumber, Single ExpiryDate, Single Cvv }
  type OrderDetails  = { List of Item, Decimal Total }
  type PaymentResult = { Choice from Success | Failure of reason:string }
  type Item          = { String name, String description, Decimal price }
  type Address       = { String firstLine, String secondline, String city, String postCode }
  
  // in ports (driver)
  abstract AddToBasket : Basket+Item->Basket
  abstract AddAddress  : Customer+Address->Customer

  // out port (driven)  
  abstract Pay         : Basket+CardDetail+OrderDetails->PaymentResult

module UseCases =
  open Domain
  fn checkout    = Pay
  fn addToBasket = AddToBasket
  fn addAddress  = AddAddress

// Not core of the Hexagon
type PaymentAdapter implements Pay
 */
}