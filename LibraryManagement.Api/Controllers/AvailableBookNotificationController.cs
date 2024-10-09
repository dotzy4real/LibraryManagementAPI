using LibraryManagement.Data.Entity;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Services.Models.Requests;
using LibraryManagement.Services.Models.Response;
using LibraryManagement.Services.Utility;
using System.Net;
using Azure;
using Microsoft.AspNetCore.Authorization;


namespace LibraryManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableBookNotificationController : ControllerBase
    {
        private readonly IAvailableBookNotificationService _availableBookNotificationService;
        private readonly IBookService _bookService;
        public AvailableBookNotificationController(IAvailableBookNotificationService availableAvailableBookNotificationNotificationService, IBookService bookService)
        {
            _availableBookNotificationService = availableAvailableBookNotificationNotificationService;
            _bookService = bookService;
        }

        // GET: api/AvailableBookNotification/Details/5
        [HttpGet("Details/{id}")]
        public ActionResult Details(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBook(id);
                var availableBookNotificationNotification = _availableBookNotificationService.GetById(id);
                if (availableBookNotificationNotification == null)
                {
                    response = UtilityProcessor.FailResponse("AvailableBookNotification to be retrieved does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    response = UtilityProcessor.SuccessulResponse(availableBookNotificationNotification);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }

        // POST: api/AvailableBookNotification/List
        [HttpPost("List")]
        public ActionResult List()
        {
            var response = new LibraryApiResponse();
            try
            {
                _bookService.UpdateAvailableBooks();
                var availableBookNotifications = _availableBookNotificationService.GetAllAvailableBookNotifications();
                response = UtilityProcessor.SuccessulResponse(availableBookNotifications);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = UtilityProcessor.FailResponse(ex.InnerException + ex.Message);
                return BadRequest(response);
            }
        }


        // DELETE: api/AvailableBookNotification/Delete/5
        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var response = new LibraryApiResponse();
            try
            {
                var checkAvailableBookNotification = _availableBookNotificationService.GetById(id);
                if (checkAvailableBookNotification == null)
                {
                    response = UtilityProcessor.FailResponse("Available Book Notification to be updated does not exist", HttpStatusCode.BadRequest);
                    return BadRequest(response);
                }
                else
                {
                    _availableBookNotificationService.DeleteAvailableBookNotification(id);
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
