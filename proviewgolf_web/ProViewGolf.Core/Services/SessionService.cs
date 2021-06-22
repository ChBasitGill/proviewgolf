using System;
using System.Linq;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Services
{
    public class SessionService
    {
        private readonly IProGolfContext _dbo;

        public SessionService(IProGolfContext context)
        {
            _dbo = context;
        }

        public long StartSession(long proId, long studentId, out string msg)
        {
            msg = "session started"; 

            try
            {
                var session = new Session
                {
                    ProRefId = proId,
                    StudentRefId = studentId
                };

                _dbo.Sessions.Add(session);
                _dbo.SaveChanges();

                return session.SessionId;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return 0;
            }
        }

        public bool EndSession(long sessionId, out string msg)
        {
            try
            {
                var session = _dbo.Sessions.SingleOrDefault(x => x.SessionId == sessionId);
                if (session != null)
                {
                    session.End = DateTime.UtcNow;
                    _dbo.Sessions.Update(session);
                    msg = "Session stopped";
                    return _dbo.SaveChanges() > 0;
                }

                msg = "No Session found";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
    }
}