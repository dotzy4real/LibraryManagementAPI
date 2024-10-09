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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAvailableBookNotificationService _availableBookNotificationService;
        private readonly IBookReservationService _bookReservationService;
        private readonly IBookService _bookService;
        public CustomerController(ICustomerService customerService, IAvailableBookNotificationService availableBookNotificationService,
            IBookReservationService bookReservationService, IBookService bookService)
        {
            _customerService = customerService;
            _availableBookNotificationService = availableBookNotificationService;
            _bookReservationService = bookReservationService;
            _bookService = bookService;
        }

        // GET: api/Customer/Details/5
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                var customer = _customerService.GetById(id);
                if (customer == null)
                {
                    response = UtilityProcessor.FailResponse("Customer to be retrieved does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    response = UtilityProcessor.SuccessulResponse(customer);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/Customer/SearchCustomer => This uses a wild card expression to search for customer's first name and last name that matches search criteria
        [HttpPost("SearchCustomer")]
        public ActionResult Search([FromBody] CustomerFilterRequest customerFilter)
        {
            var response = new LibraryApiResponse();
            try
            {
                var customers = _customerService.SearchCustomers(customerFilter.Name);
                response = UtilityProcessor.SuccessulResponse(customers);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // GET: api/AvailableBookNotification/Details/5
        [HttpGet("GetAllBookNotificationsByCustomerId/{id}")]
        public ActionResult GetAllNotificationsByCustomer(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBooks();
                var availableBookNotifications = _availableBookNotificationService.GetAllByCustomerId(id);
                if (availableBookNotifications == null)
                {
                    response = UtilityProcessor.FailResponse("Available Book Notifications for specified customer does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    response = UtilityProcessor.SuccessulResponse(availableBookNotifications);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // GET: api/AvailableBookNotification/Details/5
        [HttpGet("GetAllBookReservationsByCustomerId/{id}")]
        public ActionResult GetAllBookReservationsByCustomer(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBooks();
                var bookReservations = _bookReservationService.GetAllByCustomerId(id);
                if (bookReservations == null)
                {
                    response = UtilityProcessor.FailResponse("Book Reservations for specified customer does not exist", HttpStatusCode.BadRequest);
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

        // POST: api/Customer/List
        [HttpPost("List")]
        public ActionResult List()
        {
            var response = new LibraryApiResponse();
            try
            {
                var customers = _customerService.GetAllCustomers();
                response = UtilityProcessor.SuccessulResponse(customers);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/Customer/Create
        [HttpPost("Create")]
        public ActionResult Create([FromBody] CustomerRequest customerRequest)
        {
            var response = new LibraryApiResponse();
            try
            {
                var customer = UtilityProcessor.MapCustomerRequestToCustomer(customerRequest);

                if (string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName) || string.IsNullOrEmpty(customer.PhoneNumber) || string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Address))
                {
                    response = UtilityProcessor.FailResponse("Customer First Name, Last Name, Phone Number, Email and Address must not be empty", HttpStatusCode.BadGateway);
                    return BadRequest(response);
                }
                _customerService.AddCustomer(customer);
                response = UtilityProcessor.SuccessulResponse(customer);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // PUT: api/Customer/Edit/5
        [HttpPut("Edit/{id}")]
        public ActionResult Edit(int id, CustomerRequest customer)
        {
            var response = new LibraryApiResponse();
            try
            {
                var checkCustomer = _customerService.GetById(id);
                if (checkCustomer == null)
                {
                    response = UtilityProcessor.FailResponse("Customer to be updated does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    checkCustomer.FirstName = customer.FirstName ?? checkCustomer.FirstName;
                    checkCustomer.LastName = customer.LastName ?? checkCustomer.LastName;
                    checkCustomer.Address = customer.Address ?? checkCustomer.Address;
                    checkCustomer.PhoneNumber = customer.PhoneNumber ?? checkCustomer.PhoneNumber;
                    checkCustomer.Email = customer.Email ?? checkCustomer.Email;
                    _customerService.UpdateCustomer(checkCustomer);
                    response = UtilityProcessor.SuccessulResponse(checkCustomer);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // DELETE: api/Customer/Delete/5
        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                var checkCustomer = _customerService.GetById(id);
                if (checkCustomer == null)
                {
                    response = UtilityProcessor.FailResponse("Customer to be updated does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    _customerService.DeleteCustomer(id);
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
