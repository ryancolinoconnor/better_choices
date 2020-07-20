using System;
using SQLite;

namespace Better_Choices_1
{
    public class Stash
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime date_started { get; set; }

        public DateTime target_date{ get; set; }

        public double target_savings{ get; set; }

    }
}
