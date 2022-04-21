using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace WebAddressbookTests
{
    public class ContactTestBase : AuthTestBase
    {
        /// <summary>
        /// Сравнение списка, полученного через UI со списком, полученным через базу данных
        /// </summary>
        [TearDown]
        public void CompareContactsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> fromUIList = appManager.Contacts.GetContactList();

                List<ContactData> fromDB = ContactData.GetAll();
                fromUIList.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUIList, fromDB);
            }

            appManager.Auth.Logout();
        }      
    }
}
