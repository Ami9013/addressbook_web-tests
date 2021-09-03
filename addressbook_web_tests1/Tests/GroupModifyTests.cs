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
            GroupData group = new GroupData("name after update");
            group.Header = "header after update";
            group.Footer = "footer after update";
            appManager.Groups.Modify(group);
            appManager.Auth.Logout();
        }
    }
}
