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
    /// Базовый класс для классов-помощников. Хранит в себе общие вспомогательные методы
    /// </summary>
    public class HelperBase
    {
        protected IWebDriver driver;
        protected ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }

        /// <summary>
        /// Проверяет наличие элемента. Обрабатывает исключение если элемента нет
        /// </summary>
        public bool IsElementExists(By iElementName)
        {
            try
            {
                driver.FindElement(iElementName);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Метод проверяет наличие содержимого в полученной строке после символа. Используется в методе FromModelToStringConvert
        /// </summary>
        public string EmptyStringCheck(string symbolValue, string valueOfField)
        {
            string validFiled = default;
            if (!string.IsNullOrEmpty(valueOfField))
                validFiled = symbolValue + valueOfField;

            return validFiled;
        }
    }
}
