using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager appManager;
        [SetUp]
        public void SetupTest()
        {
            appManager = new ApplicationManager();
            appManager.Navigator.GoToHomePage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
        }
        [TearDown]
        public void TeardownTest()
        {
            appManager.Auth.Logout();
            appManager.Stop();
        }

        //TODO
        // Если -  ОК, то переписать везде геттеры и сеттеры: ContactData и GroupData
        // Если -  ОК, то переписать везде где есть findelement под идентификаторы, а не под текст
        // что делать с username и password если они нигде не используются
    }
}