using LibraryManagement.Data.DataAccess;
using LibraryManagement.Data.Entity;
using LibraryManagement.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace LibraryManagement.Services.Infrastructure
{
    public class AvailableBookNotificationService : IAvailableBookNotificationService
    {
        private readonly IRepository<AvailableBookNotification> _availableBookNotification;
        private readonly IRepository<Book> _book;
        private readonly IRepository<Customer> _customer;
        private readonly IUnitOfWork _unitOfWork;
        public AvailableBookNotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _availableBookNotification = unitOfWork.Repository<AvailableBookNotification>();
            _book = unitOfWork.Repository<Book>();
            _customer = unitOfWork.Repository<Customer>();
        }

        public void AddAvailableBookNotification(AvailableBookNotification availableBookNotification)
        {
            _availableBookNotification.Insert(availableBookNotification);
            _unitOfWork.Commit();
        }

        public void UpdateAvailableBookNotification(AvailableBookNotification availableBookNotification)
        {
            _availableBookNotification.Update(availableBookNotification);
            _unitOfWork.Commit();
        }

        public AvailableBookNotification GetById(int id)
        {
            var availableBookNotification = _availableBookNotification.GetById(id);
            return availableBookNotification;
        }

        public IList<AvailableBookNotification>? GetByBookId(int id)
        {
            var query = from bk in _book.Table()
                        join ab in _availableBookNotification.Table()
                        on bk.Id equals ab.BookId
                        where ab.BookId == id 
                        && ab.IsNotificationSent == false
                        orderby ab.Id descending
                        select ab;

            return query.ToList();
        }

        public IList<AvailableBookNotification>? GetAllByCustomerId(int customerId)
        {
            var query = from cs in _customer.Table()
                        join ab in _availableBookNotification.Table()
                        on cs.Id equals ab.CustomerId
                        where ab.CustomerId == customerId
                        orderby ab.Id descending
                        select ab;

            return query.ToList();
        }

        public AvailableBookNotification? GetByBookIdCustomerId(int bookId, int customerId)
        {
            var query = from bk in _book.Table()
                        join ab in _availableBookNotification.Table()
                        on bk.Id equals ab.BookId
                        where ab.BookId == bookId
                        && ab.CustomerId == customerId
                        && ab.IsNotificationSent == false
                        orderby ab.Id descending
                        select ab;

            return query.FirstOrDefault();
        }

        public IEnumerable<AvailableBookNotification> GetAllAvailableBookNotifications()
        {
            var availableBookNotifications = _availableBookNotification.Get(null, x => x.OrderBy(y => y.Id));
            return availableBookNotifications;
        }

        public void DeleteAvailableBookNotification(int id)
        {
            _availableBookNotification.Delete(id);
            _unitOfWork.Commit();
        }
    }
}
