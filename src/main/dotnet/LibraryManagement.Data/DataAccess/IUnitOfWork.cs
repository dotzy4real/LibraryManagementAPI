using LibraryManagement.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LibraryManagement.Data.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits all changes
        /// </summary>
        int Commit();
        /// <summary>
        /// Commits all changes and return commited count
        /// </summary>
        Task<int> CommitAsync();
        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        void RejectChanges();
        /// <summary>
        /// Resolve the Db entity repository
        /// </summary>
        IRepository<T> Repository<T>() where T : class;
        /// <summary>
        /// Resolve the Db entity
        /// </summary>
        DbSet<T> Set<T>() where T : class;
    }
}