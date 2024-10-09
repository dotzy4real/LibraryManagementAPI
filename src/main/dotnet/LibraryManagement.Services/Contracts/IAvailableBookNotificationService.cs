using LibraryManagement.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Contracts
{
    public interface IAvailableBookNotificationService
    {
        void AddAvailableBookNotification(AvailableBookNotification AvailableBookNotification);

        void UpdateAvailableBookNotification(AvailableBookNotification AvailableBookNotification);
        AvailableBookNotification GetById(int id);
        IList<AvailableBookNotification>? GetByBookId(int id);
        AvailableBookNotification? GetByBookIdCustomerId(int bookId, int customerId);
        IList<AvailableBookNotification>? GetAllByCustomerId(int customerId);
        IEnumerable<AvailableBookNotification> GetAllAvailableBookNotifications();
        void DeleteAvailableBookNotification(int id);
    }
}
