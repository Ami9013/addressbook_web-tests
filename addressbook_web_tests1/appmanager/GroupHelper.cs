using System;
using System.Collections.Generic;
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

        private List<GroupData> groupCache = null;


        /// <summary>
        /// Получаем и формируем список групп, в список записываем Name, Header, Footer и Id группы. 
        /// Возвращаем копию кеша основанного на сформированном списке
        /// </summary>
        public List<GroupData> GetGroupFullData()
        {
            if (groupCache == null)
            {
                manager.Navigator.GoToGroupsPage();
                groupCache = new List<GroupData>();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                List<string> groupIDsList = new List<string>();
                foreach (IWebElement element in elements)
                {
                    groupIDsList.Add(element.FindElement(By.CssSelector("span.group input")).GetAttribute("value"));
                }


                
                foreach (var item in groupIDsList)
                {
                    GroupData fullGroupModel = new GroupData();
                    driver.FindElement(By.CssSelector("input[name='selected[]'][value='" + item + "']")).Click();
                    EditGroup();
                    fullGroupModel.Id = item;
                    fullGroupModel.Name = driver.FindElement(By.CssSelector("form input[name='group_name']")).GetAttribute("value");
                    fullGroupModel.Header = driver.FindElement(By.CssSelector("form textarea[name='group_header']")).GetAttribute("value");
                    fullGroupModel.Footer = driver.FindElement(By.CssSelector("form textarea[name='group_footer']")).GetAttribute("value");
                    manager.Navigator.GoToGroupsPage();
                    groupCache.Add(fullGroupModel);
                }
            }
            return new List<GroupData>(groupCache);
        }



        /// <summary>
        /// Возвращает количество элементов(строк) с группами 
        /// </summary>
        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        /// <summary>
        /// Проверяет наличие групп, если их нет - создает
        /// </summary>
        /// <returns></returns>
        public GroupHelper GroupCheck()
        {
            manager.Navigator.GoToGroupsPage();
            if (GetGroupCount() == 0)
            {
                CreateGroup(GroupData.groupModel);
            }
            return this;
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
            if (GetGroupCount() == 0)
            {
                CreateGroup(GroupData.groupModel);
            }
            SelectGroup(p);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// Удаляет группу по id переданного объекта(группы) 
        /// </summary>
        public GroupHelper Remove(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            if (GetGroupCount() == 0)
            {
                CreateGroup(GroupData.groupModel);
            }
            SelectGroup(group.Id);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// Изменяет группу по id переданного объекта(группы)
        /// </summary>
        /// <param name="group"></param>
        /// <param name="newgroupData"></param>
        /// <returns></returns>
        public GroupHelper Modify(GroupData group, GroupData newgroupData)
        {
            manager.Navigator.GoToGroupsPage();
            if (GetGroupCount() == 0)
            {
                CreateGroup(GroupData.groupModel);
            }
            SelectGroup(group.Id);
            EditGroup();
            FillGroupForm(newgroupData);
            SubmitGroupEdit();
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
            if (GetGroupCount() == 0)
            {
                CreateGroup(GroupData.groupModel);
            }
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
            groupCache = null;
            return this;
        }

        /// <summary>
        /// Отмечает(выбирает) группу
        /// </summary>
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElements(By.CssSelector("input[name='selected[]']"))[index].Click();
            return this;
        }

        /// <summary>
        /// Отмечает(выбирает) группу по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GroupHelper SelectGroup(String id)
        {
            driver.FindElement(By.CssSelector("input[name='selected[]'][value='" + id + "']")).Click();
            return this;
        }


        /// <summary>
        /// Удаляет выбранную группу
        /// </summary>
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.CssSelector("div#content input[name='delete']")).Click();
            groupCache = null;
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
            groupCache = null;
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