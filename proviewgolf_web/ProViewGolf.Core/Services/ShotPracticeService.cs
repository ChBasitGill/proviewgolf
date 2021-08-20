using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Dbo.Models;
using ProViewGolf.Core.Helpers;

namespace ProViewGolf.Core.Services
{
    public class ShotPracticeService
    {
        private readonly IProGolfContext _dbo;
        private readonly IMapper _mapper;

        public ShotPracticeService(IProGolfContext context, IMapper mapper)
        {
            _dbo = context;
            _mapper = mapper;
        }

        public bool AddOrUpdate(ShotPracticeModel model, out string msg)
        {
            try
            {
                foreach (var item in model.ShotRecords)
                {
                    if (item.Shots <= 0) continue;


                    var entity = _mapper.Map<ShotPractice>(item);
                    entity.DateTime = model.DateTime;
                    entity.IsWithPro = model.IsWithPro;
                    entity.StudentId = model.StudentId;

                    var old = _dbo.ShotPractices.AsNoTracking().FirstOrDefault(x =>
                        x.StudentId == model.StudentId && x.DateTime.Date == model.DateTime.Date &&
                        x.IsWithPro == model.IsWithPro && x.ShotCategory == item.ShotCategory &&
                        x.ShotType == item.ShotType);

                    //??
                    //      new ShotPractice
                    //      {
                    //          StudentId = model.StudentId, ShotCategory = item.ShotCategory,
                    //          ShotType = item.ShotType
                    //      };



                    //if (model.IsWithPro)
                    //{
                    //    entity.ShotsWithPro = item.Shots;
                    //    entity.GoodShotsWithPro = item.GoodShots;
                    //}
                    //else
                    //{
                    //    entity.Shots = item.Shots;
                    //    entity.GoodShots = item.GoodShots;
                    //}

                    //if (entity.ShotPracticeId <= 0)
                    //    _dbo.ShotPractices.Add(entity);
                    //else
                    //    _dbo.ShotPractices.Update(entity);

                    entity.Rating = entity.GoodShots / entity.Shots * 5;
                    entity.Rating = entity.Rating.Round(1);

                    if (old != null)
                        entity.ShotPracticeId = old.ShotPracticeId;

                    _dbo.ShotPractices.AddOrUpdate(entity, entity.ShotPracticeId);
                }

                _dbo.SaveChanges();
                msg = "record successfully updated";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }


        public List<ShotRecordModel> ShotScores(long studentId, DateTime date, ShotGroup shotGroup, long userId)
        {
            var isWithPro = _dbo.Pros.Any(x => x.UserId == userId);
            var data = _dbo.ShotPractices.Where(x =>
                x.StudentId == studentId && x.DateTime.Date == date.Date && x.IsWithPro == isWithPro);

            switch (shotGroup)
            {
                case ShotGroup.PuttingRun:
                    data = data.Where(
                        x => x.ShotCategory == ShotCategory.Putt || x.ShotCategory == ShotCategory.ChipRun);
                    break;

                case ShotGroup.Bunker:
                    data = data.Where(x =>
                        x.ShotCategory == ShotCategory.GreenSide || x.ShotCategory == ShotCategory.Fairway);
                    break;

                case ShotGroup.PitchChip:
                    data = data.Where(x =>
                        x.ShotCategory == ShotCategory.Pitch || x.ShotCategory == ShotCategory.Chip);
                    break;

                case ShotGroup.OnCourse:
                    data = data.Where(x =>
                        x.ShotCategory == ShotCategory.PlayHole || x.ShotCategory == ShotCategory.Scrambling);
                    break;
            }

            return data.Select(r => _mapper.Map<ShotRecordModel>(r)).ToList();

            //if (isWithPro)
            //    return data.Select(x => new ShotRecordModel
            //    {
            //        ShotCategory = x.ShotCategory, ShotType = x.ShotType,
            //        Shots = x.ShotsWithPro, GoodShots = x.GoodShotsWithPro
            //    }).ToList();

            //return data.Select(x => new ShotRecordModel
            //{
            //    ShotCategory = x.ShotCategory, ShotType = x.ShotType,
            //    Shots = x.Shots, GoodShots = x.GoodShots
            //}).ToList();
        }

        //public List<ShotPractice> GetBunkerShotPracticeScore(ShotSearchModel shotSearchModel)
        //{
        //    return _dbo.ShotPractices.Where(x =>
        //        x.UserRefId == shotSearchModel.UserId && x.DateTime.Date == shotSearchModel.Date.Date &&
        //        (x.ShotCategory == ShotCategory.GreenSide || x.ShotCategory == ShotCategory.Fairway)).ToList();
        //}

        //public List<ShotPractice> GetChipRunShotPracticeScore(ShotSearchModel shotSearchModel)
        //{
        //    return _dbo.ShotPractices.Where(x =>
        //        x.UserRefId == shotSearchModel.UserId && x.DateTime.Date == shotSearchModel.Date.Date &&
        //        x.ShotCategory == ShotCategory.ChipRun).ToList();
        //}

        //public List<ShotPractice> GetPitchChipShotPracticeScore(ShotSearchModel shotSearchModel)
        //{
        //    return _dbo.ShotPractices.Where(x =>
        //        x.UserRefId == shotSearchModel.UserId && x.DateTime.Date == shotSearchModel.Date.Date &&
        //        (x.ShotCategory == ShotCategory.Chip || x.ShotCategory == ShotCategory.Pitch)).ToList();
        //}

        //public List<ShotPractice> GetOnCourseShotPracticeScore(ShotSearchModel shotSearchModel)
        //{
        //    return _dbo.ShotPractices.Where(x =>
        //        x.UserRefId == shotSearchModel.UserId && x.DateTime.Date == shotSearchModel.Date.Date &&
        //        (x.ShotCategory == ShotCategory.PlayHole || x.ShotCategory == ShotCategory.Scrambling)).ToList();
        //}

        //public List<ShotPractice> GetPuttShotPracticeScore(ShotSearchModel shotSearchModel)
        //{
        //    return _dbo.ShotPractices.Where(x =>
        //        x.UserRefId == shotSearchModel.UserId && x.DateTime.Date == shotSearchModel.Date.Date &&
        //        x.ShotCategory == ShotCategory.Putt).ToList();
        //}

        public List<ShotStatModel> ShotStats(long studentId, int shotType, Duration duration, long userId)
        {
            var isWithPro = _dbo.Pros.Any(x => x.UserId == userId);
            var from = DateTime.Now.Date;

            var data = _dbo.ShotPractices.Where(x =>
                    x.StudentId == studentId && x.ShotType == shotType && x.IsWithPro == isWithPro)
                .OrderBy(o => o.DateTime);

            switch (duration)
            {
                case Duration.Week:

                    from = from.AddDays(-7).Date;
                    return data.Where(x => x.DateTime.Date >= from)
                        .Select(x => new ShotStatModel
                        {
                            XAxis = x.DateTime.ToString("ddd").ToUpper(),
                            Shots = x.Shots,
                            GoodShots = x.GoodShots,
                            YAxis = x.GoodShots * 100 / x.Shots,
                        }).ToList();

                case Duration.Month:
                    from = from.AddMonths(-1).Date;
                    var monthData = data.Where(x => x.DateTime.Date >= from).ToList()
                        .GroupBy(g =>
                            CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(g.DateTime.Date,
                                CalendarWeekRule.FirstDay,
                                DayOfWeek.Monday)).Select(g => new ShotStatModel
                        {
                            XAxis = "W" + g.Key,
                            Shots = g.Sum(i => i.Shots),
                            GoodShots = g.Sum(i => i.GoodShots),
                            YAxis = g.Sum(i => i.GoodShots) * 100 / g.Sum(i => i.Shots),

                        }).ToList();

                    return monthData;


                case Duration.Year:
                    from = from.AddMonths(-12).Date;
                    var yearData = data.Where(x => x.DateTime.Date >= from).ToList()
                        .GroupBy(g => g.DateTime.Month).Select(g => new ShotStatModel
                        {
                            XAxis = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key).ToUpper(),
                            Shots = g.Sum(i => i.Shots),
                            GoodShots = g.Sum(i => i.GoodShots),
                            YAxis = g.Sum(i => i.GoodShots) * 100 / g.Sum(i => i.Shots),
                        }).ToList();

                    return yearData;
            }

            return new List<ShotStatModel>();
        }
    }
}