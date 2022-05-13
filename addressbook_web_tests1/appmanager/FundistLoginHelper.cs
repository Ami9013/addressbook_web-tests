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
    public class FundistLoginHelper : HelperBase
    {
        public FundistLoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(FundistAccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn())
                {
                    
                    return;
                }
                Logout();
            }

            driver.FindElement(By.CssSelector("form[id='LoginForm'] input[id='Login']")).Clear();
            driver.FindElement(By.CssSelector("form[id='LoginForm'] input[id='Login']")).SendKeys(account.Username);
            driver.FindElement(By.CssSelector("form[id='LoginForm'] input[id='Password']")).Clear();
            driver.FindElement(By.CssSelector("form[id='LoginForm'] input[id='Password']")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("form[id='LoginForm'] button[id='LoginBtn']")).Click();
        }

        /// <summary>
        /// Проверяет состояние: залогинены или нет (по наличию кнопки Выход)
        /// </summary>
        public bool IsLoggedIn()
        {
            return IsElementExists(By.CssSelector("button[id='LogoutButton']"));
        }


        /// <summary>
        /// Выполняет разлог
        /// </summary>
        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector("button[id='LogoutButton']"));
            }
        }

        // Здесь я бы добавил проверку того, под кем мы залогинены
    }
}
