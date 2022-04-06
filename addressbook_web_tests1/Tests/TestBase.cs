using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Globalization;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager appManager;

        [SetUp]
        public void SetupApplicationManager()
        {
            appManager = ApplicationManager.GetInstance();
        }


        public static Random rnd = new Random();

        /// <summary>
        /// Генератор рандомных буквенно-цифровых строк рандомной длины, но не больше заданной с захардкоженным наобором символов и цифр. 
        /// </summary>
        /// <param name="max">Принимает максимальную длинну строки</param>
        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < l; i++)
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65))));

            return builder.ToString();
        }
                
        
        /// <summary>
        /// Генератор данных для года рождения и годовщины. 
        /// </summary>
        /// <returns>Возвращает строку, представляющую собой год</returns>
        public static string RndYearBuilder() //добавить кейс с невалидными(граничными) значениями
        {
            int validOrNotValue = rnd.Next(0, 2); 
            StringBuilder yearBuild = new StringBuilder();

            if (validOrNotValue == 1) 
            {
                int invalidOrNot = rnd.Next(0, 2);
                if (invalidOrNot == 1)
                {
                    int validDigitYear = rnd.Next(DateTime.Now.Year - 148, DateTime.Now.Year + 1);
                    return validDigitYear.ToString();
                }

                int invalidDigitYear = rnd.Next(0,10000);
                return invalidDigitYear.ToString();
            }
            else     
            {
                for (int x = 0; x < 4; ++x)
                    yearBuild.Append((char)rnd.Next(48, 91));
                return yearBuild.ToString();
            }
        }

        /// <summary>
        /// Генератор рандомного месяца
        /// </summary>
        public static int RandomMonth()
        {
            List<string> monthsList = new List<string>();

            foreach (var item in ContactData.MonthsOfYear.Keys) //перебираем все ключи(string) из словаря и записываем их в лист в том числе 0 элемент "-"
            {
                monthsList.Add(item);
            }

            string getMonth = monthsList[rnd.Next(monthsList.Count)]; //берем случайное число из списка и присваиваем название месяца, который соотв. числу
            int covertMonthToIndex = ContactData.MonthsOfYear[getMonth]; //по названию месяца(ключ) получаем значение(int) и записываем его в поле контакта (MonthOfBirth или MonthOfAnniversary)
            return covertMonthToIndex;
        }              
        
        /// <summary>
        /// Генератор рандомного числа для дня месяца, а также для выбора группы контакта
        /// </summary>
        public static int RandomDigit(int max)
        {
            int day = rnd.Next(0, max);
            return day;
        }
    }
}