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
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string MiddleName { get; set; }
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
        public string YearOfBirth { get; set; } = "1971";
        public string YearOfAnniversary { get; set; } = "1996";

        //Вторичные данные
        public string SecondAddress { get; set; } = "Voronezh city, Moscovskiy pr-kt";
        public string SecondHome { get; set; } = "N/A";
        public string SecondNotes { get; set; } = "Lorem Ipsum";



        public ContactData(
            string firstName, 
            string lastName, 
            string nickName, 
            string middleName, 
            string title, 
            string companyName, 
            string firstAddress, 
            string firstHome, 
            string mobile, 
            string work, 
            string fax, 
            string email, 
            string email2,
            string email3, 
            string homepage
            )
        {
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            MiddleName = middleName;
            Title = title;
            Company = companyName;
            FirstAddress = firstAddress;
            FirstHome = firstHome;
            Mobile = mobile;
            Work = work;
            Fax = fax;
            Email = email;
            Email2 = email2;
            Email3 = email3;
            Homepage = homepage;
        }
    }
}
