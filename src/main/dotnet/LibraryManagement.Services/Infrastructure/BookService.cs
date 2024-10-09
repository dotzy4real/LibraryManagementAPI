using LibraryManagement.Data.DataAccess;
using LibraryManagement.Data.Entity;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Infrastructure
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _book;
        private readonly IRepository<BookReservation> _bookReservation;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAvailableBookNotificationService _availableBookNotificationService;
        private readonly IBookReservationService _bookReservationService;
        public BookService(IUnitOfWork unitOfWork, IAvailableBookNotificationService availableBookNotificationService, IBookReservationService bookReservation)
        {
            _unitOfWork = unitOfWork;
            _book = unitOfWork.Repository<Book>();
            _bookReservation = unitOfWork.Repository<BookReservation>();
            _availableBookNotificationService = availableBookNotificationService;
            _bookReservationService = bookReservation;
        }

        public void AddBook(Book book)
        {
            _book.Insert(book);
            _unitOfWork.Commit();
        }

        public void UpdateBook(Book book)
        {
            _book.Update(book);
            _unitOfWork.Commit();
        }

        public Book GetById(int id)
        {
            var book = _book.GetById(id);
            return book;
        }

        public IEnumerable<Book> SearchBooks(string title)
        {
            var books = _book.Get(null, x => x.OrderBy(y => y.Id)).Where(y => y.Title.ToLower().Contains(title.ToLower()));
            return books;
        }

        public IEnumerable<Book> FindBooks(string title)
        {
            var books = _book.Get(null, x => x.OrderBy(y => y.Id)).Where(y => y.Title.ToLower() == title.ToLower());
            return books;
        }
        public Book ReturnBook(int bookId, int customerId)
        {
            var book = _book.Table().Where(x => x.Id == bookId).FirstOrDefault();
            book!.IsAvailable = true;
            _book.Update(book);

            var bookReservation = _bookReservation.Table().Where(x => x.BookId == bookId && x.CustomerId == customerId).FirstOrDefault();
            bookReservation!.ReturnDate = DateTime.Now;
            bookReservation!.ReservationStatus = false;
            _bookReservation.Update(bookReservation);
            if (bookReservation.RequestAvailabilityNotification)
            {
                var availableBookNotification = _availableBookNotificationService.GetByBookIdCustomerId(bookReservation.BookId, bookReservation.CustomerId);
                availableBookNotification!.NotificationDate = DateTime.Now;
                availableBookNotification.IsNotificationSent = true; // we assume notification is being sent to customer here either by email/sms
                _availableBookNotificationService.UpdateAvailableBookNotification(availableBookNotification);
            }
            _unitOfWork.Commit();
            return book;
        }

        // This function only returns a book back to its available status for a customer that reserved the book BUT didn't borrow it
        public void UpdateAvailableBook(int bookId)
        {
            var query = from bk in _book.Table()
                        join br in _bookReservation.Table()
                        on bk.Id equals br.BookId
                        where br.ReservedDate < DateTime.Now.AddDays(-1)
                        && br.EndDate == null
                        && bk.Id == bookId
                        && bk.IsAvailable == false
                        && br.ReservationStatus == true
                        select bk;
            var book = query.FirstOrDefault();
            if (book != null)
            {
                book.IsAvailable = true;
                var bookReservation = _bookReservationService.FindReservedBookByBookId(book.Id);
                bookReservation!.ReservationStatus = false;
                _bookReservationService.UpdateBookReservation(bookReservation);
                var bookNotifications = _availableBookNotificationService.GetByBookId(book.Id);
                if (bookNotifications != null)
                {
                    foreach (var bookNotification in bookNotifications)
                    {
                        // This is where we notify customers that the book is available, we are only updating this table that the book is available and notification has been sent, other possible actions here could be sending of emails/sms to customers that the book is available.
                        bookNotification.NotificationDate = DateTime.Now;
                        bookNotification.IsNotificationSent = true; // we will assume actions like sending of sms or emails has happened here.
                        _availableBookNotificationService.UpdateAvailableBookNotification(bookNotification);
                    }
                }
                _book.Update(book);
                _bookReservation.Update(bookReservation);
                _unitOfWork.Commit();
            }
        }

        // This function only return books back to their available status for customers that reserved books BUT didn't borrow them
        public void UpdateAvailableBooks()
        {
            var query = from bk in _book.Table()
                        join br in _bookReservation.Table()
                        on bk.Id equals br.BookId
                        where br.ReservedDate < DateTime.Now.AddDays(-1)
                        && br.EndDate == null
                        && bk.IsAvailable == false
                        && br.ReservationStatus == true
                        select bk;
            var filterResult = query.ToList();
            foreach (var item in filterResult)
            {
                item.IsAvailable = true;
                var bookReservation = _bookReservationService.FindReservedBookByBookId(item.Id);
                bookReservation!.ReservationStatus = false;
                _bookReservationService.UpdateBookReservation(bookReservation);
                var bookNotifications = _availableBookNotificationService.GetByBookId(item.Id);
                if (bookNotifications != null)
                {
                    foreach (var bookNotification in bookNotifications)
                    {
                        // This is where we notify customers that the book is available, we are only updating this table that the book is available and notification has been sent, other possible actions here could be sending of emails/sms to customers that the book is available.
                        bookNotification.NotificationDate = DateTime.Now;
                        bookNotification.IsNotificationSent = true; // we will assume actions like sending of sms or emails has happened here.
                        _availableBookNotificationService.UpdateAvailableBookNotification(bookNotification);
                    }
                }
                _bookReservation.Update(bookReservation);
                _book.Update(item);
            }
            _unitOfWork.Commit();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var books = _book.Get(null, x => x.OrderBy(y => y.Id));
            return books;
        }

        public void DeleteBook(int id)
        {
            _book.Delete(id);
            _unitOfWork.Commit();
        }
    }
}
