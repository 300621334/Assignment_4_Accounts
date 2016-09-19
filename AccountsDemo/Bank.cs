using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDemo
{
    static class Bank
    {
        //fields
        private static List<Account> accounts;
        private static List<Person> persons;


        //methods
        static Bank()//no public(access modifier) etc allowed on static constructor
        {
            CreatePerson();
            CreateAccount();
            
        }

        //default is "private"
        //name & SIN#
        static void CreatePerson()
        {
            persons = new List<Person>(){
                new Person("Narendra", "1234-5678"),
                new Person("Ilia", "2345-6789"),
                new Person("Tom", "3456-7890"),
                new Person("Syed", "4567-8901"),
                new Person("Arben", "5678-9012"),
                new Person("Patrick", "6789-0123"),
                new Person("Yin", "7890-1234"),
                new Person("Hao", "8901-2345"),
                new Person("Ilir", "9012-3456")
            };

        }

        private static void CreateAccount()
        {
            accounts = new List<Account>{
                new VisaAccount(), //vs-100000  bal=0 limit-1200
                new VisaAccount(50, 200), //vs-100001  bal=50 limit=200
                new SavingAccount(5000), //sv-100002
                new SavingAccount(), //sv-100003
                new CheckingAccount(2000), //ck-100004
                new CheckingAccount(1500, true) //ck-100005
            };


        }


        public static void PrintAccounts()
        {
            foreach(Account x in accounts)
            {
                Console.WriteLine(x);
            }
        }

        public static void PrintPersons()
        {
            foreach (Person x in persons)
            {
                Console.WriteLine(x);
            }
        }

        
        public static Person GetPerson(string name)
        {
            Person person = null;
            foreach(Person x in persons)
            {
                if(x.Name==name)
                {
                    person = x;
                }
            }
            return person;

        }

        public static Account GetAccount(string number)
        {
            Account account = null;
            foreach (Account x in accounts)
            {
                if (x.Number == number)
                {
                    account = x;
                }
            }
            return account;
        }
    }
}
