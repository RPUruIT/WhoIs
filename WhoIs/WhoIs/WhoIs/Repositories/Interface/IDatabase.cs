using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Models;

namespace WhoIs.Repositories.Interface
{
    public interface IDatabase<T> where T : EntityBase
    {
        Task<int> Insert(T entity);

        Task<int> Delete(T entity);

        Task<T> Update(T item);

        Task<List<T>> GetAll();

        Task<T> GetFirst();

        Task<T> GetById(Guid id);

        Task<int> GetCount();
    }
}
