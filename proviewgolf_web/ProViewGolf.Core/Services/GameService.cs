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
                //    entity.StableForPoints = game.StableForPoints;
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

                    StableForPoints = studentGames.Average(x => x.StableForPoints),
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

                        StableForPoints = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.StableForPoints),
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

                        StableForPoints = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.StableForPoints),
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

                        StableForPoints = lastSevenDays.Average(x => x.StableForPoints),
                        Strokes = lastSevenDays.Average(x => x.Strokes),
                        NewHcp = lastSevenDays.Average(x => x.NewHcp),
                        DistanceWalked = lastSevenDays.Average(x => x.DistanceWalked)

                    }
                }
            };
        }
public GameDto GamesWithSum(long studentId, DateTime date, GameType type)
        {
            var studentGames = _dbo.Games.Where(x => x.StudentId == studentId && x.GameType == type).ToList();
            var lastSevenDays = studentGames.Where(x => x.DateTime.Date <= date.Date && x.DateTime.Date >= date.AddDays(-7).Date).ToList();
            return new GameDto()
            {
                Game = studentGames.FirstOrDefault(x => x.DateTime.Date == date.Date.Date) ?? new Game(),
                Averages = new GamesAverages()
                {
                    WarmupTime = studentGames.Sum(x => x.WarmupTime),
                    DriverPeaces = studentGames.Sum(x => x.DriverPeaces),
                    IronPeaces = studentGames.Sum(x => x.IronPeaces),
                    ChipPeaces = studentGames.Sum(x => x.ChipPeaces),
                    SandPeaces = studentGames.Sum(x => x.SandPeaces),
                    PuttPeaces = studentGames.Sum(x => x.PuttPeaces),
                    ExactHcp = studentGames.Sum(x => x.ExactHcp),
                    PlayingHcp = studentGames.Sum(x => x.PlayingHcp),
                    FlightPartnersRating = studentGames.Sum(x => x.FlightPartnersRating),

                    DriversRating = studentGames.Sum(x => x.DriversRating),
                    DriversLeft = studentGames.Sum(x => x.DriversLeft),
                    DriversCenter = studentGames.Sum(x => x.DriversCenter),
                    DriversRight = studentGames.Sum(x => x.DriversRight),

                    IronsRating = studentGames.Sum(x => x.IronsRating),
                    IronsLeft = studentGames.Sum(x => x.IronsLeft),
                    IronsCenter = studentGames.Sum(x => x.IronsCenter),
                    IronsRight = studentGames.Sum(x => x.IronsRight),

                    WoodsRating = studentGames.Sum(x => x.WoodsRating),
                    WoodsLeft = studentGames.Sum(x => x.WoodsLeft),
                    WoodsCenter = studentGames.Sum(x => x.WoodsCenter),
                    WoodsRight = studentGames.Sum(x => x.WoodsRight),

                    ShortIronGameRating = studentGames.Sum(x => x.ShortIronGameRating),
                    BunkerShortsRating = studentGames.Sum(x => x.BunkerShortsRating),
                    PuttingStrokes = studentGames.Sum(x => x.PuttingStrokes),
                    GreenSpeedRating = studentGames.Sum(x => x.GreenSpeedRating),

                    StableForPoints = studentGames.Sum(x => x.StableForPoints),
                    Strokes = studentGames.Sum(x => x.Strokes),
                    NewHcp = studentGames.Sum(x => x.NewHcp),
                    DistanceWalked = studentGames.Sum(x => x.DistanceWalked)
                },
                MonthlyGrouping = studentGames.OrderByDescending(x => x.DateTime).GroupBy(g =>
                  new { g.DateTime.Year, g.DateTime.Month }).
                Select(s => new GamesGrouping
                {
                    Text = string.Format("{1} {0}", s.Key.Year, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(s.Key.Month)),
                    Average = new GamesAverages()
                    {
                        WarmupTime = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.WarmupTime),
                        DriverPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.DriverPeaces),
                        IronPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.IronPeaces),
                        ChipPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.ChipPeaces),
                        SandPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.SandPeaces),
                        PuttPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.PuttPeaces),
                        ExactHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.ExactHcp),
                        PlayingHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.PlayingHcp),
                        FlightPartnersRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.FlightPartnersRating),

                        DriversRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.DriversRating),
                        DriversLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.DriversLeft),
                        DriversCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.DriversCenter),
                        DriversRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.DriversRight),

                        IronsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.IronsRating),
                        IronsLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.IronsLeft),
                        IronsCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.IronsCenter),
                        IronsRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.IronsRight),

                        WoodsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.WoodsRating),
                        WoodsLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.WoodsLeft),
                        WoodsCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.WoodsCenter),
                        WoodsRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.WoodsRight),

                        ShortIronGameRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.ShortIronGameRating),
                        BunkerShortsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.BunkerShortsRating),
                        PuttingStrokes = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.PuttingStrokes),
                        GreenSpeedRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.GreenSpeedRating),

                        StableForPoints = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.StableForPoints),
                        Strokes = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.Strokes),
                        NewHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.NewHcp),
                        DistanceWalked = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.DistanceWalked)
                    }
                }),
                YearlyGrouping = studentGames.OrderByDescending(x => x.DateTime).GroupBy(g =>
                  new { g.DateTime.Year }).
                Select(s => new GamesGrouping
                {
                    Text = string.Format("{0}", s.Key.Year),
                    Average = new GamesAverages()
                    {
                        WarmupTime = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.WarmupTime),
                        DriverPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.DriverPeaces),
                        IronPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.IronPeaces),
                        ChipPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.ChipPeaces),
                        SandPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.SandPeaces),
                        PuttPeaces = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.PuttPeaces),
                        ExactHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.ExactHcp),
                        PlayingHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.PlayingHcp),
                        FlightPartnersRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.FlightPartnersRating),

                        DriversRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.DriversRating),
                        DriversLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.DriversLeft),
                        DriversCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.DriversCenter),
                        DriversRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.DriversRight),

                        IronsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.IronsRating),
                        IronsLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.IronsLeft),
                        IronsCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.IronsCenter),
                        IronsRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.IronsRight),

                        WoodsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.WoodsRating),
                        WoodsLeft = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.WoodsLeft),
                        WoodsCenter = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.WoodsCenter),
                        WoodsRight = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.WoodsRight),

                        ShortIronGameRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.ShortIronGameRating),
                        BunkerShortsRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.BunkerShortsRating),
                        PuttingStrokes = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.PuttingStrokes),
                        GreenSpeedRating = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.GreenSpeedRating),

                        StableForPoints = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.StableForPoints),
                        Strokes = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.Strokes),
                        NewHcp = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.NewHcp),
                        DistanceWalked = studentGames.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.DistanceWalked)

                    }
                }),
                WeeklyGrouping = new GamesGrouping
                {
                    Text = date.ToString("dd-MM-yy"),
                    Average = new GamesAverages()

                    {
                        WarmupTime = lastSevenDays.Sum(x => x.WarmupTime),
                        DriverPeaces = lastSevenDays.Sum(x => x.DriverPeaces),
                        IronPeaces = lastSevenDays.Sum(x => x.IronPeaces),
                        ChipPeaces = lastSevenDays.Sum(x => x.ChipPeaces),
                        SandPeaces = lastSevenDays.Sum(x => x.SandPeaces),
                        PuttPeaces = lastSevenDays.Sum(x => x.PuttPeaces),
                        ExactHcp = lastSevenDays.Sum(x => x.ExactHcp),
                        PlayingHcp = lastSevenDays.Sum(x => x.PlayingHcp),
                        FlightPartnersRating = lastSevenDays.Sum(x => x.FlightPartnersRating),

                        DriversRating = lastSevenDays.Sum(x => x.DriversRating),
                        DriversLeft = lastSevenDays.Sum(x => x.DriversLeft),
                        DriversCenter = lastSevenDays.Sum(x => x.DriversCenter),
                        DriversRight = lastSevenDays.Sum(x => x.DriversRight),

                        IronsRating = lastSevenDays.Sum(x => x.IronsRating),
                        IronsLeft = lastSevenDays.Sum(x => x.IronsLeft),
                        IronsCenter = lastSevenDays.Sum(x => x.IronsCenter),
                        IronsRight = lastSevenDays.Sum(x => x.IronsRight),

                        WoodsRating = lastSevenDays.Sum(x => x.WoodsRating),
                        WoodsLeft = lastSevenDays.Sum(x => x.WoodsLeft),
                        WoodsCenter = lastSevenDays.Sum(x => x.WoodsCenter),
                        WoodsRight = lastSevenDays.Sum(x => x.WoodsRight),

                        ShortIronGameRating = lastSevenDays.Sum(x => x.ShortIronGameRating),
                        BunkerShortsRating = lastSevenDays.Sum(x => x.BunkerShortsRating),
                        PuttingStrokes = lastSevenDays.Sum(x => x.PuttingStrokes),
                        GreenSpeedRating = lastSevenDays.Sum(x => x.GreenSpeedRating),

                        StableForPoints = lastSevenDays.Sum(x => x.StableForPoints),
                        Strokes = lastSevenDays.Sum(x => x.Strokes),
                        NewHcp = lastSevenDays.Sum(x => x.NewHcp),
                        DistanceWalked = lastSevenDays.Sum(x => x.DistanceWalked)

                    }
                }
            };
        }
    }
}