using LibraryManagement.Data.DataAccess;
using LibraryManagement.Data.Entity;
using LibraryManagement.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Infrastructure
{
    public class BookReservationService : IBookReservationService
    {
        private readonly IRepository<BookReservation> _bookReservation;
        private readonly IRepository<Book> _book;
        private readonly IRepository<Customer> _customer;
        private readonly IUnitOfWork _unitOfWork;
        public BookReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookReservation = unitOfWork.Repository<BookReservation>();
            _book = unitOfWork.Repository<Book>();
            _customer = unitOfWork.Repository<Customer>();
        }

        public void AddBookReservation(BookReservation bookReservation)
        {
            _bookReservation.Insert(bookReservation);
            var query = from bk in _book.Table()
                        where bk.Id == bookReservation.BookId
                        && bk.IsAvailable == true
                        select bk;
            var book = query.FirstOrDefault();
            if (book != null)
            {
                book.IsAvailable = false;
                _book.Update(book);
            }
            _unitOfWork.Commit();
        }

        public void UpdateBookReservation(BookReservation bookReservation)
        {
            _bookReservation.Update(bookReservation);
            _unitOfWork.Commit();
        }

        public BookReservation GetById(int id)
        {
            var bookReservation = _bookReservation.GetById(id);
            return bookReservation;
        }

        public IEnumerable<BookReservation> GetAllBookReservations()
        {
            var bookReservations = _bookReservation.Get(null, x => x.OrderBy(y => y.Id));
            return bookReservations;
        }

        public IEnumerable<BookReservation>? GetAllByBookId(int bookId)
        {
            var query = from br in _bookReservation.Table()
                        join bk in _book.Table()
                        on br.BookId equals bk.Id
                        where br.BookId == bookId
                        select br;
            return query.ToList();
        }

        public BookReservation? FindReservedBookByBookId(int bookId)
        {
            var query = from br in _bookReservation.Table()
                        join bk in _book.Table()
                        on br.BookId equals bk.Id
                        where br.BookId == bookId
                        && bk.IsAvailable == false
                        && br.ReservationStatus == true
                        select br;
            return query.FirstOrDefault();
        }

        public BookReservation? FindReservedBorrowedBookByCustomerIdByBookId(int customerId, int bookId)
        {
            var query = from br in _bookReservation.Table()
                        join bk in _book.Table()
                        on br.BookId equals bk.Id
                        where br.CustomerId == customerId
                        && br.BookId == bookId
                        && bk.IsAvailable == false
                        && br.ReservationStatus == true
                        select br;
            return query.FirstOrDefault();
        }

        public IEnumerable<BookReservation>? GetAllByCustomerId(int customerId)
        {
            var query = from br in _bookReservation.Table()
                        join cs in _customer.Table()
                        on br.CustomerId equals cs.Id
                        where br.CustomerId == customerId
                        select br;
            return query.ToList();
        }

        public BookReservation? FindBookReservationForCustomer(int bookId, int CustomerId)
        {
            var query = from br in _bookReservation.Table()
                        join cs in _customer.Table()
                        on br.CustomerId equals cs.Id
                        where br.BookId == bookId
                        && br.CustomerId == CustomerId
                        && br.EndDate != null
                        && br.ReservationStatus == true
                        select br;
            return query.FirstOrDefault();
        }

        public void DeleteBookReservation(int id)
        {
            _bookReservation.Delete(id);
            _unitOfWork.Commit();
        }
    }
}
