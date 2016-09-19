using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Abstract=cannot instantiate. partial or no implementation hence "MustInherit" to a child who completes implementation.<>interface has NO implementation. Abstract being a class can be the ONLY parent(base) while interface can be one of many parent-interfaces. Abstract act as a base(parent) to saving, checking, & visa classes

namespace AccountsDemo
{
    abstract class Account
    {
        public readonly List<Person> holders=new List<Person>();
        public readonly List<Transaction> transactions=new List<Transaction>();
        private static int LAST_NUMBER=100000; //class var
        public readonly string Number;
        public double Balance { get; protected set; } //modified in the Deposit() method and in the PrepareMonthlyReport() of child. 
        public double LowestBalance { get; protected set; }  //modified in the Deposit() method
        
        public Account(string type, double startingBalance) 
        {
            Balance = startingBalance;
            LowestBalance = startingBalance;
            Number = type + "-"+LAST_NUMBER;
            LAST_NUMBER++;
        }

        public void AddUser(Person person) //Bank has list of ALL persons(e pwd & SIN), one of them becomes "holder" of this account.
        {
            holders.Add(person);
        }

        public void Deposit(double amount, Person person)
        {
            Balance += amount;
            LowestBalance = (Balance<LowestBalance) ? Balance : LowestBalance ;
            transactions.Add(new Transaction(Number, amount, Balance, person, DateTime.Now));
        }

        public bool IsHolder(string name) //never used!!
        {
            foreach (Person x in holders)
            { if (x.Name == name) {return true;} }
            return false;
        }

        public abstract void PrepareMonthlyReport();
        //Abstract Method cannot have a body. It's implemented in child classes
        //(Cast)Bank to Visa/Saving/Checking, in Main so that A/C-type-specific PrepareMonthlyReport() & other methods are used.


        public override string ToString()
        {
            string result = string.Empty;
            result +=Number+ "\nHolders: ";
            foreach(Person x in holders)
            {
                result += x.Name + ", ";
            }
            
            result += "\nBalance: "+Balance.ToString("C")+"\n";
            result += "Transactions:\n";
            result+="AccountNumber    Originator  Amount  Time\n";
            result+="=============    ==========  ======  ====\n";

            foreach (Transaction t in transactions)
            { result += t+"\n"; }

            return result;
        }
    }



    class CheckingAccount : Account
    {
        //fields
        public static double COST_PER_TRANSACTION=0.05;
        public static double INTEREST_RATE=0.005;
        private bool hasOverdraft;
        //methods
        public CheckingAccount(double balance=0, bool hasoverdraft=false) : base("CK", balance)
        { this.hasOverdraft = hasoverdraft; }

        new public void Deposit(double amount, Person person)
        {
            if(holders.Contains(person))
            {
                base.Deposit(amount, person);
            }
        }

        public void Withdraw(double amount, Person person)
        {
            if(person.IsAuthenticated)
            {
                if(amount<Balance || hasOverdraft)
                { base.Deposit(-amount, person); }
                //make sure -amount does minus or not?
                //or else multiply with -1
            }
        }

        //override abstract of base
        public override void PrepareMonthlyReport()
        {
            double serviceCharges = transactions.Count * COST_PER_TRANSACTION;
            double interest = Balance * INTEREST_RATE / 12;
            Balance += interest; //set: is "protected" NOT "private" so we can set it from child class
            transactions.Clear(); //transactions is re-initialized (use the Clear() method of the list class)
        }
    }


    class SavingAccount : Account
    {
        //fields
        public static double COST_PER_TRANSACTION = 0.05;
        public static double INTEREST_RATE = 0.015;
        private bool hasOverdraft;
        //methods
        public SavingAccount(double balance = 0) : base("SV", balance)
        {  }

        new public void Deposit(double amount, Person person)
        {
            if (holders.Contains(person))
            {
                base.Deposit(amount, person);
            }
        }

        public void Withdraw(double amount, Person person)
        {
            if (person.IsAuthenticated)
            {
                if (amount < Balance)
                { base.Deposit(-amount, person); }
                //make sure -amount does minus or not?
                //or else multiply with -1
            }
        }

        //override abstract of base
        public override void PrepareMonthlyReport()
        {
            double serviceCharges = transactions.Count * COST_PER_TRANSACTION;
            double interest = Balance * INTEREST_RATE / 12;
            Balance += interest; //set: is "protected" NOT "private" so we can set it from child class
            transactions.Clear(); //transactions is re-initialized (use the Clear() method of the list class)
        }
    }

    
    //inherits: Balance, LowestBalance, Number, holders, transactions, .addUser, Deposit, & IsHolder
    class VisaAccount : Account
    {
        //fields 
        
        private double creditLimit;
        public static double INTEREST_RATE= 0.1995;

        //methods
        public VisaAccount(double balance=0, double creditlimit=1200) : base("VS", balance)
        {
            this.creditLimit = creditlimit;
        }

        public void DoPayment(double amount, Person person)
        {
            base.Deposit(amount, person);
        }

        public void DoPurchase(double amount, Person person)
        {
            if((Balance-amount) > -creditLimit)
            {
                base.Deposit(-amount, person);
            }
        }

        public override void PrepareMonthlyReport()
        {
            double interest = Math.Sqrt(Math.Pow(LowestBalance, 2)) * INTEREST_RATE / 12;
            Balance -= interest; //set: is "protected" NOT "private" so we can set it from child class
            transactions.Clear(); //transactions is re-initialized (use the Clear() method of the list class)
        }
    }
}
