﻿using System;
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
            ModifyContact(1);
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
            driver.FindElement(By.XPath("//div[@id='content']//input[@type='text' and @name='firstname']")).Clear();
            driver.FindElement(By.XPath("//div[@id='content']//input[@type='text' and @name='firstname']")).SendKeys(contact.FirstName);
            driver.FindElement(By.XPath("//div[@id='content']//input[@type='text' and @name='lastname']")).Clear();
            driver.FindElement(By.XPath("//div[@id='content']//input[@type='text' and @name='lastname']")).SendKeys(contact.LastName);
            driver.FindElement(By.XPath("//div[@id='content']//input[@type='text' and @name='nickname']")).Clear();
            driver.FindElement(By.XPath("//div[@id='content']//input[@type='text' and @name='nickname']")).SendKeys(contact.NickName);
            return this;
        }

        /// <summary>
        /// Сохраняет форму создания контакта 
        /// </summary>
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form[@name='theform']/input[@type='submit' and @name='submit' and @value='Enter']")).Click();
            return this;
        }

        /// <summary>
        /// Отмечает(выбирает) контакт
        /// </summary>
        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@type='checkbox' and @name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        /// <summary>
        /// Открывает форму редактирования контакта (без захода в Details)
        /// </summary>
        public ContactHelper ModifyContact(int index)
        {
            driver.FindElement(By.XPath("(//table[@id='maintable']//a[contains(@href,'edit.php')])[" + index + "]")).Click();
            return this;
        }

        /// <summary>
        /// Сохраняет форму редактирования контакта
        /// </summary>
        public ContactHelper SubmitContactModify()
        {
            driver.FindElement(By.XPath("//input[@type='submit' and @name='update']")).Click();
            return this;
        }

        /// <summary>
        /// Удаляет отмеченный чек-боксом контакт
        /// </summary>
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@type='button' and @value='Delete']")).Click();
            return this;
        }

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
            driver.FindElement(By.XPath("//div[@class='msgbox']//a[contains(@href,'index.php')]")).Click();
            return this;
        }
    }
}