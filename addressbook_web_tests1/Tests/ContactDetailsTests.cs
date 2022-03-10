using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDetailsTest : AuthTestBase
    {
        [Test]
        public void TestcontactDetailsInfo()
        {
            //Получаем из таблицы(человечек) инфу о контакте сплошным текстом
            string fromDetails = appManager.Contacts.GetFullDetails(0);

            // Получаем информацию о контакте из формы редактирования и приводим ее к виду сплошного текста(клеим), который получили из Details(человечек)
            ContactData fromForm = appManager.Contacts.GetContactInformationFromEditForm(0);

            
            var fromformToString = appManager.Contacts.FromModelToStringConvert(fromForm).Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    
            var fromDetailsToValid = fromDetails.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            
            Assert.AreEqual(fromDetailsToValid, fromformToString);
        }
    }
}
