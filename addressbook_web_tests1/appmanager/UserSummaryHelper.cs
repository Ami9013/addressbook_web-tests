using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace WebAddressbookTests
{
    public class UserSummaryHelper : HelperBase
    {
        public UserSummaryHelper(ApplicationManager manager) : base(manager)
        {
        }

        /// <summary>
        /// Получает информацию(дату, сумму, комментарий) о всех успешных платежах пользователя
        /// </summary>
        /// <param name="list">Лист, в который будут записаны(добавлены) данные</param>
        /// <returns>Добавляет записи в лист</returns>
        public List<UserInCardDeposits> GetSuccessDepositsInfo(List<UserInCardDeposits> list) //здесь я бы добавил скролл до элемента(блока), а если записей много, то скролл еще и в цикле
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            //проверять циклом наличие (колво) элементов
            Thread.Sleep(5000);
            driver.FindElement(By.CssSelector("div[id='HeadingSucceeded']")).Click();//нет проверки развернут список или нет

            ICollection<IWebElement> depositRecords = driver.FindElements(By.CssSelector("tr[id^='lastDepositsSucceeded']"));
            foreach (IWebElement record in depositRecords)
            {
                UserInCardDeposits onlyDepoInfo = new UserInCardDeposits();
                Thread.Sleep(2000);
                var commentParse = record.FindElement(By.CssSelector("td[name='col-Note']")).Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                var amountParse = record.FindElement(By.CssSelector("td[name='col-Amount']")).Text.Split(new[] { ' ' });
                onlyDepoInfo.SuccessDepoInCardDate = record.FindElement(By.CssSelector("td[name='col-Date']")).Text;
                onlyDepoInfo.SuccessDepoInCardComment = commentParse[1];
                onlyDepoInfo.SuccessDepoInCardAmount = Math.Round(Double.Parse(amountParse[0], provider), 2);

                list.Add(onlyDepoInfo);
            }
            return list;
        }

        /// <summary>
        /// Получает информацию(дату, сумму, комментарий) о всех ручных пополнениях пользователя
        /// </summary>
        /// <param name="list">Лист, в который будут записаны(добавлены) данные</param>
        /// <returns>Добавляет записи в лист</returns>
        public List<UserInCardDeposits> GetManualDeposits(List<UserInCardDeposits> list) //здесь я бы добавил скролл до элемента(блока), а если записей много, то скролл еще и в цикле
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            driver.FindElement(By.CssSelector("div[id='HeadingManual']")).Click();

            ICollection<IWebElement> manualDepoRecords = driver.FindElements(By.CssSelector("div[id='lastDepositsManual'] tr[id^='lastDepositsManual']"));
            foreach (IWebElement record in manualDepoRecords)
            {
                UserInCardDeposits onlyManualDepoInfo = new UserInCardDeposits();
                Thread.Sleep(2000);
                var commentParse = record.FindElement(By.CssSelector("td[name='col_Note']")).Text;
                var amountParse = record.FindElement(By.CssSelector("td[name='col_Amount']")).Text.Split(new[] { ' ' });
                onlyManualDepoInfo.ManualDepoInCardDate = record.FindElement(By.CssSelector("td[name='col_Date']")).Text;
                onlyManualDepoInfo.ManualDepoInCardComment = commentParse;
                onlyManualDepoInfo.ManualDepoInCardAmount = Math.Round(Double.Parse(amountParse[0], provider), 2);

                list.Add(onlyManualDepoInfo);
            }
            return list;
        }

        /// <summary>
        /// Переходит в отчёт по счету из карточки пользователя
        /// </summary>
        public void GoToReportFromSummary()
        {
            //Actions act = new Actions(driver);
            //var element = driver.FindElement(By.CssSelector("div[id='page-wrapper']"));
            //act.SendKeys(element, Keys.Home).Build().Perform();
            //new WebDriverWait(driver, TimeSpan.FromSeconds(4)).Until(d => driver.FindElements(By.CssSelector("a[id='ReportsUser20Button']")).Count > 0);
            var el = driver.FindElement(By.CssSelector("a[id='ReportsUser20Button']"));
            el.SendKeys(Keys.Home);
            el.Click();
        }
    }
}
