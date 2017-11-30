using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;
using WhoIs.Repositories.Interface;
using Unity;

namespace WhoIs.Repositories
{
    public class Database<T> : IDatabase<T> where T : EntityBase, new()
    {
        private SQLiteAsyncConnection _database;

        public Database(string path)
        {
            var helper = DependencyContainer.Container.Resolve<IConnectionHelper>();
            _database = helper.GetConnection(path);
            _database.CreateTableAsync<T>();
        }

        public async Task<int> Insert(T entity)
        {
            return await _database.InsertAsync(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await _database.DeleteAsync(entity);
        }

        public async Task<T> Update(T item)
        {
            var old = await GetFirst();
            var id = old.Id;
            old = item;
            old.Id = id;
            await _database.UpdateAsync(old);
            return old;
        }

        public async Task<List<T>> GetAll()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<T> GetFirst()
        {
            return await _database.Table<T>().FirstOrDefaultAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _database.GetAsync<T>(id);
        }

        public async Task<int> GetCount()
        {
            return await _database.Table<T>().CountAsync();
        }
    }
}

