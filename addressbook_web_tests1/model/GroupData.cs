using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        [Column(Name = "group_name")]
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; }

        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }


        public GroupData()
        {

        }

        public GroupData(string text)
        {

        }


        // Метод сравнения Имени групп
        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name && Header == other.Header && Footer == other.Footer;
        }

        // Метод сравнения Хэш-кодов имени. Составляет числовой хэш по объекту
        public override int GetHashCode()
        {
            return (Name + Header + Footer).GetHashCode();
        }

        // Возвращает строковое представление объектов типа GroupData
        public override string ToString()
        {
            return "\nname=" + Name + "\nheader=" + Header + "\nfooter=" + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public static GroupData groupModel = new GroupData
        {
            Name = TestBase.GenerateRandomString(10),
            Header = TestBase.GenerateRandomString(10),
            Footer = TestBase.GenerateRandomString(10)
        };

        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();                
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00") select c).Distinct().ToList();
            }
        }
    }
}