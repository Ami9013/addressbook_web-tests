using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        /// <summary>
        /// Удаление через карточку редактирования
        /// </summary>
        [Test]
        public void ContactRemovalTest() 
        {
            List<ContactData> oldContacts = ContactData.GetAll();
            if (oldContacts.Count == 0)
            {
                appManager.Contacts.ContactCreate(ContactData.contactModel);
                oldContacts = ContactData.GetAll();
            }
            ContactData toBeRemoved = oldContacts[0];

            appManager.Contacts.RemoveContactInEditCard(toBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved);
            }
        }

        /// <summary>
        /// Удаление в таблице контактов
        /// </summary>
        [Test]
        public void ContactRemovalTestByIndex()
        {
            List<ContactData> oldContacts = ContactData.GetAll();
            if (oldContacts.Count == 0)
            {
                appManager.Contacts.ContactCreate(ContactData.contactModel);
                oldContacts = ContactData.GetAll();
            }
            ContactData toBeRemoved = oldContacts[1];

            appManager.Contacts.RemoveByIndex(1);

            Assert.AreEqual(oldContacts.Count - 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(1);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved);
            }
        }
    }
}
