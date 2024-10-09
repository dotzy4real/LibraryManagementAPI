
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataAccess
{
    public interface IDbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbSet<T> GetEntities<T>() where T : class;
        void Dispose();
        ChangeTracker ChangeTracker { get; }
    }
}
