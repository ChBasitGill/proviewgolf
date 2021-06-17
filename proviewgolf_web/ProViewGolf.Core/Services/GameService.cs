using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using System.Globalization;


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
        public GameDto GamesWithAverage(long studentId, DateTime date, GameType type)
        {
            var studentGames = _dbo.Games.Where(x => x.StudentId == studentId && x.GameType == type).ToList();
            var lastSevenDays = studentGames.Where(x => x.DateTime.Date <= date.Date && x.DateTime.Date >= date.AddDays(-7).Date).ToList();
            return new GameDto()
            {
                Game = studentGames.FirstOrDefault(x => x.DateTime.Date == date.Date.Date) ?? new Game(),
                Averages = new GamesAverages()
                {
                    WarmupTime = studentGames.Average(x => x.WarmupTime),
                    DriverPeaces = studentGames.Average(x => x.DriverPeaces),
                    IronPeaces = studentGames.Average(x => x.IronPeaces),
                    ChipPeaces = studentGames.Average(x => x.ChipPeaces),
                    SandPeaces = studentGames.Average(x => x.SandPeaces),
                    PuttPeaces = studentGames.Average(x => x.PuttPeaces),
                    ExactHcp = studentGames.Average(x => x.ExactHcp),
                    PlayingHcp = studentGames.Average(x => x.PlayingHcp),
                    FlightPartnersRating = studentGames.Average(x => x.FlightPartnersRating),

                    DriversRating = studentGames.Average(x => x.DriversRating),
                    DriversLeft = studentGames.Average(x => x.DriversLeft),
                    DriversCenter = studentGames.Average(x => x.DriversCenter),
                    DriversRight = studentGames.Average(x => x.DriversRight),

                    IronsRating = studentGames.Average(x => x.IronsRating),
                    IronsLeft = studentGames.Average(x => x.IronsLeft),
                    IronsCenter = studentGames.Average(x => x.IronsCenter),
                    IronsRight = studentGames.Average(x => x.IronsRight),

                    WoodsRating = studentGames.Average(x => x.WoodsRating),
                    WoodsLeft = studentGames.Average(x => x.WoodsLeft),
                    WoodsCenter = studentGames.Average(x => x.WoodsCenter),
                    WoodsRight = studentGames.Average(x => x.WoodsRight),

                    ShortIronGameRating = studentGames.Average(x => x.ShortIronGameRating),
                    BunkerShortsRating = studentGames.Average(x => x.BunkerShortsRating),
                    PuttingStrokes = studentGames.Average(x => x.PuttingStrokes),
                    GreenSpeedRating = studentGames.Average(x => x.GreenSpeedRating),

                    StableFordPoints = studentGames.Average(x => x.StableFordPoints),
                    Strokes = studentGames.Average(x => x.Strokes),
                    NewHcp = studentGames.Average(x => x.NewHcp),
                    DistanceWalked = studentGames.Average(x => x.DistanceWalked)
                },
                MonthlyGrouping = studentGames.OrderByDescending(x => x.DateTime).GroupBy(g =>
                  new { g.DateTime.Year, g.DateTime.Month }).
                Select(s => new GamesGrouping
                {
                    Text = string.Format("{1} {0}", s.Key.Year, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(s.Key.Month)),
                    Average = new GamesAverages()
                    {
                        WarmupTime = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.WarmupTime),
                        DriverPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.DriverPeaces),
                        IronPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.IronPeaces),
                        ChipPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.ChipPeaces),
                        SandPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.SandPeaces),
                        PuttPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.PuttPeaces),
                        ExactHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.ExactHcp),
                        PlayingHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.PlayingHcp),
                        FlightPartnersRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.FlightPartnersRating),

                        DriversRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.DriversRating),
                        DriversLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.DriversLeft),
                        DriversCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.DriversCenter),
                        DriversRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.DriversRight),

                        IronsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.IronsRating),
                        IronsLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.IronsLeft),
                        IronsCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.IronsCenter),
                        IronsRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.IronsRight),

                        WoodsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.WoodsRating),
                        WoodsLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.WoodsLeft),
                        WoodsCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.WoodsCenter),
                        WoodsRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.WoodsRight),

                        ShortIronGameRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.ShortIronGameRating),
                        BunkerShortsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.BunkerShortsRating),
                        PuttingStrokes = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.PuttingStrokes),
                        GreenSpeedRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.GreenSpeedRating),

                        StableFordPoints = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.StableFordPoints),
                        Strokes = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.Strokes),
                        NewHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.NewHcp),
                        DistanceWalked = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.DistanceWalked)
                    }
                }),
                YearlyGrouping = studentGames.OrderByDescending(x => x.DateTime).GroupBy(g =>
                  new { g.DateTime.Year }).
                Select(s => new GamesGrouping
                {
                    Text = string.Format("{0}", s.Key.Year),
                    Average = new GamesAverages()
                    {
                        WarmupTime = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.WarmupTime),
                        DriverPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.DriverPeaces),
                        IronPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.IronPeaces),
                        ChipPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.ChipPeaces),
                        SandPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.SandPeaces),
                        PuttPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.PuttPeaces),
                        ExactHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.ExactHcp),
                        PlayingHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.PlayingHcp),
                        FlightPartnersRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.FlightPartnersRating),

                        DriversRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.DriversRating),
                        DriversLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.DriversLeft),
                        DriversCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.DriversCenter),
                        DriversRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.DriversRight),

                        IronsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.IronsRating),
                        IronsLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.IronsLeft),
                        IronsCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.IronsCenter),
                        IronsRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.IronsRight),

                        WoodsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.WoodsRating),
                        WoodsLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.WoodsLeft),
                        WoodsCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.WoodsCenter),
                        WoodsRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.WoodsRight),

                        ShortIronGameRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.ShortIronGameRating),
                        BunkerShortsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.BunkerShortsRating),
                        PuttingStrokes = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.PuttingStrokes),
                        GreenSpeedRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.GreenSpeedRating),

                        StableFordPoints = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.StableFordPoints),
                        Strokes = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.Strokes),
                        NewHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.NewHcp),
                        DistanceWalked = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.DistanceWalked)

                    }
                }),
                WeeklyGrouping = new GamesGrouping
                {
                    Text = date.ToString("dd-MM-yy"),
                    Average = new GamesAverages()

                    {
                        WarmupTime = lastSevenDays.Average(x => x.WarmupTime),
                        DriverPeaces = lastSevenDays.Average(x => x.DriverPeaces),
                        IronPeaces = lastSevenDays.Average(x => x.IronPeaces),
                        ChipPeaces = lastSevenDays.Average(x => x.ChipPeaces),
                        SandPeaces = lastSevenDays.Average(x => x.SandPeaces),
                        PuttPeaces = lastSevenDays.Average(x => x.PuttPeaces),
                        ExactHcp = lastSevenDays.Average(x => x.ExactHcp),
                        PlayingHcp = lastSevenDays.Average(x => x.PlayingHcp),
                        FlightPartnersRating = lastSevenDays.Average(x => x.FlightPartnersRating),

                        DriversRating = lastSevenDays.Average(x => x.DriversRating),
                        DriversLeft = lastSevenDays.Average(x => x.DriversLeft),
                        DriversCenter = lastSevenDays.Average(x => x.DriversCenter),
                        DriversRight = lastSevenDays.Average(x => x.DriversRight),

                        IronsRating = lastSevenDays.Average(x => x.IronsRating),
                        IronsLeft = lastSevenDays.Average(x => x.IronsLeft),
                        IronsCenter = lastSevenDays.Average(x => x.IronsCenter),
                        IronsRight = lastSevenDays.Average(x => x.IronsRight),

                        WoodsRating = lastSevenDays.Average(x => x.WoodsRating),
                        WoodsLeft = lastSevenDays.Average(x => x.WoodsLeft),
                        WoodsCenter = lastSevenDays.Average(x => x.WoodsCenter),
                        WoodsRight = lastSevenDays.Average(x => x.WoodsRight),

                        ShortIronGameRating = lastSevenDays.Average(x => x.ShortIronGameRating),
                        BunkerShortsRating = lastSevenDays.Average(x => x.BunkerShortsRating),
                        PuttingStrokes = lastSevenDays.Average(x => x.PuttingStrokes),
                        GreenSpeedRating = lastSevenDays.Average(x => x.GreenSpeedRating),

                        StableFordPoints = lastSevenDays.Average(x => x.StableFordPoints),
                        Strokes = lastSevenDays.Average(x => x.Strokes),
                        NewHcp = lastSevenDays.Average(x => x.NewHcp),
                        DistanceWalked = lastSevenDays.Average(x => x.DistanceWalked)

                    }
                }
            };
        }

    }
}