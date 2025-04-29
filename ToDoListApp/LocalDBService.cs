using SQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ToDoListApp.Models;

namespace ToDoListApp
{
    class LocalDBService
    {
        private const string DB_NAME = "TDListDatabase.db3";
        private readonly SQLiteAsyncConnection _connection;
        public LocalDBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<TDItem>();
            _connection.CreateTableAsync<User>();
        }

        public async Task<List<TDItem>> GetTDItemsAsync()
        { 
            return Task.Run(() => _connection.Table<TDItem>().ToListAsync()).Result;
            //return await _connection.Table<TDItem>().ToListAsync(); 
        }

        public async Task Create(TDItem tditem)
        { 
            await _connection.InsertAsync(tditem); 
        }

        public async Task Update(TDItem tditem)
        {
            await _connection.UpdateAsync(tditem);
        }
        public async Task<User> GetUserAsync()
        {
            return await _connection.Table<User>().FirstOrDefaultAsync(); 
        }

        public async Task SaveUserAsync(User user)
        {
            if (user.ID != 0)
                await _connection.UpdateAsync(user);
            else
                await _connection.InsertAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _connection.DeleteAsync(user);
        }
    }

}

