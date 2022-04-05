using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        /// <summary>
        /// Читает данные из XML файла
        /// </summary>
        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            //возвращаем, приведенный в явном виде к типу List<ContactData>, результат десериализации(прочтения) из файла
            //XmlSerializer читает данные типа List<ContactData>
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));

        }

        /// <summary>
        /// Читает данные из JSON файла
        /// </summary>
        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            //возвращает результат десериализации(прочтения) объекта типа<List<ContactData>>
            //в качестве параметра для прочтения передается текст, полученный ReadAllText(из файла)
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));

        }

        [Test, TestCaseSource("ContactDataFromJsonFile")] //добавить генератор как в группах(+ там раскомментировать)
        public void ContactCreationTest(ContactData contact)
        {   
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
