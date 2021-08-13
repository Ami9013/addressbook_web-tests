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
        public NavigationHelper(IWebDriver driver, string baseURL) : base(driver)
        {
            this.baseURL = baseURL;
        }

        /// <summary>
        /// Открывает стартовую страницу
        /// </summary>
        public void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        /// <summary>
        /// Выполняет переход на вкладку "Группы"
        /// </summary>
        public void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        /// <summary>
        /// Открывает страницу создания контакта
        /// </summary>
        public void GoToAddContactPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }

        /// <summary>
        /// Возвращает на домашнюю страницу
        /// </summary>
        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home"));
        }

    }
}
