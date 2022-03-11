using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string FirstAddress { get; set; }
        public string FirstHomePhone { get; set; }
        public string Mobile { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public string YearOfBirth { get; set; }
        public int DayOfAnniversary { get; set; }
        public int MonthOfAnniversary { get; set; }
        public string YearOfAnniversary { get; set; }
        public int GroupOfContact { get; set; } 
        public string SecondAddress { get; set; }
        public string SecondHomePhone { get; set; }
        public string SecondNotes { get; set; }
        public string Id { get; set; }

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
            return FirstName == other.FirstName && LastName == other.LastName;
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
            FirstName = "iAmNewVanya",
            MiddleName = "iAmNewPetrovich",
            LastName = "iAmNewPetrov",
            NickName = "Nick",
            Title = "Any",
            Company = "Magazine",
            FirstAddress = "Any city, any street",
            FirstHomePhone = "+111",
            Mobile = "+7(800)5553535",
            WorkPhone = "+7(900)",
            Fax = "123321",
            Email = "vandamm0123@mail.no",
            Email2 = "vandamm0133@mail.no",
            Email3 = "vandamm0333@mail.no",
            Homepage = "n/a",
            DayOfBirth = 2,
            MonthOfBirth = 4,
            YearOfBirth = "1971",
            DayOfAnniversary = 2,
            MonthOfAnniversary = 9,
            YearOfAnniversary = "1996",
            GroupOfContact = 2,
            SecondAddress = "kolotushkina street",
            SecondHomePhone = "+(7800)",
            SecondNotes = "done"
        };
    }
}
