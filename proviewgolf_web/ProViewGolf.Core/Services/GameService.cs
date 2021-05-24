using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Services
{
    public class GameService
    {
        private readonly IProGolfContext _dbo;

        public GameService(IProGolfContext context)
        {
            _dbo = context;
        }

        public bool AddOrUpdate(Game game, out string msg)
        {
            try
            {
                var entity = _dbo.Games.AsNoTracking().FirstOrDefault(x =>
                    x.DateTime.Date == game.DateTime.Date && x.StudentId == game.StudentId &&
                    x.GameType == game.GameType);

                game.GameId = entity?.GameId ?? 0;
                game.Holes = game.GameType == GameType.Tournament ? Holes.H18 : game.Holes;
                _dbo.Games.AddOrUpdate(game, game.GameId);

                //if (entity != null)
                //{
                //    entity.WarmupTime = game.WarmupTime;
                //    entity.DriverPeaces = game.DriverPeaces;
                //    entity.IronPeaces = game.IronPeaces;
                //    entity.ChipPeaces = game.ChipPeaces;
                //    entity.SandPeaces = game.SandPeaces;
                //    entity.PuttPeaces = game.PuttPeaces;
                //    entity.GolfCourse = game.GolfCourse;
                //    entity.ExactHcp = game.ExactHcp;
                //    entity.PlayingHcp = game.PlayingHcp;
                //    entity.Nervous = game.Nervous;
                //    entity.FlightPartnersRating = game.FlightPartnersRating;
                //    entity.DriversRating = game.DriversRating;
                //    entity.DriversLeft = game.DriversLeft;
                //    entity.DriversCenter = game.DriversCenter;
                //    entity.DriversRight = game.DriversRight;
                //    entity.IronsRating = game.IronsRating;
                //    entity.IronsLeft = game.IronsLeft;
                //    entity.IronsCenter = game.IronsCenter;
                //    entity.IronsRight = game.IronsRight;
                //    entity.WoodsRating = game.WoodsRating;
                //    entity.WoodsLeft = game.WoodsLeft;
                //    entity.WoodsCenter = game.WoodsCenter;
                //    entity.WoodsRight = game.WoodsRight;
                //    entity.ShortIronGameRating = game.ShortIronGameRating;
                //    entity.BunkerShortsRating = game.BunkerShortsRating;
                //    entity.PuttingStrokes = game.PuttingStrokes;
                //    entity.GreenSpeedRating = game.GreenSpeedRating;
                //    entity.StableFordPoints = game.StableFordPoints;
                //    entity.Strokes = game.Strokes;
                //    entity.NewHcp = game.NewHcp;
                //    entity.Walking = game.Walking;
                //    entity.DistanceWalked = game.DistanceWalked;
                //    entity.GameDuration = game.GameDuration;


                //    entity.Holes = entity.GameType == GameType.Tournament ? Holes.H18 : game.Holes;
                //    _dbo.Update(entity);
                //}
                //else
                //{
                //    game.Holes = game.GameType == GameType.Tournament ? Holes.H18 : game.Holes;
                //    _dbo.Add(game);
                //}

                msg = "Tournament score updated successfully";
                _dbo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public Game GameScore(long studentId, DateTime date, GameType type)
        {
            return _dbo.Games.FirstOrDefault(x =>
                x.StudentId == studentId && x.DateTime.Date == date.Date && x.GameType == type);
        }
    }
}