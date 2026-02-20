using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; }
        public Money(decimal amount) { 
            
            if(amount < 0) throw new ArgumentException("Amount cannot be negative");

            Amount = amount; }
    }
}
