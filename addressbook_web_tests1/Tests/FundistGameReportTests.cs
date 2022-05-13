using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics;

namespace WebAddressbookTests
{
    [TestFixture]
    public class FundistGameReportTests : FundistAuthTestBase
    {
        [Test]
        public void FundistGameReportTest() // Структура должна быть либо тут основные(верхние) методы + проверки, а сами методы получения и пр. в хелперах (как у Баранцева)
        {
            appManager.FundistNavigator.GoToGameReport();
            //appManager.FundistFilter.ClickOnExtendedTab();

            //appManager.FundistFilter.SetMonthInDropDown();
            //appManager.FundistFilter.FindButton();

            //appManager.FundistFilter.SetRandomProvider();//метод не должен принимать решения о разлогах и т.д., интеллектуальными должны быть тесты а не методы //из списка убирать мерча, игры которого не удовлетворили условиям
            //appManager.FundistFilter.SetRandomGame();

            //FundistUserData userDataFromGameReport = appManager.FundistFilter.GetUserWithBets();

            //appManager.FundistFilter.GoToUserCard(userDataFromGameReport.Id);


            //List<UserInCardDeposits> allDepostList = new List<UserInCardDeposits>();

            ////appManager.UserHelper.GetSuccessDepositsInfo(allDepostList);
            ////appManager.UserHelper.GetManualDeposits(allDepostList);

            //appManager.UserHelper.GoToReportFromSummary();

            //FundistUserData userDataFromStatements = appManager.FundistFilter.GetBetsOfStatementReport(userDataFromGameReport);
            //Assert.AreEqual(userDataFromGameReport, userDataFromStatements);


            //var res = appManager.FundistFilter.GetUserDepositsFromStatement(allDepostList, userDataFromGameReport); //Здесь я хотел выводить список с записями, которые различаются, либо условную запись, если различий нет

            List<string> res = new List<string>();

            if (res.Count > 0)
            {
                foreach (var el in res)
                {
                    try
                    {
                        Assert.Fail(el);
                    }
                    catch
                    {

                    }

                }

            }
            Console.WriteLine("ok");
        }
    }
}
