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
        /// Удаляет группу
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для удаления группы. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public GroupHelper Remove(int p) 
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(1);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// Изменяет группу
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для изменения группы. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public GroupHelper Modify(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(1);
            EditGroup();
            FillGroupForm(group);
            SubmitGroupEdit();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// Открывает форму создания группы
        /// </summary>
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        /// <summary>
        /// Заполняет форму создания/редактирования группы переданными значениями
        /// </summary>
        public GroupHelper FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
            return this;
        }

        /// <summary>
        /// Сохраняет форму создания группы
        /// </summary>
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.XPath("//input[@type='submit' and @name='submit']")).Click();
            return this;
        }

        /// <summary>
        /// Отмечает(выбирает) группу
        /// </summary>
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@type='checkbox' and @name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        /// <summary>
        /// Удаляет выбранную группу
        /// </summary>
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        /// <summary>
        /// Открывает форму редактирования группы
        /// </summary>
        public GroupHelper EditGroup()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        /// <summary>
        /// Сохраняет форму редактирования группы
        /// </summary>
        public GroupHelper SubmitGroupEdit()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        ///// <summary>
        ///// Возвращает на вкладку "Группы"
        ///// </summary>
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.XPath("//div[@class='msgbox']//a[contains(@href,'group.php')]")).Click();
            return this;
        }
    }
}
