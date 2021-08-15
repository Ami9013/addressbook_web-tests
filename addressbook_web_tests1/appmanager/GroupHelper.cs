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
        public GroupHelper(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Открывает форму создания группы
        /// </summary>
        public void InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }

        /// <summary>
        /// Заполняет форму создания/редактирования группы переданными значениями
        /// </summary>
        public void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Click();
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }

        /// <summary>
        /// Сохраняет форму создания группы
        /// </summary>
        public void SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        /// <summary>
        /// Отмечает(выбирает) группу
        /// </summary>
        public void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
        }

        /// <summary>
        /// Удаляет выбранную группу
        /// </summary>
        public void RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }

        /// <summary>
        /// Открывает форму редактирования группы
        /// </summary>
        public void EditGroup()
        {
            driver.FindElement(By.Name("edit")).Click();
        }

        /// <summary>
        /// Сохраняет форму редактирования группы
        /// </summary>
        public void SubmitGroupEdit()
        {
            driver.FindElement(By.Name("update")).Click();
        }

        ///// <summary>
        ///// Возвращает на вкладку "Группы"
        ///// </summary>
        public void ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }


    }
}
