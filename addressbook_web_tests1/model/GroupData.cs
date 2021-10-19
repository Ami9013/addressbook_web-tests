using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData
    {
        public string Name { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }


        public GroupData()
        {

        }

        public static GroupData groupModel = new GroupData
        {
            Name = "i Am new Group Name",
            Header = "i Am new Any group header",
            Footer = "i Am new Any group footer"
        };
    }
}