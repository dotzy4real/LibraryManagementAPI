using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Data.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private DbContext _entities;

        public DbContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public Repository(DbContext context)
        {
            _entities = context;
        }

        public virtual IQueryable<T> Table()
        {
            IQueryable<T> table = _entities.Set<T>();
            return table;
        }


        public virtual IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = _entities.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            else
            {
                return query.ToList();
            }
        }


        public virtual bool Exists(System.Linq.Expressions.Expression<Func<T, bool>> filter, bool includeDeleted)
        {
            IQueryable<T> query = _entities.Set<T>();
            bool any = query.Where(filter).Any();
            return any;
        }

        public virtual T GetBy(System.Linq.Expressions.Expression<Func<T, bool>> filter, string includeProperties)
        {
            IQueryable<T> query = _entities.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault();
        }

        public T GetById(int id)
        {
            T result = _entities.Set<T>().Find(id);
            return result;
        }


        public void Update(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public void Insert(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public void Delete(int id)
        {
            _entities.Set<T>().Remove(GetById(id));
        }

        public void Save()
        {
            _entities.SaveChanges();
        }

    }
}