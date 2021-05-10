using System;

namespace Bank.Account.Domain
{
    internal class InsufficientFundsAvailableException : Exception
    {
        internal InsufficientFundsAvailableException(string msg) : base(msg)
        {
            throw new Exception("Added this exception to show that the pattern match can be used to direct usage... But how?");
        }
    }
}