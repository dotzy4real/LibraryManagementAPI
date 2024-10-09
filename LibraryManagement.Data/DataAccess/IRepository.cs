using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null);

        IQueryable<T> Table();

        bool Exists(Expression<Func<T, bool>> filter, bool includeDeleted);

        T GetBy(Expression<Func<T, bool>> filter, string includeProperties);

        T GetById(int id);

        void Update(T record);

        void Insert(T record);

        void Delete(int id);

        void Save();
    }
}
