using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;
using AutoMapper;
using ProViewGolf.Core.Dbo.Models;

namespace ProViewGolf.Controllers
{

    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _service;
        private readonly IMapper _mapper;
        public ReviewController(ReviewService service, IMapper mapper)
        {
            _service = service;
            _mapper =mapper;
        }

        [HttpPost]
        public IActionResult Add(ReviewModel review)
        {
            var model = _mapper.Map<ReviewModel,Review>(review);
            var r = _service.AddOrUpdate(model, out var msg);
            var response = new Response
            {
                Msg = msg, Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }

        [HttpGet("{studentId}/{proId}")]
        public IActionResult Reviews(long studentId,long proId)
        {
            var response = new Response
            {
                Data = _service.Reviews(studentId, proId) ?? new Review()
            };

            return Ok(response);
        }
        [HttpGet("{studentId}/{proId}")]
        public IActionResult ReviewAverage(long studentId, long proId)
        {
            var response = new Response
            {
                Data = _service.ReviewAverage(studentId, proId)
            };

            return Ok(response);
        }
    } 
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class InvitationController : ControllerBase
    {
        private readonly InvitationService _service;
        private readonly IMapper _mapper;
        public InvitationController(InvitationService service, IMapper mapper)
        {
            _service = service;
            _mapper =mapper;
        }

        [HttpGet("{studentId}")]
        public IActionResult Invitations(long studentId)
        {
            var response = new Response
            {
                Data = _service.InvitationsDetails(studentId)
            };

            return Ok(response);
        }
    }
}
