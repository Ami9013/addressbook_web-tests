using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {   
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData
            {
                Name = "Group Name",
                Header = "Any group header",
                Footer = "Any group footer",
            };

            List<GroupData> oldGroups = appManager.Groups.GetGroupFullData();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupFullData();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);

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

            List<GroupData> oldGroups = appManager.Groups.GetGroupFullData();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupFullData();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData
            {
                Name = "a'a",
                Header = "b'b",
                Footer = "c'c"
            };

            List<GroupData> oldGroups = appManager.Groups.GetGroupFullData();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupFullData();
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            appManager.Auth.Logout();
        }
    }
}
