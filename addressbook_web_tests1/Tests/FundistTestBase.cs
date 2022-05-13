using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace WebAddressbookTests
{
    public class FundistTestBase
    {
        protected ApplicationManager appManager;

        [SetUp]
        public void SetupApplicationManager()
        {
            appManager = ApplicationManager.GetFundistInstance();
        }

        [TearDown]
        public void Logout()
        {
            appManager.FundistAuth.Logout();
        }
    }
}
