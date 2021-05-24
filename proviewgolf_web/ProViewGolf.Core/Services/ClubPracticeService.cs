using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Dbo.Models;

namespace ProViewGolf.Core.Services
{
    public class ClubPracticeService
    {
        private readonly IProGolfContext _dbo;
        private readonly IMapper _mapper;

        public ClubPracticeService(IProGolfContext context, IMapper mapper)
        {
            _dbo = context;
            _mapper = mapper;
        }

        public bool AddOrUpdate(ClubPracticeModel model, out string msg)
        {
            try
            {
                foreach (var club in model.ClubRecords)
                {
                    if (club.AvgDistance <= 0) continue;

                    var entity = _mapper.Map<ClubRecordModel, ClubPractice>(club);
                    entity.DateTime = model.DateTime;
                    entity.StudentId = model.StudentId;
                    entity.IsWithPro = model.IsWithPro;

                    var old = _dbo.ClubPractices.AsNoTracking().FirstOrDefault(x =>
                        x.DateTime.Date == model.DateTime.Date && x.StudentId == model.StudentId &&
                        x.IsWithPro == model.IsWithPro && x.Club == club.Club && x.Ground == club.Ground);
                        
                        
                        
                    //    ?? new ClubPractice
                    //{
                    //    DateTime = model.DateTime, StudentId = model.StudentId,
                    //    Club = club.Club, Ground = club.Ground,
                    //};

                    //if (old != null)
                    //{
                    //    entity.ClubPracticeId = old.ClubPracticeId;
                    //    entity.Record = old.Record;
                    //    entity.RecordWithPro = old.RecordWithPro;
                    //}

                    //if (model.IsWithPro)
                    //    entity.RecordWithPro = clubRecord;
                    //else
                    //    entity.Record = clubRecord;

                    //if (entity.ClubPracticeId > 0)
                    //    _dbo.ClubPractices.Update(entity);
                    //else
                    //    _dbo.ClubPractices.Add(entity);


                    if (old != null)
                        entity.ClubPracticeId = old.ClubPracticeId;

                    _dbo.ClubPractices.AddOrUpdate(entity, entity.ClubPracticeId);



                    //if (_dbo.ClubPractices.Any(x =>
                    //    x.DateTime.Date == club.DateTime.Date && x.StudentId == club.StudentId &&
                    //    x.Club == club.Club && x.Ground == club.Ground))
                    //{
                    //    var updatedClub = _dbo.ClubPractices.AsNoTracking().FirstOrDefault(x =>
                    //        x.DateTime.Date == club.DateTime.Date && x.StudentId == club.StudentId &&
                    //        x.Club == club.Club && x.Ground == club.Ground);

                    //    updatedClub.Rating = club.Rating;
                    //    if (club.ProRefId == 0)
                    //    {
                    //        updatedClub.AvgDistance = club.AvgDistance;
                    //        updatedClub.ClubHeadSpeed = club.ClubHeadSpeed;
                    //        updatedClub.SpinRate = club.SpinRate;
                    //        updatedClub.SmashFactor = club.SmashFactor;
                    //        updatedClub.BallsAmount = club.BallsAmount;
                    //    }
                    //    else
                    //    {
                    //        updatedClub.AvgDistanceWithPro = club.AvgDistance;
                    //        updatedClub.ClubHeadSpeedWithPro = club.ClubHeadSpeed;
                    //        updatedClub.SpinRateWithPro = club.SpinRate;
                    //        updatedClub.SmashFactorWithPro = club.SmashFactor;
                    //        updatedClub.BallsAmountWithPro = club.BallsAmount;
                    //        updatedClub.ProRefId = club.ProRefId;
                    //    }

                    //    _dbo.ClubPractices.Update(club);
                    //}
                    //else
                    //{
                    //    _dbo.ClubPractices.Add(club);
                    //}

                }

                msg = "club score successfully updated";
                _dbo.SaveChanges();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }

            return true;
        }

        public List<ClubRecordModel> IronScore(long studentId, DateTime date, long userId)
        {
            var isWithPro = _dbo.Pros.Any(x => x.UserId == userId);

            var data = _dbo.ClubPractices.Where(x =>
                x.StudentId == studentId && x.DateTime.Date == date.Date && x.IsWithPro == isWithPro &&
                (int) x.Club > 100 && (int) x.Club < 200).ToList();

            return data.Select(r => _mapper.Map<ClubRecordModel>(r)).ToList();

            // return ClubRecords(data, isWithPro);
        }

        public List<ClubRecordModel> WoodScore(long studentId, DateTime date, long userId)
        {
            var isWithPro = _dbo.Pros.Any(x => x.UserId == userId);

            var data = _dbo.ClubPractices.Where(x =>
                x.StudentId == studentId && x.DateTime.Date == date.Date && x.IsWithPro == isWithPro &&
                (int) x.Club > 200 && (int) x.Club < 300).ToList();

            return data.Select(r => _mapper.Map<ClubRecordModel>(r)).ToList();

            //return ClubRecords(data, isWithPro);
        }


        //private static List<ClubRecordModel> ClubRecords(IEnumerable<ClubPractice> data, bool isWithPro)
        //{
        //    if (isWithPro)
        //    {
        //        return data.Select(x => new ClubRecordModel
        //        {
        //            Ground = x.Ground,
        //            Club = x.Club,
        //            BallsAmount = x.RecordWithPro.BallsAmount,
        //            AvgDistance = x.RecordWithPro.AvgDistance,
        //            ClubHeadSpeed = x.RecordWithPro.ClubHeadSpeed,
        //            Apex = x.RecordWithPro.Apex,
        //            SpinRate = x.RecordWithPro.SpinRate,
        //            Rating = x.RecordWithPro.Rating
        //        }).ToList();
        //    }

        //    return data.Select(x => new ClubRecordModel
        //    {
        //        Ground = x.Ground,
        //        Club = x.Club,
        //        BallsAmount = x.Record.BallsAmount,
        //        AvgDistance = x.Record.AvgDistance,
        //        ClubHeadSpeed = x.Record.ClubHeadSpeed,
        //        Apex = x.Record.Apex,
        //        SpinRate = x.Record.SpinRate,
        //        Rating = x.Record.Rating
        //    }).ToList();
        //}
    }
}