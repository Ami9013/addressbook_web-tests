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
    /// Хранит в себе вспомогательные методы по работе с контактами
    /// </summary>
    public class ContactHelper : HelperBase
    {
        public ContactHelper(IWebDriver driver) : base(driver)
        {
        }


        /// <summary>
        /// Заполняет форму создания/редактирования контакта переданными значениями
        /// </summary>
        public void FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.FirstName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            driver.FindElement(By.Name("nickname")).Click();
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys(contact.NickName);
        }

        /// <summary>
        /// Сохраняет форму создания контакта 
        /// </summary>
        public void SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        /// <summary>
        /// Отмечает(выбирает) контакт
        /// </summary>
        public void SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
        }

        /// <summary>
        /// Открывает форму редактирования контакта (без захода в Details)
        /// </summary>
        public void ModifyContact(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
        }

        /// <summary>
        /// Сохраняет форму редактирования контакта
        /// </summary>
        public void SubmitContactModify()
        {
            driver.FindElement(By.Name("update")).Click();
        }

        /// <summary>
        /// Удаляет отмеченный чек-боксом контакт
        /// </summary>
        public void RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
        }

        public void ContactCloseAlert()
        {
            driver.SwitchTo().Alert().Accept();
        }

        /// <summary>
        /// Возвращает на домашнюю страницу переходя по ссылке из сообщения после редактирования
        /// </summary>
        public void ReturnToHomePageafterUpd()
        {
            driver.FindElement(By.XPath("//a[contains(text(),'home page')]")).Click();
        }

    }
}
