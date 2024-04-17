using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPCsharp
{
    public class Person
    {
        private int age;
        private string name;

        public void setAge(int age)
        {
            if (age > 100)
            {
                this.age = 100;
                return;
            }
            this.age = age;
        }

        public int getAge()
        {
            return this.age;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }

        // propfull
        private bool gender;

        public bool Gender
        {
            get { return gender; }
            set
            {
                gender = true;
            }
        }

        // prop
        public string FirstName { get; set; }

        // method;
        public virtual void sayHello()
        {
            Console.WriteLine("Hello");
        }

        public Person()
        {
            Console.WriteLine("Handsome boy");
        }
    }
}
