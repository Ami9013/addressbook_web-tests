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
            ContactData contact = new ContactData
            {
                FirstName = "Vanya",
                LastName = "Petrov",
                YearOfAnniversary = "1996",
                YearOfBirth = "1971",
                Email = "vandamm0123@mail.no",
                Mobile = "88005553535"
            };
            appManager.Contacts.ContactCreate(contact);
            appManager.Auth.Logout();
        }
    }
}
