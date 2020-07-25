using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System.Linq;
using Better_Choices_1;
using System;
using Better_Choices_1.Analytics;

namespace Better_Choices_1
{


    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Habit>().Wait();
            _database.CreateTableAsync<Habit_Data>().Wait();
            _database.CreateTableAsync<Stash>().Wait();
        }

        public Task<List<Habit>> GetPeopleAsync()
        {
            
            return _database.Table<Habit>().ToListAsync();
            
        }
        public Task<List<Stash>> GetStashesAsync()
        {

            return _database.Table<Stash>().ToListAsync();

        }
        public List<string> GetStashesNames()
        {
            var stashes = this.GetStashesAsync().Result.ToList();
            return (from stash in stashes
                    select stash.Name).ToList();
        }
        public Task<List<Habit_Data>> GetJobsAsync()
        {

            return _database.Table<Habit_Data>().ToListAsync();

        }
        public double GetStashTarget(string name)
        {
            string sql = "select * from Stash";
            if (name != "") {
                sql = "select * from Stash where Name='" + name + "'"; 
            }
            var stashes = _database.QueryAsync<Stash>(sql).Result.ToList();
            return stashes.First().target_savings;
        }
        public int GetStashesID(string name)
        {
            string sql = "select * from Stash where Name='" + name + "'";
            var stashes = _database.QueryAsync<Stash>(sql).Result.ToList();
            return stashes.First().ID;
        }
        public List<Named_Habit_Data> GetNamedJobsAsync(string stash_="",
                                                        DateTime? start_date=null,
                                                        DateTime? end_date=null)
        {
            start_date = start_date ?? DateTime.MinValue;
            end_date = end_date ?? DateTime.MaxValue;
            string sql = "SELECT Name as name,Habit_Data.* FROM HABIT inner join Habit_data on habit.id=habit_data.job_id";
            var habits = _database.Table<Habit>().ToListAsync().Result.ToList();
            var dat_ = _database.Table<Habit_Data>().ToListAsync().Result.ToList();
            var resultList = (from habit in habits
                              join data_ in dat_
                              on habit.ID equals data_.Job_ID
                              select new Named_Habit_Data()
                              {
                                  Name = habit.Name,
                                  ID = data_.ID,
                                  date_run = data_.date_run,
                                  money_saved = data_.money_saved,
                                  money_stored = data_.money_stored,
                                  stash_to_use = data_.stash_to_use

                              }).ToList();
             
            var stashes = _database.Table<Stash>().ToListAsync().Result.ToList();
            if (stash_ != "")
            {
                stashes = stashes.Where(stash => stash.Name == stash_).ToList();
            }
            resultList = resultList.Where(habit => (end_date >= habit.date_run) && (habit.date_run >= start_date)).ToList();
            var resultList2 = (
                                from stash in stashes
                                join data_ in resultList
                                on stash.ID equals data_.stash_to_use
                                select new Named_Habit_Data {
                                    Name = data_.Name,
                                    ID = data_.ID,
                                    date_run = data_.date_run,
                                    money_saved = data_.money_saved,
                                    money_stored = data_.money_stored,
                                    stash_to_use = data_.stash_to_use,
                                    stash_name_to_use = stash.Name
                                }).ToList();


            return resultList2;
        }


        public double money_saved(string stash_ = "",
                                                        DateTime? start_date = null,
                                                        DateTime? end_date = null)
        {
            return (from habit_data in this.GetNamedJobsAsync(stash_,start_date,end_date)
                    select (habit_data.money_saved)).ToList().Sum();
        }
        public List<Better_Choices_1.Analytics.Date_Value> money_saved_per_date(String stash="",
                                                        DateTime? start_date = null,
                                                        DateTime? end_date = null)
        {

            var ls = this.GetNamedJobsAsync(stash,start_date,end_date);
            var targetlist = ls.OrderBy(l=>l.date_str).GroupBy(l => l.date_str).Select(cl => new Better_Choices_1.Analytics.Date_Value
            {
                Date = cl.First().date_str,
                value = cl.Sum(c => c.money_saved)
            }).ToList();
            
            double sum = 0;
            foreach (Date_Value dv in targetlist)
            {
                sum += dv.value;
                dv.value = sum;
            }
            
            return targetlist;
        }
        // public double money_saved_date()
        //{

        //}
        public Task<int> SaveItemAsync(Habit item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> SaveItemAsync(Stash item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> SaveItemAsync(Habit_Data item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> SavePersonAsync(Habit habit)
        {
            return _database.InsertAsync(habit);
        }
        public Task<int> SaveStashAsync(Stash habit)
        {
            return _database.InsertAsync(habit);
        }
        public Task<int> SaveJobAsync(Habit_Data habit)
        {
            return _database.InsertAsync(habit);
        }
        public Task<int> DeleteNamedHabitAsync(Named_Habit_Data habit_inst)
        {
            return _database.DeleteAsync(habit_inst.GetHabit_Data());
        }

        public Task<int> DeleteItemAsync(Named_Habit_Data item)
        {
            return this.DeleteItemAsync(item.GetHabit_Data());
        }
        public Task<int> DeleteItemAsync(Habit_Data item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<int> DeleteItemAsync(Stash item)
        {
            return _database.DeleteAsync(item);
        }
        public Task<int> DeleteItemAsync(Habit item)
        {
            return _database.DeleteAsync(item);
        }

        public Task<int> DeleteHabitAsync(Habit habit_inst)
        {
            return _database.DeleteAsync(habit_inst);
        }
        public int SearchAsync(string habit_name)
        {
            string sql = "SELECT ID FROM HABIT WHERE NAME ='{habit_name}'";
            return _database.QueryAsync<Habit>(sql).Result.ToArray()[0].ID;
        }
    }
}