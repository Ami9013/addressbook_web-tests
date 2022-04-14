using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        /// <summary>
        /// Провайдер тестовых данных для групп без использования файла
        /// </summary>
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = GenerateRandomString(30),
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }


        /// <summary>
        /// Читает данные из CSV файла
        /// </summary>
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();

            string[] lines = File.ReadAllLines(@"groups.csv");

            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData()
                {
                    Name = parts[0],
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        /// <summary>
        /// Читает данные из XML файла
        /// </summary>
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            //возвращаем, приведенный в явном виде к типу List<GroupData>, результат десериализации(прочтения) из файла
            //XmlSerializer читает данные типа List<GroupData>
            return (List<GroupData>) new XmlSerializer(typeof(List<GroupData>)).Deserialize(new StreamReader(@"groups.xml"));
            
        }

        /// <summary>
        /// Читает данные из JSON файла
        /// </summary>
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            //возвращает результат десериализации(прочтения) объекта типа<List<GroupData>>
            //в качестве параметра для прочтения передается текст, полученный ReadAllText(из файла)
            return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(@"groups.json"));

        }



        [Test, TestCaseSource("GroupDataFromJsonFile")] //определяем провайдера, который будет читать данные из файла
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);


            appManager.Auth.Logout();
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData
            {
                Name = "",
                Header = "",
                Footer = ""
            };

            List<GroupData> oldGroups = appManager.Groups.GetGroupFullData();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupFullData();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData
            {
                Name = "a'a",
                Header = "b'b",
                Footer = "c'c"
            };

            List<GroupData> oldGroups = appManager.Groups.GetGroupFullData();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupFullData();
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            appManager.Auth.Logout();
        }

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = appManager.Groups.GetGroupFullData();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }
    }
}
