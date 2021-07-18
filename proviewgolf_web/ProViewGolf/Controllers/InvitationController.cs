using ProViewGolf.DataLayer.Models;
using ProViewGolf.Core.Services;

namespace ProViewGolf.Controllers
{
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
