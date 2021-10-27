using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            appManager.Auth.Logout();
            AccountData account = new AccountData("admin", "secret");
            appManager.Auth.Login(account);
            Assert.IsTrue(appManager.Auth.IsLoggedIn(account));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            appManager.Auth.Logout();
            AccountData account = new AccountData("admin", "123467");
            appManager.Auth.Login(account);
            Assert.IsFalse(appManager.Auth.IsLoggedIn(account));
        }
    }
}
