using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;

namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _service;
        public BookingController(BookingService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(Booking booking)
        {
            var r = _service.AddOrUpdate(booking, out var msg);
            var response = new Response
            {
                Msg = msg, Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }

        [HttpGet("{studentId}/{proId}")]
        public IActionResult Bookings(long studentId,long proId)
        {
            var response = new Response
            {
                Data = _service.Booking(studentId, proId) ?? new List<Booking>()
            };

            return Ok(response);
        }
        
    }
}
