using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    /// <summary>
    /// Хранит в себе вспомогательные методы по работе с навигационной панелью
    /// </summary>
    public class NavigationHelper : HelperBase
    {
        private string baseURL;
        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        /// <summary>
        /// Открывает стартовую страницу
        /// </summary>
        public void GoToHomePage()
        {
            if (driver.Url == baseURL + "/addressbook/")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        /// <summary>
        /// Выполняет переход на вкладку "Группы"
        /// </summary>
        public void GoToGroupsPage()
        {
            if (driver.Url == baseURL + "/addressbook/group.php" && IsElementExists(By.CssSelector("input[name='new']")))
            {
                return;
            }
            driver.FindElement(By.CssSelector("div#nav li.admin a")).Click();
        }

        /// <summary>
        /// Открывает страницу создания контакта
        /// </summary>
        public void GoToAddContactPage()
        {
            if (driver.Url == baseURL + "/addressbook/edit.php")
            {
                return;
            }
            driver.FindElement(By.CssSelector("div#nav li.all a")).Click();
        }

        /// <summary>
        /// Возвращает на домашнюю страницу
        /// </summary>
        public void ReturnToHomePage()
        {
            if (driver.Url == baseURL + "/addressbook/")
            {
                return;
            }
            driver.FindElement(By.CssSelector("div#nav li a")).Click();
        }
    }
}
