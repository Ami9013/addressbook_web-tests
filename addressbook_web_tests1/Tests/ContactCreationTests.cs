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
            appManager.Navigator.GoToHomePage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
            appManager.Navigator.GoToAddContactPage();
            ContactData contact = new ContactData("Ivan", "Petrov");
            appManager.Contacts.FillContactForm(contact);
            appManager.Contacts.SubmitContactCreation();
            appManager.Navigator.ReturnToHomePage();
        }
    }
}
