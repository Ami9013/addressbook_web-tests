using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData
            {
                FirstName = "Vanya",
                MiddleName = "Petrovich",
                LastName = "Petrov",
                NickName = "Nick",
                Title = "Any",
                Company = "Magazine",
                FirstAddress = "Any city, any street",
                FirstHome = "111",
                Mobile = "88005553535",
                Work = "Main Cashier",
                Fax = "123321",
                Email = "vandamm0123@mail.no",
                Email2 = "vandamm0133@mail.no",
                Email3 = "vandamm0333@mail.no",
                Homepage = "n/a",
                DayOfBirth = 2,
                MonthOfBirth = 4,
                YearOfBirth = "1971",
                DayOfAnniversary = 2,
                MonthOfAnniversary = 9,
                YearOfAnniversary = "1996",
                GroupOfContact = 2,
                SecondAddress = "kolotushkina street",
                SecondHome = "101/1",
                SecondNotes = "done"
            };

            List<ContactData> oldContacts = appManager.Contacts.GetContactList();

            appManager.Contacts.ContactCreate(contact);

            Assert.AreEqual(oldContacts.Count + 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            appManager.Auth.Logout();
        }
    }
}
