using System;

namespace Bank.Infrastructure
{
    public class DomainEvent
    {
        public Guid AccountId { get; set; } // Aggegate root id really
    }
}