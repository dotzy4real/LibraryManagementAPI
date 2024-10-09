using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure;
using LibraryManagement.Data.Entity;
using LibraryManagement.Services.Contracts;
using LibraryManagement.Services.Infrastructure;
using LibraryManagement.Services.Models.Requests;
using LibraryManagement.Services.Models.Response;

namespace LibraryManagement.Services.Utility
{
    public static class UtilityProcessor
    {
        public static LibraryApiResponse SuccessulResponse(object? result, string message = "Successful", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var response = new LibraryApiResponse();
            response.data = result;
            response.statusCode = statusCode;
            response.message = message;
            return response;
        }

        public static LibraryApiResponse FailResponse(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, object? result = null)
        {
            var response = new LibraryApiResponse();
            response.message = message;
            response.statusCode = statusCode;
            if (result != null)
                response.data = result;
            return response;
        }

        public static LibraryApiResponse? ValidateAddBookReservation(BookReservationRequest bookReservationRequest, IBookService bookService, ICustomerService customerService, IAvailableBookNotificationService availableBookNotificationService, IBookReservationService bookReservationService)
        {
            var response = new LibraryApiResponse();
            if (((bookReservationRequest.BookId ?? 0) == 0) || ((bookReservationRequest.CustomerId ?? 0) == 0))
            {
                response = FailResponse("Specifications for Book and Customer are compulsory", HttpStatusCode.BadRequest);
                return response;
            }
            var book = bookService.GetById(Convert.ToInt32(bookReservationRequest.BookId));
            if (book == null)
            {
                response = FailResponse("Book to be reserved does not exist", HttpStatusCode.BadRequest);
                return response;
            }
            var customer = customerService.GetById(Convert.ToInt32(bookReservationRequest.CustomerId));
            if (customer == null)
            {
                response = FailResponse("Customer specified for book reservation does not exist", HttpStatusCode.BadRequest);
                return response;
            }
            var bookReservation = bookReservationService.FindReservedBorrowedBookByCustomerIdByBookId(Convert.ToInt32(bookReservationRequest.CustomerId), Convert.ToInt32(bookReservationRequest.BookId));
            if (bookReservation != null)
            {
                response = FailResponse("Customer has already borrowed or reserved specified book", HttpStatusCode.BadRequest);
                return response;
            }
            if (!book.IsAvailable)
            {
                string notify = "";
                if (bookReservationRequest.RequestAvailabilityNotification)
                {
                    CreateAvailablilityNotification(bookReservationRequest, availableBookNotificationService);
                    notify = ", a notification will be sent once available";
                }
                var bookReservaton = bookReservationService.FindReservedBookByBookId(book.Id);
                var conflictResult = new BookAvailabilityResponse
                {
                    BookAvailableDate = bookReservaton!.EndDate
                };
                response = FailResponse(string.Format("Book to be reserved is not currently available{0}", notify), HttpStatusCode.Conflict, conflictResult);
                return response;
            }
            return null;
        }

        public static LibraryApiResponse? ValidateBorrowBookReservation(BookReservation? checkBookReservation, BorrowBookRequest bookReservationRequest, IBookService bookService, IBookReservationService bookReservationService)
        {
            var response = new LibraryApiResponse();
            if (checkBookReservation == null)
            {
                response = FailResponse("BookReservation does not exist, you must reserve a book before you can borrow it", HttpStatusCode.BadRequest);
                return response;
            }
            else if (checkBookReservation.StartDate != null && checkBookReservation.StartDate > DateTime.MinValue && checkBookReservation.EndDate != null && checkBookReservation.EndDate > DateTime.MinValue)
            {
                response = FailResponse("Book has been borrowed already for this Reservation", HttpStatusCode.BadRequest);
                return response;
            }
            else if (((bookReservationRequest.StartDate ?? DateTime.MinValue) == DateTime.MinValue) || ((bookReservationRequest.EndDate ?? DateTime.MinValue) == DateTime.MinValue))
            {
                response = FailResponse("Book Borrow Start Date and End Date are compulsory", HttpStatusCode.BadRequest);
                return response;
            }
            else if (bookReservationRequest.StartDate < DateTime.Now)
            {
                response = FailResponse("Start Date of book to be borrowed can't be earlier than current date", HttpStatusCode.BadRequest);
                return response;
            }
            else if (bookReservationRequest.StartDate >= bookReservationRequest.EndDate)
            {
                response = FailResponse("Start Date of book to be borrowed can't be later or equal to End Date", HttpStatusCode.BadRequest);
                return response;
            }
            else if (checkBookReservation.ReservedDate < DateTime.Now.AddDays(-1))
            {
                bookService.UpdateAvailableBook(checkBookReservation.BookId);
                response = FailResponse("The reservation period to borrow book as exceeded 24 hours", HttpStatusCode.BadRequest);
                return response;
            }

            return null;
        }

