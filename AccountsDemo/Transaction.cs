using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDemo
{
    class Transaction
    {
        //fields
        readonly string AccountNumber;
        readonly double Amount;
        readonly double EndBalance;
        readonly Person Originator;
        readonly DateTime Time;
        //mehods
        public Transaction(string accountNumber, double amount, double endBalance, Person person, DateTime time)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            EndBalance = endBalance;
            Originator = person;
            Time = time;
        }   
        public override string ToString()
        {
            return string.Format("{0, -17}{1, -12}{2, -8}{3}", AccountNumber, Originator.Name, Amount, Time.ToShortTimeString());
        }
    }
}
