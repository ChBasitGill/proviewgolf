using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Helpers;

namespace ProViewGolf.Core.Services
{
    public class InstructorService
    {
        private readonly IProGolfContext _dbo;

        public InstructorService(IProGolfContext context)
        {
            _dbo = context;
        }

        public dynamic Students(long proId)
        {
            return _dbo.Students.Where(x => x.ProRefId == proId).Select(x => new
            {
                x.UserId, x.Email,
                x.Profile.FirstName, x.Profile.LastName
            }).ToList();
        }

        public dynamic Instructor(long studentId)
        {
            var pro = _dbo.Students.Include(o => o.Pro).FirstOrDefault(x => x.UserId == studentId)?.Pro;
            if (pro == null) return null;

            return new
            {
                pro.UserId,
                pro.Profile.LastName,
                pro.Profile.FirstName,
                pro.Email
            };
        }


        public bool Unassigned(long studentId, out string msg)
        {
            msg = "successfully unassigned!";
            var student = _dbo.Students.Find(studentId);

            if (student == null)
            {
                msg = "no result found";
                return false;
            }

            student.ProRefId = 0;
            _dbo.Students.Update(student);
            return _dbo.SaveChanges() > 0;
        }


        public bool AddStudent(string email, long proId, out string msg)
        {
            msg = string.Empty;

            try
            {
                var student = _dbo.Students.FirstOrDefault(x => x.Email == email);
                var instructor = _dbo.Pros.FirstOrDefault(x => x.UserId == proId);
                if (instructor == null) return false;

                var invitation =
                    _dbo.Invitations.FirstOrDefault(x =>
                        x.InstructorEmail == instructor.Email && x.StudentEmail == email) ?? new Invitation()
                    {
                        StudentEmail = email,
                        InstructorEmail = instructor.Email,
                    };

                invitation.Code = Guid.NewGuid().ToString();

                var acceptLink = @"https://proviewgolf-web.azurewebsites.net/api/Auth/Invitation/" + invitation.Code + "/1";
                var rejectLink = @"https://proviewgolf-web.azurewebsites.net/api/Auth/Invitation/" + invitation.Code + "/0";

                var subject = "ProView Golf Invitation";
                var body = "Hello Student" + /*student.Profile.FirstName +*/ "!<br/><p>Mr. " +
                           instructor.Profile.FirstName +
                           " has sent you an invitation.</p><br/><a href=" + acceptLink +
                           ">Accept</a> <a href=" + rejectLink + ">Reject</a><br>";
                EmailSender.SendEmail(student?.Profile.FirstName ?? "Student", email, subject, body);
                msg = "invitation successfully sent!";

                _dbo.Invitations.AddOrUpdate(invitation, invitation.InvitationId);
                return _dbo.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool AddPro(string email, long studentId, out string msg)
        {
            msg = string.Empty;

            try
            {
                var pro = _dbo.Pros.FirstOrDefault(x => x.Email == email);
                var student = _dbo.Students.FirstOrDefault(x => x.UserId == studentId);
                if (student == null) return false;

                var invitation =
                    _dbo.Invitations.FirstOrDefault(x =>
                        x.InstructorEmail == email && x.StudentEmail == student.Email) ?? new Invitation
                    {
                        StudentEmail = student.Email,
                        InstructorEmail = email,
                    };

                invitation.Code = Guid.NewGuid().ToString();

                var acceptLink = @"https://proviewgolf-web.azurewebsites.net/api/Auth/Invitation/" + invitation.Code + "/1";
                var rejectLink = @"https://proviewgolf-web.azurewebsites.net/api/Auth/Invitation/" + invitation.Code + "/0";
                var subject = "ProView Golf Invitation";
                var body = "Hello Instructor" + "!<br/><p>Mr. " + student.Profile.FirstName +
                           " has sent you an invitation.</p><br/><a href=" + acceptLink +
                           ">Accept</a>  <a href=" + rejectLink + ">Reject</a><br>";
                EmailSender.SendEmail(pro?.Profile.FirstName ?? "Instructor", email, subject, body);
                msg = "invitation successfully sent!";

                _dbo.Invitations.AddOrUpdate(invitation, invitation.InvitationId);
                return _dbo.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public dynamic StudentProfile(long proId, long studentId)
        {
            var student = _dbo.Students.Find(studentId);
            var clubs = _dbo.ClubPractices.Where(x => x.StudentId == studentId);
            var shots = _dbo.ShotPractices.Where(x => x.StudentId == studentId);
            var games = _dbo.Games.Where(x => x.StudentId == studentId);
            var sessions = _dbo.Sessions.Where(x => x.ProRefId == proId && x.StudentRefId == studentId)
                .OrderByDescending(x => x.Start).ToList();

            return new
            {
                ProViewLevel = student?.ProViewLevel,
                ProViewHCP = student?.ProViewHcp,
                CurrentHCP = student?.Profile.Hcp,

                ProLessons = sessions.Sum(x => (x.End - x.Start).TotalHours).Round(1),
                LastProLesson = sessions.FirstOrDefault()?.Start,

                BallsWithPro = clubs.Where(x => x.IsWithPro).Sum(x => x.BallsAmount) +
                               shots.Where(x => x.ShotCategory != ShotCategory.Putt && x.IsWithPro).Sum(x => x.Shots),

                BallsByStudent = clubs.Sum(x => x.BallsAmount) +
                                 shots.Where(x => x.ShotCategory != ShotCategory.Putt).Sum(x => x.Shots),

                Played18Holes = games.Count(x => x.Holes == Holes.H18),
                Played9Holes = games.Count(x => x.Holes == Holes.H9),
                PlayedTorunments = games.Count(x => x.GameType == GameType.Tournament),
                VideoAnalysis = _dbo.Skills.Sum(x => x.VideoSwingAnalysis) / 60
            };
        }
        public dynamic InstructorPro(long StudentId)
        {
            var pro = _dbo.Students.Include(o => o.Pro).FirstOrDefault(x => x.UserId == StudentId);

            if (pro.ProRefId == null) return null;
            else
            {
                var pro2 = _dbo.Pros.FirstOrDefault(x => x.UserId == pro.ProRefId);
                return new
                {
                    pro2.UserId,
                    pro2.Profile.LastName,
                    pro2.Profile.FirstName,
                    pro2.Email,
                    pro2.Profile.Age,
                    pro2.Profile.Gender,
                    pro2.Profile.Hcp
                };
            }

        }
    }
}