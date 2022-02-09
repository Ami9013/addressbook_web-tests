using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDetailsTest : AuthTestBase
    {
        [Test]
        public void TestcontactDetailsInfo()
        {
            //Получаем из таблицы(человечек) инфу о контакте сплошным текстом
            string fromDetails = appManager.Contacts.GetFullDetails(0);
            // Получаем информацию о контакте из формы редактирования и приводим ее к виду сплошного текста(клеим), который получили из Details(человечек)
            ContactData fromForm = appManager.Contacts.GetContactInformationFromEditForm(0);




            string[] stringArrFromDetails = fromDetails.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries); // не понял почему Homepage и n/a порваты

            string[] stringArrFromEditForm = fromForm.AllDetails.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);



            void RemoveArrayElement(ref string[] array, int index)
            {
                string[] newArray = new string[array.Length - 1];

                for (int i = 0; i < index; i++)
                    newArray[i] = array[i];

                for (int i = index + 1; i < array.Length; i++)
                    newArray[i - 1] = array[i];

                array = newArray;
            }
            

            ArrayFieldCheck(stringArrFromEditForm);

            string[] ArrayFieldCheck(string[] str)
            {

                for (int i = 0; i < str.Length; i++) 
                {
                    if (str[i].Contains("H: " + $"{fromForm.FirstHomePhone}") && fromForm.FirstHomePhone == "")
                    {
                        RemoveArrayElement(ref str, i);
                    }

                }

                return stringArrFromEditForm = str; //возможно есть другие способы присвоить массив, который получен в методе >>> в исходный массив

                // вроде работает, но думаю стоит попробовать в методе удаления сделать return + досмотреть видос про удаление из массива
                // + описать действия в методе удаления 
            }




            //verification
            // Полученные данные об одном и том же отдельно взятом контакте сверяем в виде AllDetails
            Assert.AreEqual(fromDetails, fromForm.AllDetails);
        }

    }
}
