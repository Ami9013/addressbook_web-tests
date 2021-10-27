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
    /// Хранит в себе вспомогательные методы по логину и разлогину
    /// </summary>
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        /// <summary>
        /// Производит вход в систему с использованием переданных значений, а также выполняет проверку на логин и под какими кредами
        /// </summary>
        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            driver.FindElement(By.CssSelector("form#LoginForm input[name='user']")).Clear();
            driver.FindElement(By.CssSelector("form#LoginForm input[name='user']")).SendKeys(account.Username);
            driver.FindElement(By.CssSelector("form#LoginForm input[name='pass']")).Clear();
            driver.FindElement(By.CssSelector("form#LoginForm input[name='pass']")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("form#LoginForm input[type='submit']")).Click();
        }

        /// <summary>
        /// Выполняет разлог
        /// </summary>
        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector("form[name='logout'] a")).Click();
            }
        }

        /// <summary>
        /// Проверяет состояние: залогинены или нет
        /// </summary>
        public bool IsLoggedIn()
        {
            return IsElementExists(By.CssSelector("form[name='logout'] a"));
        }

        /// <summary>
        /// Проверяет под какими кредами (под кем) залогинены
        /// </summary>
        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && driver.FindElement(By.CssSelector("form[name='logout'] b")).Text == "(" + account.Username + ")";
        }
    }
}