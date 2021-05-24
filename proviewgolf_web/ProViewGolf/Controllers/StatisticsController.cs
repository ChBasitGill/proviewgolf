using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;

namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statsService;
        public StatisticsController(StatisticsService statsService)
        {
            _statsService = statsService;
        }

        [HttpPost]
        public IActionResult Iron(FilterModel model)
        {
            var response = new Response();
            var data = _statsService.IronStats(model.StudentId, model.Date);
            response.Data = data;

            if (data == null)
            {
                response.Msg = "no result found";
                response.Status = ResponseStatus.NoResult;
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Wood(FilterModel model)
        {
            var response = new Response();
            var data = _statsService.WoodStats(model.StudentId, model.Date);
            response.Data = data;

            if (data == null)
            {
                response.Msg = "no result found";
                response.Status = ResponseStatus.NoResult;
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult General(FilterModel model)
        {
            var response = new Response
            {
                Data = _statsService.GeneralStats(model.StudentId, model.Date)
            };

            return Ok(response);
        }


        //[HttpPost]
        //public IActionResult Graph(StatsGraphModel graphModel)
        //{
        //    var response = new Response
        //    {
        //        Data = _statsService.StatsGraph(graphModel)
        //    };

        //    return Ok(response);
        //}
    }
}
