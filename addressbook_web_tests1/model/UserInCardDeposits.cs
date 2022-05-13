using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace WebAddressbookTests
{
    public class UserInCardDeposits
    {
        public string SuccessDepoInCardDate { get; set; }//разделение не требуется, т.к. нет разницы какие из них мы сверяем в отчете по счету
        public string SuccessDepoInCardComment { get; set; }
        public double SuccessDepoInCardAmount { get; set; }
        public double ManualDepoInCardAmount { get; set; }
        public string ManualDepoInCardComment { get; set; }
        public string ManualDepoInCardDate { get; set; }
    }
}
