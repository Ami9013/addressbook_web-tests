using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModifyTests : GroupTestBase
    {
        [Test]
        public void GroupModifyTest()
        {
            GroupData newgroupData = new GroupData
            {
                Name = GenerateRandomString(20),
                Header = GenerateRandomString(20),
                Footer = GenerateRandomString(20)
            };

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData oldData = oldGroups[0]; //группа для модификации

            appManager.Groups.Modify(oldData, newgroupData);

            Assert.AreEqual(oldGroups.Count, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
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
