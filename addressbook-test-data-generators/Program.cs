using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using WebAddressbookTests;


namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string obj = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            if (obj == "groups")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    //создаём объекты и добавляем их в список groups
                    groups.Add(new GroupData()
                    {
                        // в поля записываются значения сгенерированные функцией создающей случайную строку некоторой длинны
                        Name = TestBase.RandomDataGenerator(10),
                        Header = TestBase.RandomDataGenerator(10),
                        Footer = TestBase.RandomDataGenerator(10)
                    });
                }


                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecogonized format " + format);
                }

                writer.Close();
            }
            else if (obj == "contacts")
            {
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData()
                    {
                        FirstName = TestBase.RandomDataGenerator(10),
                        MiddleName = TestBase.RandomDataGenerator(10),
                        LastName = TestBase.RandomDataGenerator(10),
                        NickName = TestBase.RandomDataGenerator(10),
                        Title = TestBase.RandomDataGenerator(10),
                        Company = TestBase.RandomDataGenerator(10),
                        FirstAddress = TestBase.RandomDataGenerator(10),
                        FirstHomePhone = TestBase.RandomPhoneData(11),
                        Mobile = TestBase.RandomPhoneData(11),
                        WorkPhone = TestBase.RandomPhoneData(11),
                        Fax = TestBase.RandomDataGenerator(10),
                        Email = ($"{ TestBase.RandomDataGenerator(10)}@mail.no"),
                        Email2 = ($"{ TestBase.RandomDataGenerator(10)}@mail.no"),
                        Email3 = ($"{ TestBase.RandomDataGenerator(10)}@mail.no"),
                        Homepage = TestBase.RandomDataGenerator(10),
                        DayOfBirth = TestBase.RandomDigit(32),
                        MonthOfBirth = TestBase.RandomMonth(),
                        YearOfBirth = TestBase.RandomYearData(),
                        DayOfAnniversary = TestBase.RandomDigit(32),
                        MonthOfAnniversary = TestBase.RandomMonth(),
                        YearOfAnniversary = TestBase.RandomYearData(),
                        GroupOfContact = TestBase.RandomDigit(5),
                        SecondAddress = TestBase.RandomDataGenerator(10),
                        SecondHomePhone = TestBase.RandomPhoneData(11),
                        SecondNotes = TestBase.RandomDataGenerator(10)
                    });
                }

                if (format == "xml")
                {
                    writeContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    writeContactsToJsonFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecogonized format " + format);
                }

                writer.Close();
            }

            else
            {
                System.Console.Out.Write("Unrecogonized object " + obj);
                writer.Close();
            }
        }


            

        // метод, осуществляющий запись в файл формата CSV
        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }

        }

        // метод, осуществляющий запись в файл формата XML
        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            //Создаём новый сериализатор(указываем данные какого типа он будет сериализовывать), затем в Serialize передаём (куда, что)
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        // метод, осуществляющий запись в файл формата JSON
        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }







        // метод, осуществляющий запись в файл формата XML
        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            //Создаём новый сериализатор(указываем данные какого типа он будет сериализовывать), затем в Serialize передаём (куда, что)
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        // метод, осуществляющий запись в файл формата JSON
        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

    }
}
