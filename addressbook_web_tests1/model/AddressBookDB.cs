using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace WebAddressbookTests
{
    public class AddressBookDB : LinqToDB.Data.DataConnection
    {
        //конструктор вызывает базовый класс и указывает название базы данных
        public AddressBookDB() : base("AddressBook") { }

        /// <summary>
        /// Извлекаем данные из таблицы БД, привязка к которой описана в GroupData
        /// </summary>
        public ITable<GroupData> Groups { get { return GetTable<GroupData>(); } }

        /// <summary>
        /// Извлекаем данные из таблицы БД, привязка к которой описана в ContactData
        /// </summary>
        public ITable<ContactData> Contacts { get { return GetTable<ContactData>(); } }

    }
}
