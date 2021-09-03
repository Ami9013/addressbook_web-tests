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
            ContactData contact = new ContactData("updIvan", "updPetrov", "updNick");
            appManager.Contacts.Modify(contact);
            appManager.Auth.Logout();
        }
    }
}
