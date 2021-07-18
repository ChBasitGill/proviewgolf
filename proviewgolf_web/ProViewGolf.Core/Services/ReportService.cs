using System;
using System.Linq;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Helpers;

namespace ProViewGolf.Core.Services
{
    public class ReportService
    {
        private readonly IProGolfContext _dbo;

        public ReportService(IProGolfContext context)
        {
            _dbo = context;
        }

        public dynamic PracticeReport(long studentId, DateTime date)
        {
            var student = _dbo.Students.Find(studentId);
            if (student == null /*|| student.Role != Role.Student*/) return null;

            var shots = _dbo.ShotPractices.Where(x => x.StudentId == studentId && x.DateTime.Date == date.Date)
                .ToList();
            var clubs = _dbo.ClubPractices.Where(x => x.StudentId == studentId && x.DateTime.Date == date.Date)
                .ToList();
            var skills = _dbo.Skills.Where(x => x.StudentId == studentId && x.DateTime.Date == date.Date).ToList();

            var putt = shots.Where(x => x.ShotCategory == ShotCategory.Putt).ToList();
            var pitch = shots.Where(x => x.ShotCategory == ShotCategory.Pitch).ToList();
            var chip = shots.Where(x => x.ShotCategory == ShotCategory.Chip).ToList();
            var chipRun = shots.Where(x => x.ShotCategory == ShotCategory.ChipRun).ToList();

            var onCourse = shots.Where(x =>
                x.ShotCategory == ShotCategory.PlayHole).ToList();

            var bunker = shots
                .Where(x => x.ShotCategory == ShotCategory.Fairway || x.ShotCategory == ShotCategory.GreenSide)
                .ToList();

            var shortIron = clubs.Where(x =>
                x.Club == Club.PitchingWedge || x.Club == Club.GapWedge || x.Club == Club.SandWedge ||
                x.Club == Club.LobWedge).ToList();

            var iron = clubs.Where(x =>
                x.Club == Club.IronHybrid2 || x.Club == Club.IronHybrid3 || x.Club == Club.IronHybrid4 ||
                x.Club == Club.IronHybrid5).ToList();

            var wood = clubs.Where(x =>
                x.Club == Club.Wood3 || x.Club == Club.Wood5 || x.Club == Club.Wood7 || x.Club == Club.Rescue).ToList();

            var drives = clubs.Where(x => x.Club == Club.Driver).ToList();

            var data = new
            {
                student.ProViewLevel,

                AmountOfPutts = putt.Sum(i => i.Shots),
                PuttsHoled = putt.Average(x => x.Rating, 1),

                Pitching = pitch.Sum(i => i.Shots),
                PitchingRating = pitch.Average(i => i.Rating, 1),

                Chipping = chip.Sum(i => i.Shots),
                ChippingRating = chip.Average(i => i.Rating, 1),

                ChipRun = chipRun.Sum(i => i.Shots),
                ChipRunRating = chipRun.Average(i => i.Rating, 1),

                OnCourse = onCourse.Sum(i => i.Shots),

                BunkerShots = bunker.Sum(i => i.Shots),
                BunkerRating = bunker.Average(i => i.Rating, 1),

                ShortIronShots = shortIron.Sum(i => i.BallsAmount),
                ShortIronRating = shortIron.Average(i => i.Rating, 1),

                IronShots = iron.Sum(i => i.BallsAmount),
                IronShotsRating = iron.Average(i => i.Rating, 1),

                WoodShots = wood.Sum(i => i.BallsAmount),
                WoodShotsRating = wood.Average(i => i.Rating, 1),

                Drives = drives.Sum(i => i.BallsAmount),
                DrivesRating = drives.Average(i => i.Rating, 1),

                AmountOfShots = shortIron.Sum(i => i.BallsAmount) +
                                iron.Sum(i => i.BallsAmount) + wood.Sum(i => i.BallsAmount) +
                                drives.Sum(i => i.BallsAmount),
                //AmountOfShots = pitch.Sum(i => i.Shots) + chip.Sum(i => i.Shots) + chipRun.Sum(i => i.Shots) +
                //                shots.Sum(i => i.Shots) + bunker.Sum(i => i.Shots) + shortIron.Sum(i => i.BallsAmount) +
                //                iron.Sum(i => i.BallsAmount) + wood.Sum(i => i.BallsAmount) +
                //                drives.Sum(i => i.BallsAmount),

                Fitness = skills.Sum(i => i.FitnessSessionCore + i.FitnessSessionLowerBody + i.FitnessSessionUpperBody),

                GolfBasic = skills.Sum(i => i.RulesQuiz + i.VideoSwingAnalysis),
                GolfStrategy = skills.Sum(i => i.CourseManagement + i.AlignmentDrill),
                GreenReading = skills.Sum(i => i.GreenReading),
            };

            return data;
        }
    }
}