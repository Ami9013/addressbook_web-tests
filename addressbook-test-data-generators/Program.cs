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
            /*
             * string subject - сущность для которой генерируются тестовые наборы: groups или contacts
             * int countOfSets - количество генерируемых комплектов
             * StreamWriter writer - имя файла, в который будут записываться данные
             * string format - формат файла, в которые записываются данные
             */

            string typeOfData = args[0];
            int countOfSets = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            if (typeOfData == "groups")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < countOfSets; i++)
                {
                    //создаём объекты и добавляем их в список groups
                    groups.Add(new GroupData()
                    {
                        // в поля записываются значения сгенерированные функцией создающей случайную строку некоторой длинны
                        Name = TestBase.GenerateRandomString(10),
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
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

            else if (typeOfData == "contacts")
            {
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < countOfSets; i++)
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
                System.Console.Out.Write("Unrecogonized object " + typeOfData);
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
