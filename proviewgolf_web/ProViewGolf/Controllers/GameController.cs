using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;

namespace ProViewGolf.Controllers
{
    [Authorize, ApiController, Route("api/[controller]/[Action]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public IActionResult AddScore(Game game)
        {
            var r = _gameService.AddOrUpdate(game, out var msg);
            var response = new Response
            {
                Msg = msg, Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult TournamentScore(FilterModel model)
        {
            var data = _gameService.GameScore(model.StudentId, model.Date, GameType.Tournament);
            var response = new Response
            {
                Data = data ?? new Game {GameType = GameType.Tournament}
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult PlayRoundScore(FilterModel model)
        {
            var data = _gameService.GameScore(model.StudentId, model.Date, GameType.PlayRounds);
            var response = new Response
            {
                Data = data ?? new Game {GameType = GameType.PlayRounds}
            };

            return Ok(response);
        }

         [HttpPost]
        public IActionResult TournamentScoreWithAverage(FilterModel model)
        {
            var data = _gameService.GamesWithAverage(model.StudentId, model.Date, GameType.Tournament);
            var response = new Response
            {
                Data = data ?? new GameDto {GameType = GameType.Tournament}
            };

            return Ok(response);
        }
        [HttpPost]
        public IActionResult TournamentScoreWithSum(FilterModel model)
        {
            var data = _gameService.GamesWithSum(model.StudentId, model.Date, GameType.Tournament);
            var response = new Response
            {
                Data = data ?? new GameDto {GameType = GameType.Tournament}
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult PlayRoundScoreWithAverage(FilterModel model)
        {
            var data = _gameService.GamesWithAverage(model.StudentId, model.Date, GameType.PlayRounds);
            var response = new Response
            {
                Data = data ?? new GameDto {GameType = GameType.PlayRounds}
            };

            return Ok(response);
        }
        [HttpPost]
        public IActionResult PlayRoundScoreWithSum(FilterModel model)
        {
            var data = _gameService.GamesWithSum(model.StudentId, model.Date, GameType.PlayRounds);
            var response = new Response
            {
                Data = data ?? new GameDto {GameType = GameType.PlayRounds}
            };

            return Ok(response);
        }
    }
}
