using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestSchedule.Model
{
    public class FacultyGroups
    {
        // Создание словаря с ключами типа string и значениями типа int
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public FacultyGroups()
        {
            dictionary.Add("0", "Инженерно - физический факультет");
            dictionary.Add("1", "Институт искусств");
            dictionary.Add("2", "Институт физической культуры и дзюдо");
            dictionary.Add("3", "Международный факультет");
            dictionary.Add("4", "Исторический факультет");
            dictionary.Add("5", "Факультет адыгейской филологии и культуры");
            dictionary.Add("6", "Факультет естествознания");
            dictionary.Add("7", "Факультет иностранных языков");
            dictionary.Add("8", "Факультет математики и компьютерных наук");
            dictionary.Add("9", "Факультет педагогики и психологии");
            dictionary.Add("10", "Факультет социальных технологий и туризма");
            dictionary.Add("11", "Экономический факультет");
            dictionary.Add("12", "Филологический факультет");
            dictionary.Add("13", "Юридический факультет");
        }

        public string GetFaculty(string key)
        {
            return dictionary[key];
        }
    }
}
