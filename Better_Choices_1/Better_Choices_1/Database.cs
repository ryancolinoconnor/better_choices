using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Better_Choices_1;

namespace Better_Choices_1
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Habit>().Wait();
        }

        public Task<List<Habit>> GetPeopleAsync()
        {
            return _database.Table<Habit>().ToListAsync();
        }

        public Task<int> SavePersonAsync(Habit habit)
        {
            return _database.InsertAsync(habit);
        }
        public Task<int> DeleteHabitAsync(int habit_it)
        {
            return _database.DeleteAsync(habit_it);
        }
        public int SearchAsync(string habit_name)
        {
            string sql = "SELECT ID FROM HABIT WHERE NAME ='{habit_name}'";
            return _database.QueryAsync<Habit>(sql).Result.ToArray()[0].ID;
        }
    }
}