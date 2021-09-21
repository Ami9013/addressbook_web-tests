using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            //appManager.Contacts.RemoveByIndex();
            appManager.Contacts.RemoveFirstContact();
            appManager.Auth.Logout();
        }
    }
}
