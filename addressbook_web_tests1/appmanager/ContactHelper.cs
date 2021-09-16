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
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        /// <summary>
        /// Создает контакт
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для создания контакта. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public ContactHelper ContactCreate(ContactData contact)
        {
            manager.Navigator.GoToAddContactPage();
            FillContactForm(contact);
            FillDayOfBirth(7);
            FillMonthOfBirth(10);
            FillYearOfBirth(contact);
            FillDayOfAnniversary(7);
            FillMonthOfAnniversary(10);
            FillYearOfAnniversary(contact);
            SelectGroupForContact(1);
            FillSecondaryContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        /// <summary>
        /// Удаляет контакт
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для удаления контакта. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public ContactHelper Remove()
        {
            SelectContact(1);
            RemoveContact();
            ContactCloseAlert();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        /// <summary>
        /// Изменяет контакт
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для изменения контакта. Обращается к вспомогательным методам своего класса
        /// </summary>
        public ContactHelper Modify(ContactData contact)
        {
            ModifyContact(5);
            FillContactForm(contact);
            SubmitContactModify();
            ReturnToHomePageafterUpd();
            return this;
        }

        /// <summary>
        /// Заполняет форму создания/редактирования контакта переданными значениями
        /// </summary>
        public ContactHelper FillContactForm(ContactData contact)
        {
            driver.FindElement(By.CssSelector("div#content input[name='firstname']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='firstname']")).SendKeys(contact.FirstName);
            driver.FindElement(By.CssSelector("div#content input[name='lastname']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='lastname']")).SendKeys(contact.LastName);
            driver.FindElement(By.CssSelector("div#content input[name='nickname']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='nickname']")).SendKeys(contact.NickName);
            driver.FindElement(By.CssSelector("div#content input[name='middlename']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='middlename']")).SendKeys(contact.MiddleName);
            driver.FindElement(By.CssSelector("div#content input[name='title']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='title']")).SendKeys(contact.Title);
            driver.FindElement(By.CssSelector("div#content input[name='company']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='company']")).SendKeys(contact.Company);
            driver.FindElement(By.CssSelector("div#content textarea[name='address']")).Clear();
            driver.FindElement(By.CssSelector("div#content textarea[name='address']")).SendKeys(contact.FirstAddress);
            driver.FindElement(By.CssSelector("div#content input[name='home']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='home']")).SendKeys(contact.FirstHome);
            driver.FindElement(By.CssSelector("div#content input[name='mobile']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='mobile']")).SendKeys(contact.Mobile);
            driver.FindElement(By.CssSelector("div#content input[name='work']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='work']")).SendKeys(contact.Work);
            driver.FindElement(By.CssSelector("div#content input[name='fax']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='fax']")).SendKeys(contact.Fax);
            driver.FindElement(By.CssSelector("div#content input[name='email']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='email']")).SendKeys(contact.Email);
            driver.FindElement(By.CssSelector("div#content input[name='email2']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='email2']")).SendKeys(contact.Email2);
            driver.FindElement(By.CssSelector("div#content input[name='email3']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='email3']")).SendKeys(contact.Email3);
            driver.FindElement(By.CssSelector("div#content input[name='homepage']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='homepage']")).SendKeys(contact.Homepage);
            return this;
        }

        public ContactHelper FillSecondaryContactForm(ContactData contact)
        {
            driver.FindElement(By.CssSelector("div#content textarea[name='address2']")).Clear();
            driver.FindElement(By.CssSelector("div#content textarea[name='address2']")).SendKeys(contact.SecondAddress);
            driver.FindElement(By.CssSelector("div#content input[name='phone2']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='phone2']")).SendKeys(contact.SecondHome);
            driver.FindElement(By.CssSelector("div#content textarea[name='notes']")).Clear();
            driver.FindElement(By.CssSelector("div#content textarea[name='notes']")).SendKeys(contact.SecondNotes);
            return this;
        }

        // Заполняем дату, месяц и год рождения
        public ContactHelper FillDayOfBirth(int index)
        {
            driver.FindElement(By.CssSelector("div#content select[name='bday']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='bday']"))).SelectByIndex(index);
            return this;
        }

        public ContactHelper FillMonthOfBirth(int index)
        {
            driver.FindElement(By.CssSelector("div#content select[name='bmonth']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='bmonth']"))).SelectByIndex(index);
            return this;
        }

        public ContactHelper FillYearOfBirth(ContactData contact)
        {
            driver.FindElement(By.CssSelector("div#content input[name='byear']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='byear']")).SendKeys(contact.YearOfBirth);
            return this;
        }


        // Заполняем дату, месяц и год годовщины
        public ContactHelper FillDayOfAnniversary(int index)
        {
            driver.FindElement(By.CssSelector("div#content select[name='aday']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='aday']"))).SelectByIndex(index);
            return this;
        }

        public ContactHelper FillMonthOfAnniversary(int index)
        {
            driver.FindElement(By.CssSelector("div#content select[name='amonth']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='amonth']"))).SelectByIndex(index);
            return this;
        }

        public ContactHelper FillYearOfAnniversary(ContactData contact)
        {
            driver.FindElement(By.CssSelector("div#content input[name='ayear']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='ayear']")).SendKeys(contact.YearOfAnniversary);
            return this;
        }


        /// <summary>
        /// Выбирает группу для контакта
        /// </summary>
        public ContactHelper SelectGroupForContact(int index)
        {
            driver.FindElement(By.CssSelector("div#content select[name='new_group']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='new_group']"))).SelectByIndex(index);
            return this;
        }

        /// <summary>
        /// Сохраняет форму создания контакта 
        /// </summary>
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.CssSelector("form[name='theform'] input[name='submit']")).Click();
            return this;
        }

        /// <summary>
        /// Отмечает(выбирает) контакт
        /// </summary>
        public ContactHelper SelectContact(int index)
        {
            driver.FindElements(By.CssSelector("input[name='selected[]']"))[index-1].Click();
            return this;
        }

        /// <summary>
        /// Открывает форму редактирования контакта (без захода в Details)
        /// </summary>
        public ContactHelper ModifyContact(int index)
        {
            driver.FindElements(By.CssSelector("table[id=maintable] td:nth-child(8) a"))[index-1].Click();
            return this;
        }

        /// <summary>
        /// Сохраняет форму редактирования контакта
        /// </summary>
        public ContactHelper SubmitContactModify()
        {
            driver.FindElement(By.CssSelector("div#content input[name='update']")).Click();
            return this;
        }

        /// <summary>
        /// Удаляет отмеченный чек-боксом контакт
        /// </summary>
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.CssSelector("form[name = 'MainForm'] div:nth-child(8) input")).Click();
            return this;
        }

        /// <summary>
        /// Закрывает согласием всплывающее окно
        /// </summary>
        public ContactHelper ContactCloseAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        /// <summary>
        /// Возвращает на домашнюю страницу переходя по ссылке из сообщения после редактирования
        /// </summary>
        public ContactHelper ReturnToHomePageafterUpd()
        {
            driver.FindElement(By.CssSelector("div.msgbox a")).Click();
            return this;
        }
    }
}