using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        /// <summary>
        /// Удаление через карточку редактирования
        /// </summary>
        [Test]
        public void ContactRemovalTest() 
        {
            List<ContactData> oldContacts = appManager.Contacts.GetContactList();
            ContactData toBeRemoved = oldContacts[1];

            appManager.Contacts.RemoveContactInEditCard(1);

            Assert.AreEqual(oldContacts.Count - 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            oldContacts.RemoveAt(1);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved);
            }

            appManager.Auth.Logout();
        }

        /// <summary>
        /// Удаление в таблице контактов
        /// </summary>
        [Test]
        public void ContactRemovalTestByIndex()
        {
            List<ContactData> oldContacts = appManager.Contacts.GetContactList();
            ContactData toBeRemoved = oldContacts[1];

            appManager.Contacts.RemoveByIndex(1);

            Assert.AreEqual(oldContacts.Count - 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = appManager.Contacts.GetContactList();
            oldContacts.RemoveAt(1);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved);
            }

            appManager.Auth.Logout();
        }
    }
}
