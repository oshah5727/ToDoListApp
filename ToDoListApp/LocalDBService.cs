using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp
{
    class LocalDBService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;
        public LocalDBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<TDItem>();
        }
    }
}

