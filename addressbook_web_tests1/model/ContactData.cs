using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
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
        // иные поля, такие как Mobile, Fax не int т.к. в номере может быть "+", и в целом, любое поле сохраняет символы
        public ContactData()
        {

        }
    }
}
