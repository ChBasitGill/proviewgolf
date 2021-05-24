using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Dbo.Models;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;
using ProViewGolf.Platform;


namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, UserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult UpdateProfile(ProfileModel model)
        {
            var r = _userService.UpdateProfile(model, this.UserId(), out string msg);
            var response = new Response
            {
                Msg = msg,
                Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }


        [HttpGet]
        public IActionResult Profile()
        {
            var profile = _userService.Profile(this.UserId());
            var response = new Response
            {
                Status = profile != null ? ResponseStatus.Success : ResponseStatus.NoResult,
                Data = profile
            };

            return Ok(response);
        }
    }
}
