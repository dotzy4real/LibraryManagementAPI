using LibraryManagement.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Contracts
{
    public interface IBookReservationService
    {
        void AddBookReservation(BookReservation BookReservation);

        void UpdateBookReservation(BookReservation BookReservation);
        BookReservation GetById(int id);
        IEnumerable<BookReservation> GetAllBookReservations();
        IEnumerable<BookReservation>? GetAllByBookId(int bookId);
        IEnumerable<BookReservation>? GetAllByCustomerId(int customerId);
        BookReservation? FindReservedBorrowedBookByCustomerIdByBookId(int customerId, int bookId);
        BookReservation? FindReservedBookByBookId(int bookId);
        BookReservation? FindBookReservationForCustomer(int bookId, int CustomerId);
        void DeleteBookReservation(int id);
    }
}
