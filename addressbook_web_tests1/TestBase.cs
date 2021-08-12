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
    public class TestBase
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        /// <summary>
        /// Открывает стартовую страницу
        /// </summary>
        protected void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        /// <summary>
        /// Производит вход в систему с использованием переданных значений
        /// </summary>
        protected void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        /// <summary>
        /// Выполняет переход на вкладку "Группы"
        /// </summary>
        protected void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        /// <summary>
        /// Открывает форму создания группы
        /// </summary>
        protected void InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }

        /// <summary>
        /// Заполняет форму создания группы переданными значениями
        /// </summary>
        protected void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Click();
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }

        /// <summary>
        /// Сохраняет форму создания группы
        /// </summary>
        protected void SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        /// <summary>
        /// Возвращает на вкладку "Группы"
        /// </summary>
        protected void ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }

        /// <summary>
        /// Отмечает(выбирает) группу
        /// </summary>
        protected void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
        }

        /// <summary>
        /// Удаляет выбранную группу
        /// </summary>
        protected void RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }

        /// <summary>
        /// Открывает страницу создания контакта
        /// </summary>
        protected void GoToAddContactPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }

        /// <summary>
        /// Заполняет форму создания контакта переданными значениями
        /// </summary>
        protected void FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.FirstName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
        }

        /// <summary>
        /// Сохраняет форму создания контакта 
        /// </summary>
        protected void SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        /// <summary>
        /// Возвращает на домашнюю страницу
        /// </summary>
        protected void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home"));
        }





















    }
}
