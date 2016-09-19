using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDemo
{
    class Person
    {
        //fields
        private string password;
        public readonly string SIN; /*readonly modifier, assignments to the fields introduced by the declaration can only occur as part of the declaration or in a constructor in the same class.
        You can assign a value to a readonly field only in the following contexts:

    (1)When the variable is initialized in the declaration, for example:

    public readonly int y = 5;

    (2)For an instance field, in the instance constructors of the class that contains the field declaration, or for a static field, in the static constructor of the class that contains the field declaration. These are also the only contexts in which it is valid to pass a readonly field as an out or ref parameter.
        */
                                    
        
        //properties
        public bool IsAuthenticated { get; private set; }//is modified in the Login() and the Logout() methods
        public string Name { get; private set; } //name of person
        //Methods
        public void Login(string pwd)
        {
            //this.password not work coz it's static, so Person.password works.
            //IsAuthenticated=(Person.password == pwd)?true:false;
            if(password==pwd)
            {
                IsAuthenticated = true;
            }
        }
        public void Logout()
        {
            IsAuthenticated = false;
        }
        //constructor
        public Person(string name, string sin)
        {
            Name = name;
            SIN = sin;
            password = sin.Substring(0, 3); //1st 3 letters of SIN set as pwd
        }
        public override string ToString()
        {
            return string.Format("{0} is {1}", Name, IsAuthenticated ?"Authenticated":"Not authenticated");
        }


    }
}
