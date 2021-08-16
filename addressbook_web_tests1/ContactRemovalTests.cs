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
            appManager.Navigator.GoToHomePage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
            appManager.Contacts.SelectContact(1);
            appManager.Contacts.RemoveContact();
            appManager.Contacts.ContactCloseAlert();
            appManager.Navigator.ReturnToHomePage();
        }
    }
}
