using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;

namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _skillService;
        public SkillController(SkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost]
        public IActionResult Add(Skill skill)
        {
            var r = _skillService.AddOrUpdate(skill, out var msg);
            var response = new Response
            {
                Msg = msg, Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Skills(FilterModel model)
        {
            var response = new Response
            {
                Data = _skillService.Skills(model.StudentId, model.Date) ?? new Skill()
            };

            return Ok(response);
        }
        [HttpPost]
        public IActionResult SkillsWithAverage(FilterModel model)
        {
            var response = new Response
            {
                Data = _skillService.SkillsWithAverage(model.StudentId, model.Date) ?? new SkillDto()
            };

            return Ok(response);
        }]
    }
}
