using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;
using ProViewGolf.Platform;

namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _service;

        public InstructorController(InstructorService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AddPro([FromForm] string email)
        {
            var r = _service.AddPro(email, this.UserId(), out var msg);
            var response = new Response
            {
                Msg = msg,
                Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddStudent([FromForm] string email)
        {
            var r = _service.AddStudent(email, this.UserId(), out var msg);
            var response = new Response
            {
                Msg = msg, Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult Students()
        {
            var response = new Response
            {
                Data = _service.Students(this.UserId())
            };

            if (response.Data == null)
                response.Status = ResponseStatus.NoResult;

            return Ok(response);
        }

        [HttpGet]
        public IActionResult Instructor()
        {
            var response = new Response
            {
                Data = _service.Instructor(this.UserId())
            };

            if (response.Data == null)
                response.Status = ResponseStatus.NoResult;

            return Ok(response);
        }


        [HttpPost]
        public IActionResult StudentProfile([FromForm] long studentId)
        {
            var response = new Response
            {
                Data = _service.StudentProfile(this.UserId(), studentId)
            };

            if (response.Data != null)
                return Ok(response);

            response.Msg = "no result found";
            response.Status = ResponseStatus.NoResult;
            return Ok(response);
        }


        [HttpPost]
        public IActionResult Unassigned([FromForm] long studentId)
        {
            var r = _service.Unassigned(studentId, out var msg);
            var response = new Response
            {
                Msg = msg,
                Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }
        [HttpGet]
        public IActionResult InstructorProfile(long UserId)
        {
            var response = new Response
            {
                Data = _service.InstructorPro(UserId)
            };

            if (response.Data == null)
                response.Status = ResponseStatus.NoResult;

            return Ok(response);
        }

    }
}
