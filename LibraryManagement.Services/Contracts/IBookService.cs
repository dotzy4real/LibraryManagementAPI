using LibraryManagement.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Contracts
{
    public interface IBookService
    {
        void AddBook(Book Book);

        void UpdateBook(Book Book);
        Book GetById(int id);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> SearchBooks(string title);
        IEnumerable<Book> FindBooks(string title);
        Book ReturnBook(int bookId, int customerId);
        void UpdateAvailableBook(int bookId);
        void UpdateAvailableBooks();
        void DeleteBook(int id);
    }
}
