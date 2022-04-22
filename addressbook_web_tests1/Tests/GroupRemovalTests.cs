using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = GroupData.GetAll();
            if (oldGroups.Count == 0)
            {
                appManager.Groups.CreateGroup(GroupData.groupModel);
                oldGroups = GroupData.GetAll();
            }
            GroupData toBeRemoved = oldGroups[0];

            appManager.Groups.Remove(toBeRemoved);

            Assert.AreEqual(oldGroups.Count - 1, appManager.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
