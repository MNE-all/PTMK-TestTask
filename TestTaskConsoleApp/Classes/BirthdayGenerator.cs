using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskConsoleApp.Classes
{
    /// <summary>
    /// Генератор дат рождения совершеннолетних людей
    /// </summary>
    public class BirthdayGenerator
    {
        DateTime startDate = DateTime.Now.AddYears(-100);
        DateTime endDate = DateTime.Now.AddYears(-18);
        Random random = new Random();

        /// <summary>
        /// Получить дату
        /// </summary>
        public DateTime Make()
        {
            var randomYear = random.Next(startDate.Year, endDate.Year);
            var randomMonth = random.Next(1, 12);
            var randomDay = random.Next(1, DateTime.DaysInMonth(randomYear, randomMonth));

            return new DateTime(randomYear, randomMonth, randomDay);
        }

    }
}
