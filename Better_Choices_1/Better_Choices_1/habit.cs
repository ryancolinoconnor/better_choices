using SQLite;
using System;
namespace Better_Choices_1
{
    public class Habit
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime date_started { get; set; }
        public double money_saved { get; set; }

    }
}