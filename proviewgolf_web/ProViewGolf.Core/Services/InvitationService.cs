using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Models;

namespace ProViewGolf.Core.Services
{
    public class InvitationService
    {
        private readonly IProGolfContext _dbo;

        public InvitationService(IProGolfContext context)
        {
            _dbo = context;
        }


        public List<InvitationModel> InvitationsDetails(long studentId)
        {
            // its working here
            var data = new List<InvitationModel>();
            var pros = _dbo.Pros.AsNoTracking().ToList();

            var studentEmail = _dbo.Students.Find(studentId).Email;

            var result = _dbo.Invitations.Where(x => x.StudentEmail == studentEmail).AsNoTracking().ToList();
            foreach (var item in result)
            {
                data.Add(new InvitationModel()
                {
                    Code = item.Code,
                    InstructorEmail = item.InstructorEmail,
                    Status = item.Status,
                    StudentEmail = item.StudentEmail,
                    Pro = pros.FirstOrDefault(x=>x.Email== item.InstructorEmail)
                });
            }
            return data;
        }
    }
    
}