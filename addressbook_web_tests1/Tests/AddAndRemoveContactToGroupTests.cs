using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddAndRemoveContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();


            appManager.Contacts.AddContactToGroup(contact, group);


            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();


            Assert.AreEqual(oldList, newList);

        }

        [Test]
        public void TestRemoveContactFromGroup()
        {


            ContactData contact = ContactData.GetAll()[0]; 

            List<GroupData> gcrOldList = contact.GetGroupRelationToRemove(); 

            GroupData groupToRemove = GroupData.GetAll().First(); 


            appManager.Contacts.RemoveContactFromGroup(contact, groupToRemove);

            List<GroupData> gcrNewList = contact.GetGroupRelationToRemove();
            gcrOldList.Remove(groupToRemove);
            gcrOldList.Sort();
            gcrNewList.Sort();

            Assert.AreEqual(gcrOldList, gcrNewList);
        }
    }
}