        public static BookReservation MapBookReservationRequestToEntity(BookReservationRequest bookRequest)
        {
            var bookReservation = new BookReservation
            {
                BookId = Convert.ToInt32(bookRequest.BookId),
                CustomerId = Convert.ToInt32(bookRequest.CustomerId),
                ReservedDate = DateTime.Now,
                RequestAvailabilityNotification = bookRequest.RequestAvailabilityNotification
            };
            return bookReservation;
        }

        public static void CreateAvailablilityNotification(BookReservationRequest bookReservation, IAvailableBookNotificationService availableBookNotificationService)
        {
            var existingAvailableBookNotification = availableBookNotificationService.GetByBookIdCustomerId(Convert.ToInt32(bookReservation.BookId), Convert.ToInt32(bookReservation.CustomerId));
            if (existingAvailableBookNotification == null)
            {
                var availableBookNotification = new AvailableBookNotification
                {
                    BookId = Convert.ToInt32(bookReservation.BookId),
                    CustomerId = Convert.ToInt32(bookReservation.CustomerId),
                    IsNotificationSent = false
                };
                availableBookNotificationService.AddAvailableBookNotification(availableBookNotification);
            }
        }

        public static LibraryApiResponse? ValidateReturnBook(ReturnBookRequest returnBook, IBookService bookService, ICustomerService customerService, IBookReservationService bookReservationService)
        {
            var response = new LibraryApiResponse();
            if (((returnBook.BookId ?? 0) == 0) || ((returnBook.CustomerId ?? 0) == 0))
            {
                response = FailResponse("Specifications for Book and Customer are compulsory", HttpStatusCode.BadRequest);
                return response;
            }
            var book = bookService.GetById(Convert.ToInt32(returnBook.BookId));
            if (book == null)
            {
                response = FailResponse("Book to be returned does not exist", HttpStatusCode.BadRequest);
                return response;
            }
            var customer = customerService.GetById(Convert.ToInt32(returnBook.CustomerId));
            if (customer == null)
            {
                response = FailResponse("Customer specified for book return does not exist", HttpStatusCode.BadRequest);
                return response;
            }
            var customerBookReservation = bookReservationService.FindBookReservationForCustomer(Convert.ToInt32(returnBook.BookId), Convert.ToInt32(returnBook.CustomerId));
            if (customerBookReservation == null)
            {
                response = FailResponse("Either Customer specified has not borrowed book yet or has returned book already", HttpStatusCode.BadRequest);
                return response;
            }
            if (book.IsAvailable)
            {
                response = FailResponse("Book to be returned is available already", HttpStatusCode.BadRequest);
                return response;
            }
            return null;
        }

        public static Customer MapCustomerRequestToCustomer(CustomerRequest request)
        {
            return new Customer
            {
                FirstName = request.FirstName!,
                LastName = request.LastName!,
                Email = request.Email!,
                Address = request.Address!,
                PhoneNumber = request.PhoneNumber!
            };
        }

        public static Book MapBookRequestToBook(BookRequest request)
        {
            return new Book
            {
                Title = request.Title!,
                Author = request.Author!,
                Description = request.Description!
            };
        }
    }
}
