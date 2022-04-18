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
            List<GroupData> allGroupList = GroupData.GetAll();
            if (allGroupList.Count == 0)
            {
                appManager.Groups.CreateGroup(GroupData.groupModel);
                allGroupList = GroupData.GetAll();
            }

            GroupData groupForView = allGroupList[0];
            var allContactsInGroup = groupForView.GetContacts();
            var diffContactsInGroup = ContactData.GetAll().Except(allContactsInGroup);

            if (diffContactsInGroup.Count() == 0)
            {
                appManager.Contacts.ContactCreate(ContactData.contactModel);
                diffContactsInGroup = ContactData.GetAll().Except(allContactsInGroup);
            }

            appManager.Contacts.AddContactToGroup(diffContactsInGroup.First(), groupForView);
            allContactsInGroup.Add(diffContactsInGroup.First());
            var newContactsInGroup = groupForView.GetContacts();

            allContactsInGroup.Sort();
            newContactsInGroup.Sort();
            Assert.AreEqual(allContactsInGroup, newContactsInGroup);            
        }



        [Test]
        public void TestRemoveContactFromGroup()
        {
            List<GroupData> allGroupList = GroupData.GetAll();
            if (allGroupList.Count == 0)
            {
                appManager.Groups.CreateGroup(GroupData.groupModel);
                allGroupList = GroupData.GetAll();
            }

            GroupData groupToRemoveFromIt = allGroupList[0];
            var allContactsInGroup = groupToRemoveFromIt.GetContacts();

            if (allContactsInGroup.Count == 0)
            {
                var allContacts = ContactData.GetAll();
                if (allContacts.Count == 0)
                {
                    appManager.Contacts.ContactCreate(ContactData.contactModel);
                    allContacts = ContactData.GetAll();
                }
                appManager.Contacts.AddContactToGroup(allContacts[0], groupToRemoveFromIt);
                allContactsInGroup.Add(allContacts[0]);
            }

            appManager.Contacts.RemoveContactFromGroup(allContactsInGroup[0], groupToRemoveFromIt);
            allContactsInGroup.RemoveAt(0);
            var newContactsInGroup = groupToRemoveFromIt.GetContacts();

            allContactsInGroup.Sort();
            newContactsInGroup.Sort();
            Assert.AreEqual(allContactsInGroup, newContactsInGroup);
        }
    }
}
