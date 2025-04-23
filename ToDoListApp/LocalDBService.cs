using SQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public async Task<List<TDItem>> GetTDItemsAsync()
        { return await _connection.Table<TDItem>().ToListAsync(); }

        public async Task Create(TDItem tditem)
        { await _connection.InsertAsync(tditem); }

        public async Task Update(TDItem tditem)
        { await _connection.UpdateAsync(tditem); }

        public async Task Delete(TDItem tditem)
        { await _connection.DeleteAsync(tditem); }
    }
}

