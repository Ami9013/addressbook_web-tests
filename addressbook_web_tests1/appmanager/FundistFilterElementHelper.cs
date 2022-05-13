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
    public class FundistFilterElementHelper : HelperBase //в целом по хелперу часть методов на мой взгляд стоило вынести в отдельные (какие-то только для фильтр форм, какие-то для таблиц)
    {
        public FundistFilterElementHelper(ApplicationManager manager) : base(manager)
        {
        }

        public static Random rnd = new Random();
        

        /// <summary>
        /// Переходим во вкладку "Расширенный отчет" отчета "Игровая активность" > "Игры"
        /// </summary>
        public void ClickOnExtendedTab()
        {
            driver.FindElement(By.CssSelector("ul[id='report-nav'] li[id='linkExtendedReport']")).Click();
        }

        
        /// <summary>
        /// Определяем какой месяц является предыдущим и выбираем его в селекторе(дропдауне)
        /// </summary>
        public void SetMonthInDropDown()
        {
            string preMonth = ($"{DateTime.Now.Year}-{(DateTime.Now.Month - 1)}").ToString();//тут я отнимаю от инта, поэтому если месяц январь - получу 0

            //var n = DateTime.Now; // пример приведения даты в нужный формат без форматирования строк
            //var m = n.AddMonths(-1).ToString("YYYY-M");

            driver.FindElement(By.CssSelector("span[id='select2-Month-container']")).Click();
            driver.FindElement(By.CssSelector($"li[id$='-{preMonth}']")).Click();        
        }

        /// <summary>
        /// Получаем список мерчей по которым были ставки и если их нет - завершает тест, в противном случае выбираем рандомный из списка и переходим(кликаем) к списку его игр, по которым были ставки
        /// </summary>
        public void SetRandomProvider()//название Гет или подобное, т.к. Set не отражает суть метода
        {            
            var js = (IJavaScriptExecutor)driver;
            Actions act = new Actions(driver);
            int merchantCount = driver.FindElements(By.CssSelector("table[id='extended-table'] tbody tr[id^='game_']")).Count;
            if (merchantCount == 0)
            {
                manager.FundistAuth.Logout();
                driver.Quit();
            }
            
            List<string> merchantNames = new List<string>();
            ICollection<IWebElement> merchants = driver.FindElements(By.CssSelector("table[id='extended-table'] tbody tr[id^='game_']"));
            foreach (IWebElement merch in merchants)
            {
                int bets = Convert.ToInt32(merch.FindElement(By.CssSelector("td[name='col-BetsCount']")).Text);
                if (bets > 1)
                {
                    merchantNames.Add(merch.FindElement(By.CssSelector("td[name='col-Merchant']")).Text);
                }
            }

            int getRandomMerchName = rnd.Next(0, merchantNames.Count);//удаление из списка если 0 у мерча
            var elem = driver.FindElement(By.LinkText(merchantNames[getRandomMerchName]));

            js.ExecuteScript("arguments[0].scrollIntoView(true);", elem);                 //работает и без скролла (и без ожидаения?)
            //act.SendKeys(elem, Keys.Up + Keys.Up).Build().Perform();

            var element = driver.FindElement(By.CssSelector("table[id='extended-table']"));
            act.SendKeys(element, Keys.Up + Keys.Up).Build().Perform();
            Thread.Sleep(2000);
            elem.Click();
            //new WebDriverWait(driver, TimeSpan.FromSeconds(4)).Until(d => driver.FindElements(By.LinkText(merchantNames[getRandomMerchName])).Count > 0);
            //driver.FindElement(By.LinkText(merchantNames[getRandomMerchName])).Click();
        }

        /// <summary>
        /// Получаем список игр по которым были ставки и если их нет переходим в WHILE и возвращаемся на этап рандомного выбора провайдера.
        /// Если ставки по играм есть - выбираем рандомную из списка игру и переходим к списку пользователей, которые делали в ней ставки
        /// </summary>
        public void SetRandomGame()
        {
            Actions act = new Actions(driver);
            var element = driver.FindElement(By.CssSelector("table[id='extended-table']"));
            var js = (IJavaScriptExecutor)driver;
            List<string> gameNames = new List<string>();
            ICollection<IWebElement> games = driver.FindElements(By.CssSelector("table[id='extended-table'] tbody tr[id^='game_']"));
            foreach (IWebElement game in games) //Цикл здесь и в методе ниже я бы вынес в отдельный метод с передачей флага (первая итерация или нет) чтобы исключить дублирование, но не было времени обдумать и реализовать это
            {
                int bets = Convert.ToInt32(game.FindElement(By.CssSelector("td[name='col-BetsCount']")).Text);
                if (bets > 2)
                {
                    gameNames.Add(game.FindElement(By.CssSelector("td[name='col-Merchant']")).Text);
                }
            }

            while (gameNames.Count == 0) //вероятность бесконечного цикла
            {
                driver.Navigate().Back();
                SetRandomProvider();
                foreach (IWebElement game in games)
                {
                    int bets = Convert.ToInt32(game.FindElement(By.CssSelector("td[name='col-BetsCount']")).Text);
                    if (bets > 2)
                    {
                        gameNames.Add(game.FindElement(By.CssSelector("td[name='col-Merchant']")).Text);
                    }
                }
            }

            int getRandomGameName = rnd.Next(0, gameNames.Count);
            var elem = driver.FindElement(By.LinkText(gameNames[getRandomGameName]));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", elem);
            act.SendKeys(element, Keys.Up + Keys.Up).Build().Perform();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText(gameNames[getRandomGameName])).Click();         
        }

        /// <summary>
        /// Кнопка [Найти]
        /// </summary>
        public void FindButton()
        {
            driver.FindElement(By.CssSelector("button[id='ButtonFilter']")).Click();
        }

                

        private FundistUserData userDataCache = null;
        /// <summary>
        /// Выбираем юзеру, у которого были ставки, если ни у одного из пользователей не было ставок, возвращаемся через цикл к рандомному выбору игры
        /// Если пользователь, у которого есть ставки, определен - записываем данные в экземпляр
        /// </summary>
        /// <returns>Возвращает заполненный экземпляр, содержащий данные о ставках, победах, Id юзера и название игры</returns>
        public FundistUserData GetUserWithBets()
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            if (userDataCache == null)
            {
                userDataCache = new FundistUserData();

                ICollection<IWebElement> users = driver.FindElements(By.CssSelector("table[id='player-DataList'] tr[id^='player']"));
                foreach (IWebElement user in users)
                {
                    FundistUserData userData = new FundistUserData();
                    int userBets = Convert.ToInt32(user.FindElement(By.CssSelector("td[name='col-BetsCount']")).Text);
                    if (userBets > 1)
                    {
                        userData.Id = user.FindElement(By.CssSelector("td[name='col-UserID']")).Text;
                        userData.DateOfBet = user.FindElement(By.CssSelector("td[name='col-Date']")).Text;
                        userData.CountOfBet = Convert.ToInt32(user.FindElement(By.CssSelector("td[name='col-BetsCount']")).Text);
                        userData.AmountOfBet = Math.Round(Double.Parse((user.FindElement(By.CssSelector("td[name='col-BetsAmount']")).Text), provider), 2);
                        //userData.AmountOfBet = Math.Round(Double.Parse((user.FindElement(By.CssSelector("td[name='col-BetsAmount']")).Text), provider), 2);
                        userData.CountOfWin = Convert.ToInt32(user.FindElement(By.CssSelector("td[name='col-WinsCount']")).Text);
                        userData.AmountOfWin = Math.Round(Double.Parse((user.FindElement(By.CssSelector("td[name='col-WinsAmount']")).Text), provider), 2);
                        userData.GameOfUser = user.FindElement(By.CssSelector("td[name='col-Game']")).Text;

                        userDataCache = userData;
                        break;
                    }
                }

                while (userDataCache == null)
                {
                    SetRandomGame();
                    foreach (IWebElement user in users)
                    {
                        FundistUserData userData = new FundistUserData();
                        int userBets = Convert.ToInt32(user.FindElement(By.CssSelector("td[name='col-BetsCount']")).Text);
                        if (userBets > 1)
                        {
                            userData.Id = user.FindElement(By.CssSelector("td[name='col-UserID']")).Text;
                            userData.DateOfBet = user.FindElement(By.CssSelector("td[name='col-Date']")).Text;
                            userData.CountOfBet = Convert.ToInt32(user.FindElement(By.CssSelector("td[name='col-BetsCount']")).Text);
                            userData.AmountOfBet = Math.Round(Double.Parse((user.FindElement(By.CssSelector("td[name='col-BetsAmount']")).Text), provider), 2);
                            userData.AmountOfBet = Math.Round(Double.Parse((user.FindElement(By.CssSelector("td[name='col-BetsAmount']")).Text), provider), 2);
                            userData.CountOfWin = Convert.ToInt32(user.FindElement(By.CssSelector("td[name='col-WinsCount']")).Text);
                            userData.AmountOfWin = Math.Round(Double.Parse((user.FindElement(By.CssSelector("td[name='col-WinsAmount']")).Text), provider), 2);
                            userData.GameOfUser = user.FindElement(By.CssSelector("td[name='col-Game']")).Text;

                            userDataCache = userData;
                            break;
                        }
                    }
                }
            }
            return userDataCache;            
        }


        /// <summary>
        /// По переданному id пользователя переходим в его карточку
        /// </summary>
        /// <param name="userId"></param>
        public void GoToUserCard(string userId)
        {
            Actions act = new Actions(driver);
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(d => driver.FindElements(By.CssSelector("table[id='player-DataList']")).Count > 0);
            var element = driver.FindElement(By.CssSelector("table[id='player-DataList']"));
            var js = (IJavaScriptExecutor)driver;

            new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(d => driver.FindElements(By.LinkText(userId)).Count > 0);
            var elem = driver.FindElement(By.LinkText(userId));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", elem);
            act.SendKeys(element, Keys.Up + Keys.Up).Build().Perform();
            driver.FindElement(By.LinkText(userId)).Click();
        }

        /// <summary>
        /// Получает данные о ставках и выигрышах пользователя из отчёта по счету
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Возвращает заполненный инкрементированными данными из отчёта экземпляр</returns>
        public FundistUserData GetBetsOfStatementReport(FundistUserData user)
        {
            var dateOfBet = user.DateOfBet.Split(new[] {'-'});
            string dateFrom = ($"{dateOfBet[2]}.{dateOfBet[1]}.{dateOfBet[0]}");

            driver.FindElement(By.CssSelector("input[id='dateFrom']")).Clear();
            driver.FindElement(By.CssSelector("input[id='dateFrom']")).SendKeys($"{dateFrom} 00:00");
            driver.FindElement(By.CssSelector("input[id='dateTo']")).Clear();
            driver.FindElement(By.CssSelector("input[id='dateTo']")).SendKeys($"{dateFrom} 23:59");
            driver.FindElement(By.CssSelector("button[id='ButtonFilter']")).Click();

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            FundistUserData userDataOfStatements = new FundistUserData
            {
                AmountOfBet = 0,
                AmountOfWin = 0,
                CountOfBet = 0,
                CountOfWin = 0
            };

            Actions act = new Actions(driver);
            var js = (IJavaScriptExecutor)driver;
            ICollection<IWebElement> allPages = driver.FindElements(By.CssSelector("ul[id='InspiniaPaginator'] li[numpage]"));
            for (int i = 0; i < allPages.Count; i++)  // циклом for я обрабатываю пагинатор (макс 5 страниц). В планах было обрабатывать страницы, которые не видно(6 и далее), добавляя страницы в allPages, либо добавив сразу все элементы li[numpage] в allPages, потом перебирать их
            {
                var elementS = driver.FindElement(By.CssSelector("ul[id='InspiniaPaginator'] li a"));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", elementS);

                driver.FindElements(By.CssSelector("ul[id='InspiniaPaginator'] li a"))[i + 1].Click();
                ICollection<IWebElement> allRecord = driver.FindElements(By.CssSelector("table[id='DataList'] tr[id^='transactions_']"));
                foreach (IWebElement record in allRecord)
                {
                    if (record.FindElement(By.CssSelector("td[name='col-Note']")).Text.Contains(user.GameOfUser)
                        && record.FindElement(By.CssSelector("td[name='col-Amount']")).Text.Contains("-"))
                    {
                        var betsAmountParse = record.FindElement(By.CssSelector("td[name='col-Amount']")).Text.Split(new[] { '-', ' ' });

                        if (betsAmountParse.Length > 2)
                        {
                            string str = string.Join(" ", betsAmountParse);
                            userDataOfStatements.AmountOfBet = Math.Round(userDataOfStatements.AmountOfBet + Double.Parse(str.Substring(0, str.LastIndexOf(" ")).Replace(" ", "").Replace("-", ""), provider), 2);
                            userDataOfStatements.CountOfBet++;
                        }
                        else
                        {
                            userDataOfStatements.AmountOfBet = Math.Round(userDataOfStatements.AmountOfBet + Double.Parse(betsAmountParse[1], provider), 2);
                            userDataOfStatements.CountOfBet++;
                        }
                    }

                    if (record.FindElement(By.CssSelector("td[name='col-Note']")).Text.Contains(user.GameOfUser)
                        && (record.FindElement(By.CssSelector("td[name='col-Amount']")).Text.Contains("-")) == false)
                    {
                        var winAmountParse = record.FindElement(By.CssSelector("td[name='col-Amount']")).Text.Split(' ');
                        if (winAmountParse.Length > 2)
                        {
                            string str = string.Join(" ", winAmountParse);
                            userDataOfStatements.AmountOfWin += Math.Round(Double.Parse(str.Substring(0, str.LastIndexOf(" ")).Replace(" ", ""), provider), 2);
                            userDataOfStatements.CountOfWin++;
                        }
                        else
                        {
                            userDataOfStatements.AmountOfWin += (Math.Round(Double.Parse(winAmountParse[0], provider), 2));
                            userDataOfStatements.CountOfWin++;
                        }
                    }
                }
            }
            return userDataOfStatements;  
        }


        /// <summary>
        /// Получает и сверяет данные об успешных и ручных пополнениях пользователя.
        /// </summary>
        /// <param name="depoList">Список, сформированный из Успешных платежей и Ручных пополнений пользователя</param>
        /// <param name="user"></param>
        /// <returns>Возвращает список строк с сопутствующими комментариями о том что различается и какой параметр. Если различий нет - возвращает соотв. запись</returns>
        public List<string> GetUserDepositsFromStatement(List<UserInCardDeposits> depoList, FundistUserData user)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            List<string> diffDataList = new List<string>();

            for (int i = 0; i < depoList.Count; i++)
            {
                if (depoList[i].SuccessDepoInCardDate != null) 
                {
                    var dateOfEvent = depoList[i].SuccessDepoInCardDate.Split(' ');
                    string validTime = dateOfEvent[0].Substring(0, dateOfEvent[0].LastIndexOf(":"));                //получили корректное время без секунд
                    var validDate = dateOfEvent[1].Split('.');                                                      //получили отдельно день, месяц и год
                    string dateFrom = ($"{validDate[0]}.{validDate[1]}.20{validDate[2]}");                          //составили корректную для отчёта дату


                    driver.FindElement(By.CssSelector("input[id='dateFrom']")).Clear();  //эти шаги с заполнением датой и временем фильтр формы я бы вынес в отдельный метод с передачей в него даты и времени
                    driver.FindElement(By.CssSelector("input[id='dateFrom']")).SendKeys($"{dateFrom} {validTime}");
                    driver.FindElement(By.CssSelector("input[id='dateTo']")).Clear();
                    driver.FindElement(By.CssSelector("input[id='dateTo']")).SendKeys($"{dateFrom} {Convert.ToDateTime(validTime).AddMinutes(1).ToShortTimeString()}");
                    driver.FindElement(By.CssSelector("button[id='ButtonFilter']")).Click();

                    string dateForCheck = driver.FindElement(By.CssSelector("tr[id^=transactions] td[name='col-Date']")).Text.Split(' ')[0];
                    if (dateFrom != dateForCheck)
                    {
                        diffDataList.Add($"Дата успешного платежа из отчёта по счету: {dateForCheck} и она отличается от полученной в карточке пользователя");
                    }

                    string noteForCheck = driver.FindElement(By.CssSelector("tr[id^=transactions] td[name='col-Note']")).Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)[1];
                    if (depoList[i].SuccessDepoInCardComment != noteForCheck)
                    {
                        diffDataList.Add($"Комментарий к успешному платежу из отчёта по счету: {noteForCheck} и он отличается от полученного в карточке пользователя");
                    }

                    var depoAmountParse = driver.FindElement(By.CssSelector("tr[id^=transactions] td[name='col-Amount']")).Text.Split(' ');
                    double amountForCheck;
                    if (depoAmountParse.Length > 2)
                    {
                        string str = string.Join(" ", depoAmountParse);
                        amountForCheck = Math.Round(Double.Parse(str.Substring(0, str.LastIndexOf(" ")).Replace(" ", ""), provider), 2);
                    }
                    else
                    {
                        amountForCheck = (Math.Round(Double.Parse(depoAmountParse[0], provider), 2));
                    }
                    
                    if (depoList[i].SuccessDepoInCardAmount != amountForCheck)
                    {
                        diffDataList.Add($"Сумма успешного платежа из отчёта по счету: {amountForCheck} и она отличается от полученной в карточке пользователя");
                    }
                }
                else
                {
                    var dateOfEvent = depoList[i].ManualDepoInCardDate.Split(' ');
                    string validTime = dateOfEvent[0].Substring(0, dateOfEvent[0].LastIndexOf(":"));                 
                    var validDate = dateOfEvent[1].Split('.');                                      
                    string dateFrom = ($"{validDate[0]}.{validDate[1]}.20{validDate[2]}");

                    driver.FindElement(By.CssSelector("button[id='ButtonReset']")).Click();
                    driver.FindElement(By.CssSelector("input[id='dateFrom']")).Clear();
                    driver.FindElement(By.CssSelector("input[id='IDUser']")).Clear();
                    driver.FindElement(By.CssSelector("input[id='IDUser']")).SendKeys(user.Id);
                    driver.FindElement(By.CssSelector("input[id='dateFrom']")).SendKeys($"{dateFrom} {validTime}");
                    driver.FindElement(By.CssSelector("input[id='dateTo']")).Clear();
                    driver.FindElement(By.CssSelector("input[id='dateTo']")).SendKeys($"{dateFrom} {Convert.ToDateTime(validTime).AddMinutes(1).ToShortTimeString()}");
                    driver.FindElement(By.CssSelector("button[id='ButtonFilter']")).Click();

                    string dateForCheck = driver.FindElement(By.CssSelector("tr[id^=transactions] td[name='col-Date']")).Text.Split(' ')[0];
                    if (dateFrom != dateForCheck)
                    {
                        diffDataList.Add($"Дата ручного пополнения из отчёта по счету: {dateForCheck} и она отличается от полученной в карточке пользователя");
                    }

                    string noteForCheck = driver.FindElement(By.CssSelector("tr[id^=transactions] td[name='col-Note']")).Text.Trim();
                    if (depoList[i].ManualDepoInCardComment != noteForCheck)
                    {
                        diffDataList.Add($"Комментарий к ручному пополнению из отчёта по счету: {noteForCheck} и он отличается от полученного в карточке пользователя");
                    }

                    
                    var manualDepoAmountParse = driver.FindElement(By.CssSelector("tr[id^=transactions] td[name='col-Amount']")).Text.Split(' ');
                    double amountForCheck;
                    if (manualDepoAmountParse.Length > 2)
                    {
                        string str = string.Join(" ", manualDepoAmountParse);
                        amountForCheck = Math.Round(Double.Parse(str.Substring(0, str.LastIndexOf(" ")).Replace(" ", ""), provider), 2);
                    }
                    else
                    {
                        amountForCheck = (Math.Round(Double.Parse(manualDepoAmountParse[0], provider), 2));
                    }
                    
                    if (depoList[i].ManualDepoInCardAmount != amountForCheck)
                    {
                        diffDataList.Add($"Сумма ручного пополнения из отчёта по счету: {amountForCheck} и она отличается от полученной в карточке пользователя");
                    }
                }
            }
            if (diffDataList.Count > 0)
            {
                return diffDataList;
            }
            else
            {
                diffDataList.Add("Различия не выявлены");
                return diffDataList;
            }
        }
    }
}
