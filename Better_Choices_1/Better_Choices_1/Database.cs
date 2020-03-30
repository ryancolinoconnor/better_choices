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
    }
}