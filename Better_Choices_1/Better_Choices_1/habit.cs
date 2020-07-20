using SQLite;
using System;
using System.Data;
using System.Collections.Generic;
namespace Better_Choices_1
{
    public class Habit
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string frequency{ get; set; }
        public double how_common { get; set; }
        public DateTime date_started { get; set; }

        public DateTime date_ended { get; set; }

        public double money_saved { get; set; }

        // public double percent_stashed { get; set; }

        public int stash_to_use { get; set; }
    }
}