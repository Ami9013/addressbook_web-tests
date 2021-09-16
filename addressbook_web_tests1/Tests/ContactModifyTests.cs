using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModifyTests : TestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            ContactData contact = new ContactData(
                "updIvan", 
                "updPetrov", 
                "updNick", 
                "updAndreevich", 
                "updTitle", 
                "updMagnit", 
                "upd Voronezh city, Lizyukova street", 
                "15", 
                "+198765", 
                "updCashier", 
                "121212", 
                "updpetro12@mail.no", 
                "upd2petro12@mail.no", 
                "upd3petro12@mail.no", 
                "updN/A"
                );
            appManager.Contacts.Modify(contact);
            appManager.Auth.Logout();
        }
    }
}
