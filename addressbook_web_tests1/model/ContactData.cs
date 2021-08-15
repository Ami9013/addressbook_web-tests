using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
    {
        private string firstName;
        private string lastName;
        private string nickName = "";

        public ContactData(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public ContactData(string firstName, string lastName, string nickName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.nickName = nickName;
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        public string NickName
        {
            get
            {
                return nickName;
            }
            set
            {
                nickName = value;
            }
        }
    }
}
