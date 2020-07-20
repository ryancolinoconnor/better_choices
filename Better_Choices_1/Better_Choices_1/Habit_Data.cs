using SQLite;
using System;
namespace Better_Choices_1
{
    public class Habit_Data
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int Job_ID { get; set; }

        //public int Stash_ID { get; set; }
        // private Habit Job { get; set; }
        public DateTime date_run { get; set; }
        public double money_saved { get; set; }
        public double money_stored { get; set; }
        public int stash_to_use { get; set; }

    }
    public class Named_Habit_Data: Habit_Data
    {
        public string Name { get; set; }
        public int ID { get; set; }
        
        public string stash_name_to_use { get; set; }
        public string date_str
        {
            get
            {
                return this.date_run.ToString("MMM dd, yyyy");
            }
        }
        public Habit_Data GetHabit_Data()
        {
            return new Habit_Data
            {
                ID = this.ID,
                Job_ID = this.Job_ID,
                date_run = this.date_run,
                money_saved = this.money_saved,
                money_stored = this.money_stored,
            };
        }
    }
}