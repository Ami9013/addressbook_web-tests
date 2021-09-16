using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData(
                "Ivan", 
                "Petrov", 
                "Nick", 
                "Andreevich", 
                "Title", 
                "Magnit", 
                "Voronezh city, Lizyukova street", 
                "10", 
                "+1123456", 
                "Cashier", 
                "54673", 
                "petro12@mail.no", 
                "2petro12@mail.no", 
                "3petro12@mail.no", 
                "N/A"
                );
            appManager.Contacts.ContactCreate(contact);
            appManager.Auth.Logout();
        }
    }
}
