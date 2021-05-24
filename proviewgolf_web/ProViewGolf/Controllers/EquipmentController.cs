using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Dbo.Models;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;
using ProViewGolf.Platform;

namespace ProViewGolf.Controllers
{

    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentService _equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        public IActionResult Add(EquipmentModel model)
        {
            var response = new Response();

            var r = _equipmentService.AddOrUpdate(model, out var msg);

            response.Msg = msg;
            response.Status = r ? ResponseStatus.Success : ResponseStatus.Error;

            return Ok(response);
            
        }

        [HttpPost]
        public IActionResult Delete([FromForm]long id)
        {
            var response = new Response();

            var r = _equipmentService.Delete(id, out var msg);

            response.Msg = msg;
            response.Status = r ? ResponseStatus.Success : ResponseStatus.Error;

            return Ok(response);
        }

        [HttpGet]
        public IActionResult Equipments(EquipmentType type)
        {
            var response = new Response
            {
                Data = _equipmentService.All(this.UserId(), type) ?? new List<Equipment>()
            };

            return Ok(response);
        }
    }
}

