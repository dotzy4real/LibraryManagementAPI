using LibraryManagement.Data.Entity;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Services.Models.Requests;
using LibraryManagement.Services.Models.Response;
using LibraryManagement.Services.Utility;
using System.Net;
using Azure;
using LibraryManagement.Services.Infrastructure;
using Microsoft.AspNetCore.Authorization;


namespace LibraryManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICustomerService _customerService;
        private readonly IBookReservationService _bookReservationService;
        public BookController(IBookService bookService, ICustomerService customerService, IBookReservationService bookReservationService)
        {
            _bookService = bookService;
            _customerService = customerService;
            _bookReservationService = bookReservationService;
        }

        // GET: api/Book/Details/5
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBook(id);
                var book = _bookService.GetById(id);
                if (book == null)
                {
                    response = UtilityProcessor.FailResponse("Book to be retrieved does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    response = UtilityProcessor.SuccessulResponse(book);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/Book/List
        [HttpPost("List")]
        public ActionResult List()
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBooks();
                var books = _bookService.GetAllBooks();
                response = UtilityProcessor.SuccessulResponse(books);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/Book/SearchByBookTitle => This uses a wild card expression to look for relevant book title based on search criteria
        [HttpPost("SearchByBookTitle")]
        public ActionResult Search([FromBody] BookFilterRequest bookRequest)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBooks();
                var books = _bookService.SearchBooks(bookRequest.BookTitle);
                response = UtilityProcessor.SuccessulResponse(books);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/Book/FindByBookTitle => This looks for exact match of the book title based on filter criteria
        [HttpPost("FindByBookTitle")]
        public ActionResult Find([FromBody] BookFilterRequest bookRequest)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBooks();
                var books = _bookService.FindBooks(bookRequest.BookTitle);
                response = UtilityProcessor.SuccessulResponse(books);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // GET: api/AvailableBookNotification/Details/5
        [HttpGet("GetAllBookReservationsByBookId/{id}")]
        public ActionResult GetAllBookReservationsByBook(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBooks();
                var bookReservations = _bookReservationService.GetAllByBookId(id);
                if (bookReservations == null)
                {
                    response = UtilityProcessor.FailResponse("Book Reservations for specified book does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    response = UtilityProcessor.SuccessulResponse(bookReservations);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/Book/Create
        [HttpPost("Create")]
        public ActionResult Create([FromBody] BookRequest bookRequest)
        {
            var response = new LibraryApiResponse();
            try
            {
                var book = UtilityProcessor.MapBookRequestToBook(bookRequest);
                if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author) || string.IsNullOrEmpty(book.Description))
                {
                    response = UtilityProcessor.FailResponse("Book Title, Author and Description must not be empty", HttpStatusCode.BadGateway);
                    return BadRequest(response);
                }
                _bookService.AddBook(book);
                response = UtilityProcessor.SuccessulResponse(book);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // PUT: api/Book/Edit/5
        [HttpPut("Edit/{id}")]
        public ActionResult Edit(int id, BookRequest book)
        {
            var response = new LibraryApiResponse();
            try
            {
                var checkBook = _bookService.GetById(id);
                if (checkBook == null)
                {
                    response = UtilityProcessor.FailResponse("Book to be updated does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    checkBook.Author = book.Author ?? checkBook.Author;
                    checkBook.Title = book.Title ?? checkBook.Title;
                    checkBook.Description = book.Description ?? checkBook.Description;
                    _bookService.UpdateBook(checkBook);
                    response = UtilityProcessor.SuccessulResponse(checkBook);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // DELETE: api/Book/Delete/5
        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                var checkBook = _bookService.GetById(id);
                if (checkBook == null)
                {
                    response = UtilityProcessor.FailResponse("Book to be deleted does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    _bookService.DeleteBook(id);
                    response = UtilityProcessor.SuccessulResponse(null);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }
    }
}
