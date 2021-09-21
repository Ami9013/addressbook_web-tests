using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    /// <summary>
    /// Хранит в себе вспомогательные методы по работе с группами
    /// </summary>
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        /// <summary>
        /// Создает группу
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для создания группы. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public GroupHelper CreateGroup(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// Удаляет группу по переданному индексу
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для удаления группы. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public GroupHelper Remove(int p) 
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(p);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// Изменяет группу по переданному индексу
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для изменения группы. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public GroupHelper Modify(int p, GroupData newgroupData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(p);
            EditGroup();
            FillGroupForm(newgroupData);
            SubmitGroupEdit();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// Открывает форму создания группы
        /// </summary>
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.CssSelector("form input[name='new']")).Click();
            return this;
        }

        /// <summary>
        /// Заполняет форму создания/редактирования группы переданными значениями
        /// </summary>
        public GroupHelper FillGroupForm(GroupData group)
        {
            driver.FindElement(By.CssSelector("form input[name='group_name']")).Clear();
            driver.FindElement(By.CssSelector("form input[name='group_name']")).SendKeys(group.Name);
            driver.FindElement(By.CssSelector("form textarea[name='group_header']")).Clear();
            driver.FindElement(By.CssSelector("form textarea[name='group_header']")).SendKeys(group.Header);
            driver.FindElement(By.CssSelector("form textarea[name='group_footer']")).Clear();
            driver.FindElement(By.CssSelector("form textarea[name='group_footer']")).SendKeys(group.Footer);
            return this;
        }

        /// <summary>
        /// Сохраняет форму создания группы
        /// </summary>
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.CssSelector("form input[name='submit']")).Click();
            return this;
        }

        /// <summary>
        /// Отмечает(выбирает) группу
        /// </summary>
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElements(By.CssSelector("input[name='selected[]']"))[index-1].Click();
            return this;
        }

        /// <summary>
        /// Удаляет выбранную группу
        /// </summary>
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.CssSelector("div#content input[name='delete']")).Click();
            return this;
        }

        /// <summary>
        /// Открывает форму редактирования группы
        /// </summary>
        public GroupHelper EditGroup()
        {
            driver.FindElement(By.CssSelector("div#content input[name='edit']")).Click();
            return this;
        }

        /// <summary>
        /// Сохраняет форму редактирования группы
        /// </summary>
        public GroupHelper SubmitGroupEdit()
        {
            driver.FindElement(By.CssSelector("div#content input[name='update']")).Click();
            return this;
        }

        /// <summary>
        /// Возвращает на вкладку "Группы"
        /// </summary>
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.CssSelector("div.msgbox a")).Click();
            return this;
        }
    }
}
