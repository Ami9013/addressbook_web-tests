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
    public class ContactCreationTests : ContactTestBase
    {
        /// <summary>
        /// Провайдер тестовых данных для контактов без использования файла
        /// </summary>
        public static IEnumerable<ContactData> RandomContactsDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < 2; i++)
            {
                contacts.Add(new ContactData()
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
                });
            }
            return contacts;
        }



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


        [Test, TestCaseSource("RandomContactsDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {

            List<ContactData> oldContacts = ContactData.GetAll();

            appManager.Contacts.ContactCreate(contact);

            Assert.AreEqual(oldContacts.Count + 1, appManager.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            appManager.Auth.Logout();
        }
    }
}
