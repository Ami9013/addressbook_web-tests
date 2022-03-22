using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        public string Name { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }

        public string Id { get; set; }


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
            Name = "i Am new Group Name",
            Header = "i Am new Any group header",
            Footer = "i Am new Any group footer"
        };
    }
}