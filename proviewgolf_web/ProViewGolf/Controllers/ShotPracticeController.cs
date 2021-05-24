using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Dbo.Models;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;
using ProViewGolf.Platform;


namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class ShotPracticeController : ControllerBase
    {
        private readonly ShotPracticeService _shotService;

        public ShotPracticeController(ShotPracticeService shotService)
        {
            _shotService = shotService;
        }

        [HttpPost]
        public IActionResult AddScore(ShotPracticeModel shots)
        {
            var r = _shotService.AddOrUpdate(shots, out var msg);
            var response = new Response {Msg = msg, Status = r ? ResponseStatus.Success : ResponseStatus.Error};

            return Ok(response);
        }


        [HttpPost]
        public IActionResult ShotScores(FilterModel model)
        {
            var response = new Response
            {
                Data = _shotService.ShotScores(model.StudentId, model.Date, model.ShotGroup, this.UserId())
            };

            return Ok(response);
        }


        //[HttpPost(ApiRoutes.ShotPractise.GetBunkerShotPracticeScore)]
        //public IActionResult GetBunkerShotPracticeScore(ShotSearchModel ShotSearchModel)
        //{
        //    var response = new Response();
        //    var data = _shotPracticeService.GetBunkerShotPracticeScore(ShotSearchModel);
        //    response.Data = data;

        //    if (data == null)
        //    {
        //        response.Msg = "no result found";
        //        response.Status = ResponseStatus.NoResult;
        //    }

        //    return Ok(response);
        //}


        //[HttpPost(ApiRoutes.ShotPractise.GetChipRunShotPracticeScore)]
        //public IActionResult GetChipRunShotPracticeScore(ShotSearchModel ShotSearchModel)
        //{
        //    var response = new Response();
        //    var data = _shotPracticeService.GetChipRunShotPracticeScore(ShotSearchModel);
        //    response.Data = data;

        //    if (data == null)
        //    {
        //        response.Msg = "no result found";
        //        response.Status = ResponseStatus.NoResult;
        //    }

        //    return Ok(response);
        //}
        //[HttpPost(ApiRoutes.ShotPractise.GetPitchChipShotPracticeScore)]
        //public IActionResult GetPitchChipShotPracticeScore(ShotSearchModel ShotSearchModel)
        //{
        //    var response = new Response();
        //    var data = _shotPracticeService.GetPitchChipShotPracticeScore(ShotSearchModel);
        //    response.Data = data;

        //    if (data == null)
        //    {
        //        response.Msg = "no result found";
        //        response.Status = ResponseStatus.NoResult;
        //    }

        //    return Ok(response);
        //}
        //[HttpPost(ApiRoutes.ShotPractise.GetOnCourseShotPracticeScore)]
        //public IActionResult GetOnCourseShotPracticeScore(ShotSearchModel ShotSearchModel)
        //{
        //    var response = new Response();
        //    var data = _shotPracticeService.GetOnCourseShotPracticeScore(ShotSearchModel);
        //    response.Data = data;

        //    if (data == null)
        //    {
        //        response.Msg = "no result found";
        //        response.Status = ResponseStatus.NoResult;
        //    }

        //    return Ok(response);
        //}
        //[HttpPost(ApiRoutes.ShotPractise.GetPuttShotPracticeScore)]
        //public IActionResult GetPuttShotPracticeScore(ShotSearchModel ShotSearchModel)
        //{
        //    var response = new Response();
        //    var data = _shotPracticeService.GetPuttShotPracticeScore(ShotSearchModel);
        //    response.Data = data;

        //    if (data == null)
        //    {
        //        response.Msg = "no result found";
        //        response.Status = ResponseStatus.NoResult;
        //    }

        //    return Ok(response);
        //}


        [HttpPost]
        public IActionResult Stats(FilterModel model)
        {
            var response = new Response
            {
                Data = _shotService.ShotStats(model.StudentId, model.ShotType, model.Duration, this.UserId())
            };
            return Ok(response);
        }
    }
}
