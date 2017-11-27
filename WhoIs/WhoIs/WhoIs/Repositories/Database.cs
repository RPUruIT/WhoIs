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
        private SQLiteConnection _database;

        public Database(string path)
        {
            var helper = DependencyContainer.Container.Resolve<IConnectionHelper>();
            _database = helper.GetConnection(path);
            _database.CreateTable<T>();
        }

        public async Task<int> Insert(T entity)
        {
            return await Task.Run(() => _database.Insert(entity));
        }

        public async Task<int> Delete(T entity)
        {
            return await Task.Run(() => _database.Delete(entity));
        }

        public async Task<T> Update(T item)
        {
            var old = await GetFirst();
            var id = old.Id;
            old = item;
            old.Id = id;
            await Task.Run(() => _database.Update(old));
            return old;
        }

        public async Task<List<T>> GetAll()
        {
            return await Task.Run(() => _database.Table<T>().ToList());
        }

        public async Task<T> GetFirst()
        {
            var exec = _database.Table<T>().FirstOrDefault();
            return await Task.Run(() => exec);
        }

        public async Task<T> GetById(Guid id)
        {
            var exec = _database.Get<T>(id);
            return await Task.Run(() => exec);
        }
    }
}

