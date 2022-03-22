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
            string typeOfData = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
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

            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < count; i++)
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
                    FirstHomePhone = TestBase.GenerateRandomString(10), //нужны цифры, допустимы скобки, тире и +
                    Mobile = TestBase.GenerateRandomString(10), //нужны цифры, допустимы скобки, тире и +
                    WorkPhone = TestBase.GenerateRandomString(10), //нужны цифры, допустимы скобки, тире и +
                    Fax = TestBase.GenerateRandomString(10), //нужны цифры + буквы
                    Email = TestBase.GenerateRandomString(10), //нужны цифры, допустимы скобки, тире и + //нужно делать с окончанием @mail.no
                    Email2 = TestBase.GenerateRandomString(10), //нужны цифры, допустимы скобки, тире и + //нужно делать с окончанием @mail.no
                    Email3 = TestBase.GenerateRandomString(10), //нужны цифры, допустимы скобки, тире и + //нужно делать с окончанием @mail.no
                    Homepage = TestBase.GenerateRandomString(10), //нужны цифры, допустимы скобки, тире и + //нужно делать с окончанием @mail.no
                    DayOfBirth = 2, //int 0-31
                    MonthOfBirth = 4, //int 0-12
                    YearOfBirth = TestBase.GenerateRandomString(10), //наверное желательны только цифры
                    DayOfAnniversary = 2,//int 0-31
                    MonthOfAnniversary = 9,//int 0-12
                    YearOfAnniversary = TestBase.GenerateRandomString(10), //наверное желательны только цифры
                    GroupOfContact = 2, //рандомное число от 0 до скольки то
                    SecondAddress = TestBase.GenerateRandomString(10),
                    SecondHomePhone = TestBase.GenerateRandomString(10), //нужны цифры, допустимы скобки, тире и +
                    SecondNotes = TestBase.GenerateRandomString(10),
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

    }
}
