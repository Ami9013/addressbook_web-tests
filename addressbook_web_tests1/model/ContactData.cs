using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "middlename")]
        public string MiddleName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "nickname")]
        public string NickName { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "address")]
        public string FirstAddress { get; set; }

        [Column(Name = "home")]
        public string FirstHomePhone { get; set; }

        [Column(Name = "mobile")]
        public string Mobile { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "homepage")]
        public string Homepage { get; set; }

        [Column(Name = "bday")]
        public int DayOfBirth { get; set; }

        public int MonthOfBirth { get; set; }

        [Column(Name = "bmonth")]
        public string monthOfBirth
        {
            get
            {
                return MonthsOfYear[monthOfBirth].ToString();
            }
            set
            {
                MonthOfBirth = MonthsOfYear[value];
            }
        }

        [Column(Name = "byear")]
        public string YearOfBirth { get; set; }

        [Column(Name = "aday")]
        public int DayOfAnniversary { get; set; }

        public int MonthOfAnniversary { get; set; }

        [Column(Name = "amonth")]
        public string monthOfAnniversary
        {
            get
            {
                return MonthsOfYear[monthOfAnniversary].ToString();
            }
            set
            {
                value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
                MonthOfAnniversary = MonthsOfYear[value];
            }
        }

        [Column(Name = "ayear")]
        public string YearOfAnniversary { get; set; }

        public int GroupOfContact { get; set; }

        [Column(Name = "address2")]
        public string SecondAddress { get; set; }

        [Column(Name = "phone2")]
        public string SecondHomePhone { get; set; }

        [Column(Name = "notes")]
        public string SecondNotes { get; set; }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }



        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }

        public List<GroupData> GetGroupRelationToRemove()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups from gcr in db.GCR.Where(p => p.ContactId == Id && p.GroupId == g.Id && g.Deprecated == "0000-00-00 00:00:00") select g).Distinct().ToList();
            }
        }

        //С ключем коллекции месяцев сопоставляется string monthOfBirth / string monthOfAnniversary и получаем int значение месяца, которое присваиваем в соотв.поле
        public static Dictionary<string, int> MonthsOfYear = new Dictionary<string, int>()
        {
            
            { "-", 0 },
            { "January", 1 },
            { "February", 2 },
            { "March", 3 },
            { "April", 4 },
            { "May", 5 },
            { "June", 6 },
            { "July", 7 },
            { "August", 8 },
            { "September", 9 },
            { "October", 10 },
            { "November", 11 },
            { "December", 12 }
        };


        

        private string fullName;
        public string FullName
        {

            get
            {
                if (fullName != null)
                {
                    return fullName;
                }
                else
                {
                    // склеиваем имя + отчество + фамилию, а также удаляет лишние разделители в начале и в конце строки
                    return (FirstName + MiddleName + LastName).Trim();
                }
            }

            set
            {
                fullName = value;
            }
        }

        private string allEmails;
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    // Склеивает значения email и удаляет лишние разделители в начале и в конце строки
                    return (EmailCleanUp(Email) + EmailCleanUp(Email2) + EmailCleanUp(Email3)).Trim();
                }
            }

            set
            {
                allEmails = value;
            }
        }

        /// <summary>
        /// Выполняет проверку наличия значения и возвращает его, добавляя отступ
        /// </summary>
        private string EmailCleanUp(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n";
        }

        private string allPhones;
        public string AllPhones
        {

            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    //склеивает значения номеров телефонов и очищает от символов, не отображающихся в таблице, а также удаляет лишние разделители в начале и в конце строки
                    return (CleanUp(FirstHomePhone) + CleanUp(Mobile) + CleanUp(WorkPhone) + CleanUp(SecondHomePhone)).Trim();
                }
            }

            set
            {
                allPhones = value;
            }
        }


        /// <summary>
        /// В полученном значении(телефон) преобразует пробел, тире и круглые скобки в пустую строку, а также добавляет отступ
        /// </summary>
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
            // phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "")
        }

        public ContactData()
        {

        }

        public ContactData(string text)
        {

        }

        
        public bool Equals(ContactData other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return FirstName == other.FirstName && LastName == other.LastName && Id == other.Id;
        }


        /// Метод сравнения Хэш-кодов имени контактов
        public override int GetHashCode()
        {
            return (FirstName + LastName).GetHashCode();
        }


        // Возвращает строковое представление объектов типа ContactData
        public override string ToString()
        {
            return
                "\n" + "firstName=" + FirstName + ", " +
                "\n" + "lastName=" + LastName + "\n" +
                "\n" + "middleName=" + MiddleName + "\n" +
                "\n" + "nickName=" + NickName + "\n" +
                "\n" + "title=" + Title + "\n" +
                "\n" + "companyName=" + Company + "\n" + 
                "\n" + "firstAddress=" + FirstAddress + "\n" + 
                "\n" + "firstHomePhone=" + FirstHomePhone + "\n" +
                "\n" + "mobilePhone=" + Mobile +"\n" +
                "\n" + "workPhone=" + WorkPhone +"\n" +
                "\n" + "fax=" + Fax +"\n" +
                "\n" + "email1=" + Email +"\n" +
                "\n" + "email2=" + Email2 +"\n" +
                "\n" + "email3=" + Email3 +"\n" +
                "\n" + "homePage=" + Homepage + "\n" +
                "\n" + "dayOfBirth=" + DayOfBirth + "\n" +
                "\n" + "monthOfBirth=" + MonthOfBirth + "\n" +
                "\n" + "yearOfBirth=" + YearOfBirth + "\n" +
                "\n" + "dayOfAnniversary=" + DayOfAnniversary + "\n" +
                "\n" + "monthOfAnniversary=" + MonthOfAnniversary + "\n" +
                "\n" + "yearOfAnniversary=" + YearOfAnniversary + "\n" +
                "\n" + "secondAddress=" + SecondAddress + "\n" +
                "\n" + "secondHomePhone=" + SecondHomePhone + "\n" +
                "\n" + "secondNotes=" + SecondNotes + "\n";                
        }


        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return FirstName.CompareTo(other.FirstName);
        }


        public static ContactData contactModel = new ContactData
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
        };
    }
}
