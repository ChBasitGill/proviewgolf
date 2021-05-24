using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;

namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class ReportController : ControllerBase
    {
          private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public IActionResult Practice(FilterModel model)
        {
            var response = new Response
            {
                Data = _reportService.PracticeReport(model.StudentId, model.Date)
            };
            return Ok(response);
        }
    }
}
