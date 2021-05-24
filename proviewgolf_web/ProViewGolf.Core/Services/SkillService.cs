using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;

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
    }
}