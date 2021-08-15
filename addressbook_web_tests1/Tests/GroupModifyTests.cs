using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModifyTests : TestBase
    {
        [Test]
        public void GroupModifyTest()
        {
            appManager.Navigator.GoToHomePage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
            appManager.Navigator.GoToGroupsPage();
            appManager.Groups.SelectGroup(1);
            appManager.Groups.EditGroup();
            GroupData group = new GroupData("name after update");
            group.Header = "header after update";
            group.Footer = "footer after update";
            appManager.Groups.FillGroupForm(group);
            appManager.Groups.SubmitGroupEdit();
            appManager.Groups.ReturnToGroupsPage();
        }
    }
}
