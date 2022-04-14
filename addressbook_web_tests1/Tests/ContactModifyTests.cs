using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModifyTests : ContactTestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            ContactData newContactData = new ContactData
            {
                FirstName = TestBase.GenerateRandomString(10),
                MiddleName = TestBase.GenerateRandomString(10),
                LastName = TestBase.GenerateRandomString(10),
                NickName = TestBase.GenerateRandomString(10),
                Title = TestBase.GenerateRandomString(10),
                Company = TestBase.GenerateRandomString(10),
                FirstAddress = TestBase.GenerateRandomString(10),
                FirstHomePhone = TestBase.GenerateRandomString(11),
                Mobile = TestBase.GenerateRandomString(11),
                WorkPhone = TestBase.GenerateRandomString(11),
                Fax = TestBase.GenerateRandomString(10),
                Email = ($"{ TestBase.GenerateRandomString(10)}@mail.no"),
                Email2 = ($"{ TestBase.GenerateRandomString(10)}@mail.no"),
                Email3 = ($"{ TestBase.GenerateRandomString(10)}@mail.no"),
                Homepage = TestBase.GenerateRandomString(10),
                DayOfBirth = TestBase.RandomDigit(32),
                MonthOfBirth = TestBase.RandomMonth(),
                YearOfBirth = TestBase.RndYearBuilder(),
                DayOfAnniversary = TestBase.RandomDigit(32),
                MonthOfAnniversary = TestBase.RandomMonth(),
                YearOfAnniversary = TestBase.RndYearBuilder(),
                GroupOfContact = TestBase.RandomDigit(5),
                SecondAddress = TestBase.GenerateRandomString(10),
                SecondHomePhone = TestBase.GenerateRandomString(11),
                SecondNotes = TestBase.GenerateRandomString(10)
            };

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[0];

            appManager.Contacts.Modify(oldData, newContactData);

            Assert.AreEqual(oldContacts.Count, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].FirstName = newContactData.FirstName;
            oldContacts[0].LastName = newContactData.LastName;
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
