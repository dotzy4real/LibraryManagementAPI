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
    public class BookReservationController : ControllerBase
    {
        private readonly IBookReservationService _bookReservationService;
        private readonly IBookService _bookService;
        private readonly IAvailableBookNotificationService _availableBookNotificationService;
        private readonly ICustomerService _customerService;
        public BookReservationController(IBookReservationService bookReservationService, IBookService bookService, IAvailableBookNotificationService availableBookNotificationService, ICustomerService customerService)
        {
            _bookReservationService = bookReservationService;
            _bookService = bookService;
            _availableBookNotificationService = availableBookNotificationService;
            _customerService = customerService;
        }

        // GET: api/BookReservation/Details/5
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBook(id);
                var bookReservation = _bookReservationService.GetById(id);
                if (bookReservation == null)
                {
                    response = UtilityProcessor.FailResponse("BookReservation to be retrieved does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    response = UtilityProcessor.SuccessulResponse(bookReservation);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/BookReservation/List
        [HttpPost("List")]
        public ActionResult List()
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBooks();
                var bookReservations = _bookReservationService.GetAllBookReservations();
                response = UtilityProcessor.SuccessulResponse(bookReservations);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/BookReservation/ReserveBook
        [HttpPost("ReserveBook")]
        public ActionResult Create([FromBody] BookReservationRequest bookReservationRequest)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBook(Convert.ToInt32(bookReservationRequest.BookId));
                var validationResponse = UtilityProcessor.ValidateAddBookReservation(bookReservationRequest, _bookService, _customerService, _availableBookNotificationService, _bookReservationService);
                if (validationResponse != null)
                {
                    return BadRequest(validationResponse);
                }
                var bookReservation = UtilityProcessor.MapBookReservationRequestToEntity(bookReservationRequest);
                _bookReservationService.AddBookReservation(bookReservation);
                response = UtilityProcessor.SuccessulResponse(bookReservation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // PUT: api/BookReservation/BorrowBook/5 => You must have reserved a book before it can be borrowed
        [HttpPut("BorrowBook/{id}")]
        public ActionResult Edit(int id, [FromBody] BorrowBookRequest bookReservation)
        {
            var response = new LibraryApiResponse();
            try
            {
                var checkBookReservation = _bookReservationService.GetById(id);
                _bookService.UpdateAvailableBook(Convert.ToInt32(checkBookReservation?.BookId));
                var validationResponse = UtilityProcessor.ValidateBorrowBookReservation(checkBookReservation, bookReservation, _bookService, _bookReservationService);
                if (validationResponse != null)
                {
                    return BadRequest(validationResponse);
                }
                checkBookReservation!.StartDate = Convert.ToDateTime(bookReservation.StartDate);
                checkBookReservation.EndDate = Convert.ToDateTime(bookReservation.EndDate);
                _bookReservationService.UpdateBookReservation(checkBookReservation);
                response = UtilityProcessor.SuccessulResponse(checkBookReservation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }


        // POST: api/Book/ReturnBook
        [HttpPost("ReturnBook")]
        public ActionResult ReturnBook([FromBody] ReturnBookRequest returnBook)
        {
            var response = new LibraryApiResponse();
            try
            {
                var validationResponse = UtilityProcessor.ValidateReturnBook(returnBook, _bookService, _customerService, _bookReservationService);
                if (validationResponse != null)
                {
                    return BadRequest(validationResponse);
                }
                var book = _bookService.ReturnBook(Convert.ToInt32(returnBook.BookId), Convert.ToInt32(returnBook.CustomerId));
                response = UtilityProcessor.SuccessulResponse(book);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // DELETE: api/BookReservation/Delete/5
        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                var checkBookReservation = _bookReservationService.GetById(id);
                if (checkBookReservation == null)
                {
                    response = UtilityProcessor.FailResponse("BookReservation to be deleted does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    _bookReservationService.DeleteBookReservation(id);
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
