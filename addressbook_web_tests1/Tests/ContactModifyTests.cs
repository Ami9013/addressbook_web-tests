using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModifyTests : TestBase
    {
        [Test]
        public void ContactModifyTest()
        {
            ContactData newContactData = new ContactData
            {
                FirstName = "UpdVanya",
                MiddleName = "UpdPetrovich",
                LastName = "UpdPetrov",
                NickName = "UpdNick",
                Title = "UpdAny",
                Company = "UpdMagazine",
                FirstAddress = "Upd Any city, any street",
                FirstHome = "Upd 111",
                Mobile = "123321123",
                Work = "Upd Main Cashier",
                Fax = "987654",
                Email = "Updvandamm0123@mail.no",
                Email2 = "Updvandamm0133@mail.no",
                Email3 = "Updvandamm0333@mail.no",
                Homepage = "Updn/a",
                DayOfBirth = 31,
                MonthOfBirth = (int)MonthesEnumClass.Monthes.July,
                YearOfBirth = "1900",
                DayOfAnniversary = 1,
                MonthOfAnniversary = (int)MonthesEnumClass.Monthes.December,
                YearOfAnniversary = "2222",
                SecondAddress = "Upd kolotushkina street",
                SecondHome = "1/101",
                SecondNotes = "upd done"
            };
            appManager.Contacts.Modify(2, newContactData);
            appManager.Auth.Logout();
        }
    }
}
