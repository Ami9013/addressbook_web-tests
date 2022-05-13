using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class FundistAccountData
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public FundistAccountData(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
