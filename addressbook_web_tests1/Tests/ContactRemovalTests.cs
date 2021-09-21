using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        /// <summary>
        /// Удаление через карточку редактирования
        /// </summary>
        [Test]
        public void ContactRemovalTest()
        {
            appManager.Contacts.RemoveContactInEditCard(3);
            appManager.Auth.Logout();
        }

        /// <summary>
        /// Удаление в таблице контактов
        /// </summary>
        [Test]
        public void ContactRemovalTestByIndex()
        {
            appManager.Contacts.RemoveByIndex(2);
            appManager.Auth.Logout();
        }
    }
}
