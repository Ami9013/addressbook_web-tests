using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        /// <summary>
        /// Сравнение списка, полученного через UI со списком, полученным через базу данных
        /// </summary>
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                appManager.Auth.Login(new AccountData("admin", "secret"));
                List<GroupData> fromUI = appManager.Groups.GetGroupFullData();
                List<GroupData> fromDB = GroupData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }            
        }
    }
}
