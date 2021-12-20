using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string FirstHome { get; set; }
        public string Mobile { get; set; }
        public string Work { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; } // int, т.к. выбираю месяц в селекте по индексу, который получаю от выбранного в Enum значения, приведенного в int
        public string YearOfBirth { get; set; }
        public int DayOfAnniversary { get; set; }
        public int MonthOfAnniversary { get; set; } // также как и в MonthOfBirth
        public string YearOfAnniversary { get; set; }
        public int GroupOfContact { get; set; } 
        public string SecondAddress { get; set; }
        public string SecondHome { get; set; }
        public string SecondNotes { get; set; }
        public string FullName { get; set; }
        public string Id { get; set; }


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
            return FirstName == other.FirstName & LastName == other.LastName;
            // Или просто return FirstName == other.FirstName; ??
        }

        /// Метод сравнения Хэш-кодов имени контактов
        public override int GetHashCode()
        {
            return FirstName.GetHashCode() & LastName.GetHashCode();
            // Или просто return FirstName.GetHashCode(); ??
        }



        // Возвращает строковое представление объектов типа GroupData
        public override string ToString()
        {
            return 
                "\n" + "firstName=" + FirstName + ", " +
                "\n" + "lastName=" + LastName +  "\n";
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return FirstName.CompareTo(other.FirstName) & LastName.CompareTo(other.LastName);
            // или просто return FirstName.CompareTo(other.FirstName); ??
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
            FirstHome = "111",
            Mobile = "88005553535",
            Work = "Main Cashier",
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
            SecondHome = "101/1",
            SecondNotes = "done"
        };
    }
}
