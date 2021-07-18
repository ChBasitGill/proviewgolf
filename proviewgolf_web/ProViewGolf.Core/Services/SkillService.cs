using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using System.Globalization;
namespace ProViewGolf.Core.Services
{
    public class SkillService
    {
        private readonly IProGolfContext _dbo;

        public SkillService(IProGolfContext context)
        {
            _dbo = context;
        }

        public bool AddOrUpdate(Skill model, out string msg)
        {
            try
            {
                var entity = _dbo.Skills.AsNoTracking().FirstOrDefault(x =>
                    x.DateTime.Date == model.DateTime.Date && x.StudentId == model.StudentId);

                model.SkillId = entity?.SkillId ?? 0;
                _dbo.Skills.AddOrUpdate(model, model.SkillId);

                //if (skill != null)
                //{
                //    skill.Stretching = model.Stretching;
                //    skill.FitnessSessionLowerBody = model.FitnessSessionLowerBody;
                //    skill.FitnessSessionUpperBody = model.FitnessSessionUpperBody;
                //    skill.FitnessSessionCore = model.FitnessSessionCore;
                //    skill.MentalTraining = model.MentalTraining;
                //    skill.AlignmentDrill = model.AlignmentDrill;
                //    skill.GreenReading = model.GreenReading;
                //    skill.CourseManagement = model.CourseManagement;
                //    skill.RulesQuiz = model.RulesQuiz;
                //    skill.VideoSwingAnalysis = model.VideoSwingAnalysis;
                //    skill._18HolesWalk = model._18HolesWalk;
                //    skill._9HolesWalk = model._9HolesWalk;
                //    skill._18HolesPlayedWithGolfCar = model._18HolesPlayedWithGolfCar;
                //    _dbo.Update(skill);
                //}
                //else
                //{
                //    _dbo.Add(model);
                //}

                msg = "Skills updated successfully";
                _dbo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        // where parameters are up to 3, 4, try to pass them as parameters
        public Skill Skills(long studentId, DateTime date)
        {
            // its working here
            return _dbo.Skills.FirstOrDefault(x => x.StudentId == studentId && x.DateTime.Date == date.Date.Date);
        }
        public SkillDto SkillsWithAverage(long studentId, DateTime date)
        {
            var studentSkills = _dbo.Skills.Where(x => x.StudentId == studentId).ToList();
            var lastSevenDays = studentSkills.Where(x => x.DateTime.Date <= date.Date && x.DateTime.Date  >= date.AddDays(-7).Date).ToList();
            return new SkillDto()
            {
                Skill = studentSkills.FirstOrDefault(x => x.DateTime.Date == date.Date.Date) ?? new Skill(),
                Averages = new SkillsAverages()
                {
                    Stretching = studentSkills.Average(x => x.Stretching),
                    FitnessSessionLowerBody = studentSkills.Average(x => x.FitnessSessionLowerBody),
                    FitnessSessionUpperBody = studentSkills.Average(x => x.FitnessSessionUpperBody),
                    FitnessSessionCore = studentSkills.Average(x => x.FitnessSessionCore),
                    MentalTraining = studentSkills.Average(x => x.MentalTraining),
                    AlignmentDrill = studentSkills.Average(x => x.AlignmentDrill),
                    GreenReading = studentSkills.Average(x => x.GreenReading),
                    CourseManagement = studentSkills.Average(x => x.CourseManagement),
                    RulesQuiz = studentSkills.Average(x => x.RulesQuiz),
                    VideoSwingAnalysis = studentSkills.Average(x => x.VideoSwingAnalysis),
                    _18HolesWalk = studentSkills.Average(x => x._18HolesWalk),
                    _9HolesWalk = studentSkills.Average(x => x._9HolesWalk),
                    _18HolesPlayedWithGolfCar = studentSkills.Average(x => x._18HolesPlayedWithGolfCar)
                },
                MonthlyGrouping = studentSkills.OrderByDescending(x => x.DateTime).GroupBy(g =>
                  new { g.DateTime.Year, g.DateTime.Month }).
                Select(s => new SkillGrouping
                {
                    Text = string.Format("{1} {0}", s.Key.Year, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(s.Key.Month)),
                    Average = new SkillsAverages()
                    {
                        Stretching = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.Stretching),
                        FitnessSessionLowerBody = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.FitnessSessionLowerBody),
                        FitnessSessionUpperBody = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.FitnessSessionUpperBody),
                        FitnessSessionCore = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.FitnessSessionCore),
                        MentalTraining = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.MentalTraining),
                        AlignmentDrill = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.AlignmentDrill),
                        GreenReading = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.GreenReading),
                        CourseManagement = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.CourseManagement),
                        RulesQuiz = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.RulesQuiz),
                        VideoSwingAnalysis = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x.VideoSwingAnalysis),
                        _18HolesWalk = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x._18HolesWalk),
                        _9HolesWalk = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x._9HolesWalk),
                        _18HolesPlayedWithGolfCar = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Average(x => x._18HolesPlayedWithGolfCar)
                    }
                }),
                YearlyGrouping = studentSkills.OrderByDescending(x => x.DateTime).GroupBy(g =>
                  new { g.DateTime.Year}).
                Select(s => new SkillGrouping
                {
                    Text = string.Format("{0}", s.Key.Year),
                    Average = new SkillsAverages()
                    {
                        Stretching = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.Stretching),
                        FitnessSessionLowerBody = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.FitnessSessionLowerBody),
                        FitnessSessionUpperBody = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.FitnessSessionUpperBody),
                        FitnessSessionCore = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.FitnessSessionCore),
                        MentalTraining = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.MentalTraining),
                        AlignmentDrill = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.AlignmentDrill),
                        GreenReading = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.GreenReading),
                        CourseManagement = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.CourseManagement),
                        RulesQuiz = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.RulesQuiz),
                        VideoSwingAnalysis = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x.VideoSwingAnalysis),
                        _18HolesWalk = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x._18HolesWalk),
                        _9HolesWalk = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x._9HolesWalk),
                        _18HolesPlayedWithGolfCar = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Average(x => x._18HolesPlayedWithGolfCar)
                    }
                }),
                WeeklyGrouping =  new SkillGrouping
                {
                    Text = date.ToString("dd-MM-yy"),
                    Average = new SkillsAverages()

                    {
                        Stretching = lastSevenDays.Average(x => x.Stretching),
                        FitnessSessionLowerBody = lastSevenDays.Average(x => x.FitnessSessionLowerBody),
                        FitnessSessionUpperBody = lastSevenDays.Average(x => x.FitnessSessionUpperBody),
                        FitnessSessionCore = lastSevenDays.Average(x => x.FitnessSessionCore),
                        MentalTraining = lastSevenDays.Average(x => x.MentalTraining),
                        AlignmentDrill = lastSevenDays.Average(x => x.AlignmentDrill),
                        GreenReading = lastSevenDays.Average(x => x.GreenReading),
                        CourseManagement = lastSevenDays.Average(x => x.CourseManagement),
                        RulesQuiz = lastSevenDays.Average(x => x.RulesQuiz),
                        VideoSwingAnalysis = lastSevenDays.Average(x => x.VideoSwingAnalysis),
                        _18HolesWalk = lastSevenDays.Average(x => x._18HolesWalk),
                        _9HolesWalk = lastSevenDays.Average(x => x._9HolesWalk),
                        _18HolesPlayedWithGolfCar = lastSevenDays.Average(x => x._18HolesPlayedWithGolfCar)
                    }
                }
            };
        }
        public SkillDto SkillsWithSum(long studentId, DateTime date)
        {
            var studentSkills = _dbo.Skills.Where(x => x.StudentId == studentId).ToList();
            var lastSevenDays = studentSkills.Where(x => x.DateTime.Date <= date.Date && x.DateTime.Date  >= date.AddDays(-7).Date).ToList();
            return new SkillDto()
            {
                Skill = studentSkills.FirstOrDefault(x => x.DateTime.Date == date.Date.Date) ?? new Skill(),
                Averages = new SkillsAverages()
                {
                    Stretching = studentSkills.Sum(x => x.Stretching),
                    FitnessSessionLowerBody = studentSkills.Sum(x => x.FitnessSessionLowerBody),
                    FitnessSessionUpperBody = studentSkills.Sum(x => x.FitnessSessionUpperBody),
                    FitnessSessionCore = studentSkills.Sum(x => x.FitnessSessionCore),
                    MentalTraining = studentSkills.Sum(x => x.MentalTraining),
                    AlignmentDrill = studentSkills.Sum(x => x.AlignmentDrill),
                    GreenReading = studentSkills.Sum(x => x.GreenReading),
                    CourseManagement = studentSkills.Sum(x => x.CourseManagement),
                    RulesQuiz = studentSkills.Sum(x => x.RulesQuiz),
                    VideoSwingAnalysis = studentSkills.Sum(x => x.VideoSwingAnalysis),
                    _18HolesWalk = studentSkills.Sum(x => x._18HolesWalk),
                    _9HolesWalk = studentSkills.Sum(x => x._9HolesWalk),
                    _18HolesPlayedWithGolfCar = studentSkills.Sum(x => x._18HolesPlayedWithGolfCar)
                },
                MonthlyGrouping = studentSkills.OrderByDescending(x => x.DateTime).GroupBy(g =>
                  new { g.DateTime.Year, g.DateTime.Month }).
                Select(s => new SkillGrouping
                {
                    Text = string.Format("{1} {0}", s.Key.Year, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(s.Key.Month)),
                    Average = new SkillsAverages()
                    {
                        Stretching = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.Stretching),
                        FitnessSessionLowerBody = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.FitnessSessionLowerBody),
                        FitnessSessionUpperBody = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.FitnessSessionUpperBody),
                        FitnessSessionCore = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.FitnessSessionCore),
                        MentalTraining = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.MentalTraining),
                        AlignmentDrill = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.AlignmentDrill),
                        GreenReading = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.GreenReading),
                        CourseManagement = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.CourseManagement),
                        RulesQuiz = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.RulesQuiz),
                        VideoSwingAnalysis = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x.VideoSwingAnalysis),
                        _18HolesWalk = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x._18HolesWalk),
                        _9HolesWalk = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x._9HolesWalk),
                        _18HolesPlayedWithGolfCar = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year && x.DateTime.Date.Month == s.Key.Month).Sum(x => x._18HolesPlayedWithGolfCar)
                    }
                }),
                YearlyGrouping = studentSkills.OrderByDescending(x => x.DateTime).GroupBy(g =>
                  new { g.DateTime.Year}).
                Select(s => new SkillGrouping
                {
                    Text = string.Format("{0}", s.Key.Year),
                    Average = new SkillsAverages()
                    {
                        Stretching = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.Stretching),
                        FitnessSessionLowerBody = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.FitnessSessionLowerBody),
                        FitnessSessionUpperBody = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.FitnessSessionUpperBody),
                        FitnessSessionCore = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.FitnessSessionCore),
                        MentalTraining = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.MentalTraining),
                        AlignmentDrill = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.AlignmentDrill),
                        GreenReading = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.GreenReading),
                        CourseManagement = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.CourseManagement),
                        RulesQuiz = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.RulesQuiz),
                        VideoSwingAnalysis = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x.VideoSwingAnalysis),
                        _18HolesWalk = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x._18HolesWalk),
                        _9HolesWalk = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x._9HolesWalk),
                        _18HolesPlayedWithGolfCar = studentSkills.Where(x => x.DateTime.Date.Year == s.Key.Year).Sum(x => x._18HolesPlayedWithGolfCar)
                    }
                }),
                WeeklyGrouping =  new SkillGrouping
                {
                    Text = date.ToString("dd-MM-yy"),
                    Average = new SkillsAverages()

                    {
                        Stretching = lastSevenDays.Sum(x => x.Stretching),
                        FitnessSessionLowerBody = lastSevenDays.Sum(x => x.FitnessSessionLowerBody),
                        FitnessSessionUpperBody = lastSevenDays.Sum(x => x.FitnessSessionUpperBody),
                        FitnessSessionCore = lastSevenDays.Sum(x => x.FitnessSessionCore),
                        MentalTraining = lastSevenDays.Sum(x => x.MentalTraining),
                        AlignmentDrill = lastSevenDays.Sum(x => x.AlignmentDrill),
                        GreenReading = lastSevenDays.Sum(x => x.GreenReading),
                        CourseManagement = lastSevenDays.Sum(x => x.CourseManagement),
                        RulesQuiz = lastSevenDays.Sum(x => x.RulesQuiz),
                        VideoSwingAnalysis = lastSevenDays.Sum(x => x.VideoSwingAnalysis),
                        _18HolesWalk = lastSevenDays.Sum(x => x._18HolesWalk),
                        _9HolesWalk = lastSevenDays.Sum(x => x._9HolesWalk),
                        _18HolesPlayedWithGolfCar = lastSevenDays.Sum(x => x._18HolesPlayedWithGolfCar)
                    }
                }
            };
        }
    }
}