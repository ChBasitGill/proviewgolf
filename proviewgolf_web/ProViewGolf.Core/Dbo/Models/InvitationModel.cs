using ProViewGolf.Core.Dbo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProViewGolf.Core.Dbo.Models
{
    public class InvitationModel
    {
        public string StudentEmail { get; set; }
        public string InstructorEmail { get; set; }

        public string Code { get; set; } = Guid.NewGuid().ToString();
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
        public Pro Pro { get; set; }
    }
}
