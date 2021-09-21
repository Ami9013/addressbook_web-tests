using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {   
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData
            {
                Name = "Group Name",
                Header = "Any group header",
                Footer = "Any group footer"
            };

            appManager.Groups.CreateGroup(group);
            appManager.Auth.Logout();
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData
            {
                Name = "",
                Header = "",
                Footer = ""
            };

            appManager.Groups.CreateGroup(group);
            appManager.Auth.Logout();
        }
    }
}
