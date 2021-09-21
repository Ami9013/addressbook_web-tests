﻿using System;
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
            GroupData newgroupData = new GroupData
            {
                Name = "Group name after update",
                Header = "Group header after update",
                Footer = "Group footer after update"
            };

            appManager.Groups.Modify(2, newgroupData);
            appManager.Auth.Logout();
        }
    }
}
