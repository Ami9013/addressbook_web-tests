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
                Footer = "Any group footer"
            };

            List<GroupData> oldGroups = appManager.Groups.GetGroupList();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
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

            List<GroupData> oldGroups = appManager.Groups.GetGroupList();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
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
                Header = "",
                Footer = ""
            };

            List<GroupData> oldGroups = appManager.Groups.GetGroupList();

            appManager.Groups.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupList();
            // Т.к. группу с именем "a'a" создать нельзя - старый и новый списки, по количеству, будут равны. Поэтому я их просто сортирую и сравниваю 
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            appManager.Auth.Logout();
        }
    }
}
