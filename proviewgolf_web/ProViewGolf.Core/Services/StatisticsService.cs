using System;
using System.Linq;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Helpers;


namespace ProViewGolf.Core.Services
{
    public class StatisticsService
    {
        private readonly IProGolfContext _dbo;

        public StatisticsService(IProGolfContext context)
        {
            _dbo = context;
        }

        public dynamic IronStats(long studentId, DateTime date)
        {
            var ironStats = _dbo.ClubPractices
                .Where(x => x.StudentId == studentId && (int) x.Club >= 100 && (int) x.Club < 200).ToList()
                .GroupBy(x => new {x.Club, x.Ground}).ToList();

            return ironStats.Select(c =>
                new
                {
                    c.Key.Ground,
                    c.Key.Club,
                    AvgDistance = Math.Round(c.Average(a => a.AvgDistance), 1),
                    ClubHeadSpeed = Math.Round(c.Average(a => a.ClubHeadSpeed), 1),
                    SpinRate = Math.Round(c.Average(a => a.SpinRate), 1),
                    SmashFactor = Math.Round(c.Average(a => a.Apex), 1),
                    BallsAmount = Math.Round(c.Average(a => a.BallsAmount), 1),
                    Rating = Math.Round(c.Average(a => a.Rating), 1)
                });
        }

        public dynamic WoodStats(long studentId, DateTime date)
        {
            var woodStats = _dbo.ClubPractices
                .Where(x => x.StudentId == studentId && (int) x.Club >= 200 && (int) x.Club < 300).ToList()
                .GroupBy(x => new {x.Club, x.Ground}).ToList();

            return woodStats.Select(c =>
                new
                {
                    c.Key.Ground,
                    c.Key.Club,
                    AvgDistance = Math.Round(c.Average(a => a.AvgDistance), 1),
                    ClubHeadSpeed = Math.Round(c.Average(a => a.ClubHeadSpeed), 1),
                    SpinRate = Math.Round(c.Average(a => a.SpinRate), 1),
                    SmashFactor = Math.Round(c.Average(a => a.Apex), 1),
                    BallsAmount = Math.Round(c.Average(a => a.BallsAmount), 1),
                    Rating = Math.Round(c.Average(a => a.Rating), 1)
                });
        }

        public dynamic GeneralStats(long studentId, DateTime date)
        {
            var clubs = _dbo.ClubPractices.Where(x => x.StudentId == studentId);
            var shots = _dbo.ShotPractices.Where(x => x.StudentId == studentId);
            var games = _dbo.Games.Where(x => x.StudentId == studentId);

            return new
            {
                ProLessons = _dbo.Sessions.Where(x => x.StudentRefId == studentId).AsEnumerable()
                    .Sum(x => (x.End - x.Start).TotalHours),

                BallsWithPro = clubs.Where(x => x.IsWithPro).Sum(x => x.BallsAmount) +
                               shots.Where(x => x.ShotCategory != ShotCategory.Putt && x.IsWithPro).Sum(x => x.Shots),

                PracticeBalls = clubs.Sum(x => x.BallsAmount) +
                                shots.Where(x => x.ShotCategory != ShotCategory.Putt).Sum(x => x.Shots),

                ProViewLevel = _dbo.Students.Find(studentId).ProViewLevel,
                ProViewHcp = _dbo.Students.Find(studentId).ProViewHcp,

                Skills = 0,

                HoursPlayedRounds = games.Where(x => x.GameType == GameType.PlayRounds).Sum(x => x.GameDuration),
                TournamentRoundsPlayed = games.Count(x => x.GameType == GameType.Tournament),
                RoundsPlayed = games.Count(x => x.GameType == GameType.PlayRounds),
                DistanceWalked = games.Sum(x => x.DistanceWalked),
                HCPImprovement = games.OrderByDescending(x => x.GameId).FirstOrDefault()?.NewHcp ?? 0,
                StrokesImprovement = games.Any() ? games.Average(a => a.Strokes) : 0,
                PuttingImprovement = games.Any() ? games.Average(a => a.PuttingStrokes): 0
            };
        }
    }
}