using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModifyTests : AuthTestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            ContactData newContactData = new ContactData
            {
                FirstName = "UpdVanya",
                MiddleName = "UpdPetrovich",
                LastName = "UpdPetrov",
                NickName = "UpdNick",
                Title = "UpdAny",
                Company = "UpdMagazine",
                FirstAddress = "Upd Any city, any street",
                FirstHome = "Upd 111",
                Mobile = "123321123",
                Work = "Upd Main Cashier",
                Fax = "987654",
                Email = "Updvandamm0123@mail.no",
                Email2 = "Updvandamm0133@mail.no",
                Email3 = "Updvandamm0333@mail.no",
                Homepage = "Updn/a",
                DayOfBirth = 31,
                MonthOfBirth = 7,
                YearOfBirth = "1900",
                DayOfAnniversary = 1,
                MonthOfAnniversary = 12,
                YearOfAnniversary = "2222",
                SecondAddress = "Upd kolotushkina street",
                SecondHome = "1/101",
                SecondNotes = "upd done"
            };

            List<ContactData> oldContacts = appManager.Contacts.GetContactList();
            ContactData oldData = oldContacts[1];

            appManager.Contacts.Modify(1, newContactData);

            Assert.AreEqual(oldContacts.Count, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            oldContacts[1].FirstName = newContactData.FirstName;
            oldContacts[1].LastName = newContactData.LastName;
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newContactData.FirstName, contact.FirstName);
                    Assert.AreEqual(newContactData.LastName, contact.LastName);
                }
            }

            appManager.Auth.Logout();
        }
    }
}
