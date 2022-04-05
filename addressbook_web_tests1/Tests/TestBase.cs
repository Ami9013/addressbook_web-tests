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
        /// Генератор рандомных строк по Баранцеву, который я немного модернизировал. Ни оригинал, ни текущая версия мне не зашла, т.к. задавая диапазон символов часто встречаются !@#$%^&*() что визуально не гуд
        /// </summary>
        /// <param name="p">Принимает максимальную длинну строки</param>
        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < l; i++)
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65))));

            return builder.ToString();
        }


        /// <summary>
        /// Генератор рандомных буквенно-цифровых строк рандомной длины, но не больше заданной с захардкоженным наобором символов и цифр. ------ убрать
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        //public static string RandomDataGenerator(int max)
        //{
        //    int length = Convert.ToInt32(rnd.NextDouble() * max);
        //    StringBuilder builder = new StringBuilder();
        //    string words = "abcdefghijklomnopqrstuvwxyz_ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    for (int i = 0; i < length; i++)
        //        builder.Append(words[rnd.Next(words.Length)]);
        //    return builder.ToString();
        //}


        /// <summary>
        /// Что-то похожее на метод по Баранцеву, но также всё упирается в заданный диапазон символов, в который входят всякие !"№;%:?*() ------ убрать
        /// </summary>
        //public static string GenerateRandomDataAscII(int length)
        //{
        //    Random random = new Random();
        //    StringBuilder sbuilder = new StringBuilder();
        //    for (int x = 0; x < length; ++x)
        //        sbuilder.Append((char)random.Next(48, 91));

        //    return sbuilder.ToString();
        //}


        /// <summary>
        /// Генератор данных для номеров телефонов. Собирает строку рандомной длины, но не более переданной (int max) из символов, допустимых для формирования номера телефона(хоть и в перемешку).
        /// </summary>
        //public static string RandomPhoneData(int max)
        //{
        //    int length = Convert.ToInt32(rnd.NextDouble() * max);
        //    StringBuilder builder = new StringBuilder();
        //    string words = "1234567890()-+";
        //    for (int i = 0; i < length; i++)
        //        builder.Append(words[rnd.Next(words.Length)]);
        //    return builder.ToString();
        //}


        /// <summary>
        /// Генератор данных для года рождения и годовщины. 
        /// </summary>
        //public static string RandomYearData()
        //{
        //    Random rand = new Random();
        //    int validOrNotValue = rand.Next(0, 2); // определяем, будет ли значения валидным или нет
        //    StringBuilder yearBuild = new StringBuilder();

        //    if (validOrNotValue == 1) // если рандомом определено, что значение валидное, то Год составляется из чисел, не выходящих за заданный диапазон
        //    {
        //        int a;
        //        int b = 0;
        //        int c;
        //        int d;
        //        a = rand.Next(1, 3);

        //        if (a == 1) // если год начинается с 1, то минимальный диапазон - 1874
        //        {
        //            b = rand.Next(8, 10);

        //            if (b == 8)
        //            {
        //                c = rand.Next(7, 10);
        //                d = rand.Next(4, 10);
        //            }
        //            else
        //            {
        //                c = rand.Next(1, 10);
        //                d = rand.Next(1, 10);
        //            }
        //        }
        //        else //если год начинается с 2, то максимальный диапазон - 2022
        //        {

        //            c = rand.Next(0, 3);
        //            if (c == 2)
        //                d = rand.Next(0, 3);
        //            else
        //                d = rand.Next(0, 10);
        //        }
        //        return $"{a}{b}{c}{d}";
        //    }
        //    else     // если рандомом определено, что значение должно быть невалидным, то составляем значение длиной = 4 символам из рандомных символов в заданном диапазоне таблицы ASCII
        //    {
        //        for (int x = 0; x < 4; ++x)
        //            yearBuild.Append((char)rand.Next(48, 91));
        //        return yearBuild.ToString();
        //    }
        //}


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
                int year = rnd.Next(DateTime.Now.Year - 148, DateTime.Now.Year + 1);
                return year.ToString();
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
            //int newDate = rnd.Next(ContactData.MonthsOfYear.Count); 
            //return newDate;
            //эта реализация примитивна, можно было бы обойтись и просто Next(0,13), но а в итоге пришел к рандомайзу по List 

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