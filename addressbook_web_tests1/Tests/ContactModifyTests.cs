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
            appManager.Navigator.GoToHomePage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
            appManager.Contacts.ModifyContact(1);
            ContactData contact = new ContactData("updIvan", "updPetrov", "updNick");
            appManager.Contacts.FillContactForm(contact);
            appManager.Contacts.SubmitContactModify();
            appManager.Contacts.ReturnToHomePageafterUpd();

        }
    }
}
