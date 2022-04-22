using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Globalization;


namespace WebAddressbookTests
{
    /// <summary>
    /// Хранит в себе вспомогательные методы по работе с контактами
    /// </summary>
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }


        /// <summary>
        /// Получает и возвращает информацию о контакте, полученную из формы редактирования
        /// </summary>
        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            ModifyContact(index);
            string firstName = driver.FindElement(By.CssSelector("div#content input[name='firstname']")).GetAttribute("value");
            string middleName = driver.FindElement(By.CssSelector("div#content input[name='middlename']")).GetAttribute("value");
            string lastName = driver.FindElement(By.CssSelector("div#content input[name='lastname']")).GetAttribute("value");
            string nickName = driver.FindElement(By.CssSelector("div#content input[name='nickname']")).GetAttribute("value");
            string companyName = driver.FindElement(By.CssSelector("div#content input[name='company']")).GetAttribute("value");
            string title = driver.FindElement(By.CssSelector("div#content input[name='title']")).GetAttribute("value");
            string address = driver.FindElement(By.CssSelector("div#content textarea[name='address']")).GetAttribute("value");
            string homePhone = driver.FindElement(By.CssSelector("div#content input[name='home']")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.CssSelector("div#content input[name='mobile']")).GetAttribute("value");
            string workPhone = driver.FindElement(By.CssSelector("div#content input[name='work']")).GetAttribute("value");
            string fax = driver.FindElement(By.CssSelector("div#content input[name='fax']")).GetAttribute("value");
            string email1 = driver.FindElement(By.CssSelector("div#content input[name='email']")).GetAttribute("value");
            string email2 = driver.FindElement(By.CssSelector("div#content input[name='email2']")).GetAttribute("value");
            string email3 = driver.FindElement(By.CssSelector("div#content input[name='email3']")).GetAttribute("value");
            string homePage = driver.FindElement(By.CssSelector("div#content input[name='homepage']")).GetAttribute("value");
            string dayOfBirth = driver.FindElement(By.CssSelector("div#content select[name='bday'] option[selected]")).GetAttribute("value");
            string monthOfBirth = driver.FindElement(By.CssSelector("div#content select[name='bmonth'] option[selected]")).GetAttribute("value");
            string yearOfBirth = driver.FindElement(By.CssSelector("div#content input[name='byear']")).GetAttribute("value");
            string dayOfAnniversary = driver.FindElement(By.CssSelector("div#content select[name='aday'] option[selected]")).GetAttribute("value");
            string monthOfAnniversary = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(driver.FindElement(By.CssSelector("div#content select[name='amonth'] option[selected]")).GetAttribute("value"));
            string yearOfAnniversary = driver.FindElement(By.CssSelector("div#content input[name='ayear']")).GetAttribute("value");
            string secondAddress = driver.FindElement(By.CssSelector("div#content textarea[name='address2']")).GetAttribute("value");
            string homePhone2 = driver.FindElement(By.CssSelector("div#content input[name='phone2']")).GetAttribute("value");
            string secondNotes = driver.FindElement(By.CssSelector("div#content textarea[name='notes']")).GetAttribute("value");

            return new ContactData()
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                NickName = nickName,
                Company = companyName,
                Title = title,
                FirstAddress = address,
                FirstHomePhone = homePhone,
                Mobile = mobilePhone,
                WorkPhone = workPhone,
                Fax = fax,
                Email = email1,
                Email2 = email2,
                Email3 = email3,
                Homepage = homePage,
                DayOfBirth = Convert.ToInt32(dayOfBirth),
                MonthOfBirth = ContactData.MonthsOfYear[monthOfBirth],
                YearOfBirth = yearOfBirth,
                DayOfAnniversary = Convert.ToInt32(dayOfAnniversary),
                MonthOfAnniversary = ContactData.MonthsOfYear[monthOfAnniversary],
                YearOfAnniversary = yearOfAnniversary,
                SecondAddress = secondAddress,
                SecondHomePhone = homePhone2,
                SecondNotes = secondNotes
            };
        }

        /// <summary>
        /// Принимает объект типа ContactData, преобразует его в строку и возвращает её
        /// </summary>
        public string FromModelToStringConvert(ContactData modelToConvert)
        {

            string convertResult =

            modelToConvert.FirstName + EmptyStringCheck(" ", modelToConvert.MiddleName) + EmptyStringCheck(" ", modelToConvert.LastName) + Environment.NewLine +
            modelToConvert.NickName + Environment.NewLine +
            modelToConvert.Title + Environment.NewLine +
            modelToConvert.Company + Environment.NewLine +
            modelToConvert.FirstAddress + Environment.NewLine +
            EmptyStringCheck("H: ", modelToConvert.FirstHomePhone) + Environment.NewLine + 
            EmptyStringCheck("M: ", modelToConvert.Mobile) + Environment.NewLine +
            EmptyStringCheck("W: ", modelToConvert.WorkPhone) + Environment.NewLine +
            EmptyStringCheck("F: ", modelToConvert.Fax) + Environment.NewLine +
            modelToConvert.AllEmails + Environment.NewLine +
            EmptyStringCheck($"{ "Homepage:" + Environment.NewLine}", modelToConvert.Homepage) + Environment.NewLine +
            EmptyStringCheck($"Birthday", $"{DayValidate(modelToConvert.DayOfBirth)}{MonthValidate(modelToConvert.MonthOfBirth)} {modelToConvert.YearOfBirth}{CalculateYearOfBirth(modelToConvert)}") + Environment.NewLine +
            EmptyStringCheck($"Anniversary", $"{DayValidate(modelToConvert.DayOfAnniversary)}{MonthValidate(modelToConvert.MonthOfAnniversary)} {modelToConvert.YearOfAnniversary}{CalculateYearOfAnniversary(modelToConvert)}") + Environment.NewLine +
            modelToConvert.SecondAddress + Environment.NewLine +
            EmptyStringCheck("P: ", modelToConvert.SecondHomePhone) + Environment.NewLine +
            modelToConvert.SecondNotes;

            return convertResult.Trim();
        }

        
        
        // Валидатор дня рождения и годовщины
        string DayValidate(int day)
        {
            if (day == 0)
            {
                return "";
            }
            return Convert.ToString($" {day}.");
        }

        //Валидатор месяца рождения и годовщины
        string MonthValidate(int months)
        {
            if (months == 0)
            {
                return "";
            }
            return $" { new DateTime().AddMonths(months - 1).ToString("MMMM", new CultureInfo("en-US"))}";
        }

        /// <summary>
        /// Калькулятор  прожитых лет
        /// </summary>
        string CalculateYearOfBirth(ContactData getModel)
        {
            var currentDate = DateTime.Today;
            bool valueIsDigit = int.TryParse(getModel.YearOfBirth, out int validYear);

            // если полученную строку(ГОД) не удалось конвертировать в целое число, то расчет прожитых лет не выполняется. Возраст в скобках, следовательно, не выводится
            if (valueIsDigit)
            {
                int yearCalculate = currentDate.Year - validYear - 1 + ((currentDate.Month > getModel.MonthOfBirth || currentDate.Month == getModel.MonthOfBirth && currentDate.Day >= getModel.DayOfBirth) ? 1 : 0);
                                
                
                if (yearCalculate <= 149)
                {
                    string result = $" ({Convert.ToString(yearCalculate)})";
                    return result;
                }
            }                        
            return "";
        }

        /// <summary>
        /// Калькулятор годовщины
        /// </summary>
        string CalculateYearOfAnniversary(ContactData getModel)
        {
            var currentDate = DateTime.Today;
            bool valueIsDigit = int.TryParse(getModel.YearOfAnniversary, out int validYear);

            // если полученную строку(ГОД) не удалось конвертировать в целое число, то расчет годовщины лет не выполняется. Результат в скобках, следовательно, не выводится
            if (valueIsDigit)
            {
                //int yearCalculate = currentDate.Year - validYear;
                int yearCalculate = currentDate.Year - validYear - 1 + ((currentDate.Month > getModel.MonthOfAnniversary || currentDate.Month == getModel.MonthOfAnniversary && currentDate.Day >= getModel.DayOfAnniversary) ? 1 : 0);



                if (yearCalculate <= 149)
                {
                    string result = $" ({Convert.ToString(yearCalculate)})";
                    return result;
                }
            }
            return "";
        }

        
        /// <summary>
        /// Получает и возвращает информацию о контакте, полученную из таблицы на главной странице(home)
        /// </summary>
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData()
            {
                FirstName = firstName,
                LastName = lastName,
                FirstAddress = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        /// <summary>
        /// Получаем всё содержимое страницы с детальной информацией о контакте
        /// </summary>
        public string GetFullDetails(int p)
        {
            manager.Navigator.GoToHomePage();
            GoToDetailsPage(p);
            string allDetails = driver.FindElement(By.CssSelector("div[id='content']")).Text.Trim();
            if (allDetails.Contains("Member of"))
            {
                return allDetails.Remove(allDetails.IndexOf("Member of"));
            }
            return allDetails;
        }


        private List<ContactData> contactCache = null;
        /// <summary>
        /// Получаем и формируем список контактов, в список записываем First Name, Last Name, Full Name и Id контакта. 
        /// Возвращаем копию кеша основанного на сформированном списке
        /// </summary>
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    ContactData contact = new ContactData();
                    var tdElements = element.FindElements(By.CssSelector("td"));
                    var contactIDs = element.FindElement(By.TagName("input")).GetAttribute("id");
                    string textFirstName = tdElements[2].Text;
                    contact.FirstName = textFirstName;
                    string textLastName = tdElements[1].Text;
                    contact.LastName = textLastName;
                    string textFullName = tdElements[1].Text + " " + tdElements[2].Text;
                    contact.FullName = textFullName;
                    contact.Id = contactIDs;

                    contactCache.Add(contact);
                }
            }

            return new List<ContactData>(contactCache);
        }


        /// <summary>
        /// Возвращает количество элементов(строк) с контактами
        /// </summary>
        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }

        
        /// <summary>
        /// Создает контакт
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для создания контакта. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public ContactHelper ContactCreate(ContactData contact)
        {
            manager.Navigator.GoToAddContactPage();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        /// <summary>
        /// Удаляет контакт по переданному индексу
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для удаления контакта. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public ContactHelper RemoveByIndex(int p)
        {
            manager.Navigator.GoToHomePage();
            if (GetContactCount() == 0)
            {
                ContactCreate(ContactData.contactModel);
            }
            SelectContact(p);
            RemoveContact();
            ContactCloseAlert();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        /// <summary>
        /// Удаляет первый по списку контакт
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для удаления контакта. Обращается к вспомогательным методам своего класса и к методам класса NavigationHelper
        /// </summary>
        public ContactHelper RemoveContactInEditCard(int p)
        {
            manager.Navigator.GoToHomePage();
            if (GetContactCount() == 0)
            {
                ContactCreate(ContactData.contactModel);
            }
            ModifyContact(p);
            RemoveContactInCard();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        /// <summary>
        /// Удаляет контакт по id переданного объекта
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ContactHelper RemoveContactInEditCard(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            if (GetContactCount() == 0)
            {
                ContactCreate(ContactData.contactModel);
            }
            ModifyContact(contact.Id);
            RemoveContactInCard();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        /// <summary>
        /// Изменяет контакт по переданному индексу
        /// Высокоуровневый метод. Содержит в себе все необходимые методы для изменения контакта. Обращается к вспомогательным методам своего класса
        /// </summary>
        public ContactHelper Modify(int p, ContactData newContactData)
        {
            manager.Navigator.GoToHomePage();
            if (GetContactCount() == 0)
            {
                ContactCreate(ContactData.contactModel);
            }
            ModifyContact(p);
            FillContactForm(newContactData, false);
            SubmitContactModify();
            ReturnToHomePageafterUpd();
            return this;
        }

        /// <summary>
        /// Изменяет контакт по переданному id 
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="newContactData"></param>
        /// <returns></returns>
        public ContactHelper Modify(ContactData contact, ContactData newContactData)
        {
            manager.Navigator.GoToHomePage();
            if (GetContactCount() == 0)
            {
                ContactCreate(ContactData.contactModel);
            }
            ModifyContact(contact.Id);
            FillContactForm(newContactData, false);
            SubmitContactModify();
            ReturnToHomePageafterUpd();
            return this;
        }

        /// <summary>
        /// Заполняет форму создания контакта переданными значениями
        /// </summary>
        public ContactHelper FillContactForm(ContactData contact, bool DoModifyGroup = true)
        {
            driver.FindElement(By.CssSelector("div#content input[name='firstname']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='firstname']")).SendKeys(contact.FirstName);
            driver.FindElement(By.CssSelector("div#content input[name='middlename']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='middlename']")).SendKeys(contact.MiddleName);
            driver.FindElement(By.CssSelector("div#content input[name='lastname']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='lastname']")).SendKeys(contact.LastName);
            driver.FindElement(By.CssSelector("div#content input[name='nickname']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='nickname']")).SendKeys(contact.NickName);
            driver.FindElement(By.CssSelector("div#content input[name='company']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='company']")).SendKeys(contact.Company);
            driver.FindElement(By.CssSelector("div#content input[name='title']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='title']")).SendKeys(contact.Title);
            driver.FindElement(By.CssSelector("div#content textarea[name='address']")).Clear();
            driver.FindElement(By.CssSelector("div#content textarea[name='address']")).SendKeys(contact.FirstAddress);
            driver.FindElement(By.CssSelector("div#content input[name='home']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='home']")).SendKeys(contact.FirstHomePhone);
            driver.FindElement(By.CssSelector("div#content input[name='mobile']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='mobile']")).SendKeys(contact.Mobile);
            driver.FindElement(By.CssSelector("div#content input[name='work']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='work']")).SendKeys(contact.WorkPhone);
            driver.FindElement(By.CssSelector("div#content input[name='fax']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='fax']")).SendKeys(contact.Fax);
            driver.FindElement(By.CssSelector("div#content input[name='email']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='email']")).SendKeys(contact.Email);
            driver.FindElement(By.CssSelector("div#content input[name='email2']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='email2']")).SendKeys(contact.Email2);
            driver.FindElement(By.CssSelector("div#content input[name='email3']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='email3']")).SendKeys(contact.Email3);
            driver.FindElement(By.CssSelector("div#content input[name='homepage']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='homepage']")).SendKeys(contact.Homepage);
            driver.FindElement(By.CssSelector("div#content select[name='bday']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='bday']"))).SelectByIndex(contact.DayOfBirth + 1);
            driver.FindElement(By.CssSelector("div#content select[name='bmonth']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='bmonth']"))).SelectByIndex(contact.MonthOfBirth);
            driver.FindElement(By.CssSelector("div#content input[name='byear']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='byear']")).SendKeys(contact.YearOfBirth);
            driver.FindElement(By.CssSelector("div#content select[name='aday']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='aday']"))).SelectByIndex(contact.DayOfAnniversary + 1);
            driver.FindElement(By.CssSelector("div#content select[name='amonth']")).Click();
            new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='amonth']"))).SelectByIndex(contact.MonthOfAnniversary);
            driver.FindElement(By.CssSelector("div#content input[name='ayear']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='ayear']")).SendKeys(contact.YearOfAnniversary);
            if (DoModifyGroup)
            {
                driver.FindElement(By.CssSelector("div#content select[name='new_group']")).Click();
                new SelectElement(driver.FindElement(By.CssSelector("div#content select[name='new_group']"))).SelectByIndex(contact.GroupOfContact);
            }
            driver.FindElement(By.CssSelector("div#content textarea[name='address2']")).Clear();
            driver.FindElement(By.CssSelector("div#content textarea[name='address2']")).SendKeys(contact.SecondAddress);
            driver.FindElement(By.CssSelector("div#content input[name='phone2']")).Clear();
            driver.FindElement(By.CssSelector("div#content input[name='phone2']")).SendKeys(contact.SecondHomePhone);
            driver.FindElement(By.CssSelector("div#content textarea[name='notes']")).Clear();
            driver.FindElement(By.CssSelector("div#content textarea[name='notes']")).SendKeys(contact.SecondNotes);
            return this;
        }


        /// <summary>
        /// Сохраняет форму создания контакта 
        /// </summary>
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.CssSelector("form[name='theform'] input[name='submit']")).Click();
            contactCache = null;
            return this;
        }

        /// <summary>
        /// Отмечает(выбирает) контакт
        /// </summary>
        public ContactHelper SelectContact(int index)
        {
            driver.FindElements(By.CssSelector("input[name='selected[]']"))[index].Click();
            return this;
        }

        /// <summary>
        /// Открывает форму редактирования контакта (без захода в Details)
        /// </summary>
        public ContactHelper ModifyContact(int index)
        {
            driver.FindElements(By.CssSelector("table[id=maintable] td:nth-child(8) a"))[index].Click();
            return this;
        }

        /// <summary>
        /// Открывает форму редактирования контакта по переданному id 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ContactHelper ModifyContact(String id)
        {
            driver.FindElement(By.CssSelector("table[id=maintable] td:nth-child(8) a[href$='" + id + "']")).Click();
            return this;
        }

        /// <summary>
        /// Осуществляет переход в карточку просмотра детальной информации о контакте
        /// </summary>
        public ContactHelper GoToDetailsPage(int index)
        {
            driver.FindElements(By.CssSelector("table[id=maintable] td:nth-child(7) a "))[index].Click();
            return this;
        }

        /// <summary>
        /// Сохраняет форму редактирования контакта
        /// </summary>
        public ContactHelper SubmitContactModify()
        {
            driver.FindElement(By.CssSelector("div#content input[name='update']")).Click();
            contactCache = null;
            return this;
        }

        /// <summary>
        /// Удаляет отмеченный чек-боксом контакт
        /// </summary>
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.CssSelector("form[name = 'MainForm'] div:nth-child(8) input")).Click();
            contactCache = null;
            return this;
        }

        /// <summary>
        /// Удаляет контакт (через карточку редактирования)
        /// </summary>
        public ContactHelper RemoveContactInCard()
        {
            driver.FindElement(By.CssSelector("form:nth-child(3) input[name='update']")).Click();
            contactCache = null;
            return this;
        }

        /// <summary>
        /// Закрывает согласием всплывающее окно
        /// </summary>
        public ContactHelper ContactCloseAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        /// <summary>
        /// Возвращает на домашнюю страницу переходя по ссылке из сообщения после редактирования
        /// </summary>
        public ContactHelper ReturnToHomePageafterUpd()
        {
            driver.FindElement(By.CssSelector("div.msgbox a")).Click();
            return this;
        }
    }
}