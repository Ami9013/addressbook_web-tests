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
        /// Производит вход в систему с использованием переданных значений
        /// </summary>
        public void Login(AccountData account)
        {
            driver.FindElement(By.CssSelector("form#LoginForm input[name='user']")).Clear();
            driver.FindElement(By.CssSelector("form#LoginForm input[name='user']")).SendKeys(account.Username);
            driver.FindElement(By.CssSelector("form#LoginForm input[name='pass']")).Clear();
            driver.FindElement(By.CssSelector("form#LoginForm input[name='pass']")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("form#LoginForm input[type='submit']")).Click();
        }

        public void Logout()
        {
            driver.FindElement(By.CssSelector("form[name='logout'] a")).Click();
        }
    }
}