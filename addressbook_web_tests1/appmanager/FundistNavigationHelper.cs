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
    public class FundistNavigationHelper : HelperBase
    {
        private string testFundistUrl;

        public FundistNavigationHelper(ApplicationManager manager, string testFundistUrl) : base(manager)
        {
            this.testFundistUrl = testFundistUrl;
        }

        /// <summary>
        /// Открывает стартовую страницу логина или Админ панель, если роль уже залогинена
        /// </summary>
        public void GoToMainPage()
        {
            if (driver.Url == testFundistUrl)
            {
                return;
            }
            driver.Navigate().GoToUrl(testFundistUrl);
        }

        
        /// <summary>
        /// Переходит в отчёт "Отчёты" -> "Игровая активность"
        /// </summary>
        public void GoToGameReport()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(4)).Until(d => driver.FindElements(By.CssSelector("div[id='sidemenu-scrollable-wrapper'] a[id='SideMenu-Reports']")).Count > 0);
            driver.FindElement(By.CssSelector("div[id='sidemenu-scrollable-wrapper'] a[id='SideMenu-Reports']")).Click();// обработка по внутренним в меню элементам(видимость)

            //new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(d => driver.FindElements(By.CssSelector("ul[id='SideMenu-Reports-second-level'] a[id='SideMenu-Reports-GameActivity']")).Count > 0);
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("ul[id='SideMenu-Reports-second-level'] a[id='SideMenu-Reports-GameActivity']")).Click();
        }
    }
}
