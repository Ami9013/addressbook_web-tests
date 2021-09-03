using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; } = "";

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactData(string firstName, string lastName, string nickName)
        {
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
        }
    }
}
