using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Dbo.Models;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;
using ProViewGolf.Platform;


namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class ClubPracticeController : ControllerBase
    {
        private readonly ClubPracticeService _clubService;

        public ClubPracticeController(ClubPracticeService clubService)
        {
            _clubService = clubService;
        }

        [HttpPost]
        public IActionResult AddScore(ClubPracticeModel clubs)
        {
            var r = _clubService.AddOrUpdate(clubs, out var msg);
            var response = new Response
            {
                Msg = msg, 
                Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }

        //[HttpPost]
        //public IActionResult IronScore(ClubSearchModel clubSearchModel)
        //{
        //    var response = new Response();
        //    var data = _clubService.IronScore(clubSearchModel);
        //    response.Data = data;

        //    if (data == null)
        //    {
        //        response.Msg = "no result found";
        //        response.Status = ResponseStatus.NoResult;
        //    }

        //    return Ok(response);
        //}

        [HttpPost]
        public IActionResult IronScores(FilterModel model)
        {
            var response = new Response
            {
                Data = _clubService.IronScore(model.StudentId, model.Date, this.UserId()) ?? new List<ClubRecordModel>()
            };

            return Ok(response);
        }


        [HttpPost]
        public IActionResult WoodScores(FilterModel model)
        {
            var response = new Response
            {
                Data = _clubService.WoodScore(model.StudentId, model.Date, this.UserId()) ?? new List<ClubRecordModel>()
            };

            return Ok(response);
        }
    }
}
