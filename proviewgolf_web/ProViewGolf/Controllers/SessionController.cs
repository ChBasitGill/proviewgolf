using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;
using ProViewGolf.Platform;

namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class SessionController : ControllerBase
    {
        private readonly SessionService _sessionService;

        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult Start(long studentId)
        {

            var sessionId = _sessionService.StartSession(this.UserId(), studentId, out var msg);
            var response = new Response
            {
                Msg =  msg,
                Status = studentId > 0 ? ResponseStatus.Success : ResponseStatus.Error,
                Data = sessionId,
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult End(long sessionId)
        {
            var r = _sessionService.EndSession(sessionId, out var msg);
            var response = new Response
            {
                Msg = msg, 
                Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }
        [HttpGet("{studentId}/{proId}")]
        public IActionResult Sessions(long studentId,long proId){
            var r = _sessionService.Session(proId,studentId, out var msg);
            var response = new Response
            {
                Msg = msg, 
                Data =r ?? new List<Core.Dbo.Entities.Session>(),
                Status = r !=null ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);    
        }
    }
}
