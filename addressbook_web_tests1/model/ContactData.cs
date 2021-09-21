using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string NickName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string Title { get; set; } = "";
        public string Company { get; set; } = "";
        public string FirstAddress { get; set; } = "";
        public string FirstHome { get; set; } = "";
        public string Mobile { get; set; } = "";
        public string Work { get; set; } = "";
        public string Fax { get; set; } = "";
        public string Email { get; set; } = "";
        public string Email2 { get; set; } = "";
        public string Email3 { get; set; } = "";
        public string Homepage { get; set; } = "";
        public string YearOfBirth { get; set; } = "";
        public string YearOfAnniversary { get; set; } = "";

        //Вторичные данные
        public string SecondAddress { get; set; } = "";
        public string SecondHome { get; set; } = "";
        public string SecondNotes { get; set; } = "";

        public ContactData()
        {

        }
    }
}
