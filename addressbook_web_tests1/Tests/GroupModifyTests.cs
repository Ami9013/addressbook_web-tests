using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModifyTests : AuthTestBase
    {
        [Test]
        public void GroupModifyTest()
        {
            GroupData newgroupData = new GroupData
            {
                Name = "Group name after update",
                Header = "Group header after update",
                Footer = "Group footer after update"
            };

            List<GroupData> oldGroups = appManager.Groups.GetGroupFullData();
            GroupData oldData = oldGroups[0]; 

            appManager.Groups.Modify(0, newgroupData);

            Assert.AreEqual(oldGroups.Count, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupFullData();
            //реализация ниже именно такая, а не "oldGroups[0] = newgroupData;", т.к. в таком случае мы не теряем Id группы, хоть по нему и нет сравнения в Equals
            oldGroups[0].Name = newgroupData.Name;
            oldGroups[0].Header = newgroupData.Header;
            oldGroups[0].Footer = newgroupData.Footer;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newgroupData, group);
                }
            }

            appManager.Auth.Logout();

        }
    }
}
