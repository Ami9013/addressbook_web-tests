using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class FundistAuthTestBase : FundistTestBase
    {
        [SetUp]
        public void SetupLogin() //логинюсь по заданным кредам, на обработку в зависимости от среды не хватило времени
        {
            appManager.FundistAuth.Login(new FundistAccountData("anatoly_opsait_jpc", "Test123!"));
        }
    }
}