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
            ContactData contact = new ContactData
            {
                FirstName = "Name after update",
                LastName = "Lastname after update"
            };
            appManager.Contacts.Modify(2, contact);
            appManager.Auth.Logout();
        }
    }
}
