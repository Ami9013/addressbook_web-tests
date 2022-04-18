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
            appManager.Contacts.ContactCheck();
            appManager.Groups.GroupCheck();


            //List<ContactData> contactList = appManager.Contacts.GetContactList(); // мб понабодится для цикла, пока не используется

            List<ContactData> allContact = ContactData.GetAll(); // получаем общий список контактов из БД
            List<GroupData> allGroupList = GroupData.GetAll(); // получаем общий список групп из БД
            List<GroupData> diffGroup = new List<GroupData>(); // временный список групп, в которые добавлен проверяемый контакт


            for (int i = 0; i < allContact.Count; i++)
            {
                ContactData contactForView = ContactData.GetAll()[i];
                diffGroup = contactForView.GetGroupRelationToRemove();
                if (diffGroup.Count < allGroupList.Count) // если список групп, в которых состоит контакт меньше чем общее количество групп - выбираем этот контакт для добавления в группу
                {
                    allGroupList.Sort();
                    diffGroup.Sort();
                    int count = 0;
                    int cnt = 1;
                    foreach (GroupData item in allGroupList)
                    {

                        if (diffGroup.Count >= cnt)
                        {
                            if (item.Id == diffGroup[count].Id)
                            {
                                count++;
                                cnt++;
                                continue;
                            }                            
                        }
                        appManager.Contacts.AddContactToGroup(contactForView, item);
                    }     
                }
            }

            //TODO:
            //Описать шаги комментами
            // Добавить шаг с созданием нового контакта если все контакты есть во всех группах


            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();

            
            ContactData contact = ContactData.GetAll().Except(oldList).First();


            appManager.Contacts.AddContactToGroup(contact, group);


            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();


            Assert.AreEqual(oldList, newList);

            // по каждому контакту получить все его связи из адресс_ин_групс
            // сверить количество связей с группами которые вообще есть
            // по id группы опрелить какой нет и туда добавлять

        }

        [Test]
        public void TestRemoveContactFromGroup()
        {
            appManager.Contacts.ContactCheck();
            appManager.Groups.GroupCheck();

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
