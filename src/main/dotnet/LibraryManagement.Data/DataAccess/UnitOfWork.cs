using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext _context;
        private Hashtable? _repository;
        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repository == null)
                _repository = new Hashtable();
            var name = typeof(T).Name;
            if (!_repository.ContainsKey(name))
            {
                var type = typeof(Repository<>);
                var instance = Activator.CreateInstance(type.MakeGenericType(typeof(T)), _context);
                _repository.Add(name, instance);
            }
            return (IRepository<T>)_repository[name]!;
        }

        public virtual int Commit()
        {
            return _context.SaveChanges();
        }

        public virtual async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void RejectChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public DbSet<T> Set<T>() where T : class
        {
            return _context.GetEntities<T>();
        }
    }
}