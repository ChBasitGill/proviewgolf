using System;

namespace ProViewGolf.Core.Dbo.Entities
{
    public enum InvitationStatus
    {
        Approved = 1,
        Rejected = 2,
        Pending = 3
    }

    public class Invitation
    {
        public long InvitationId { get; protected set; }

        public string StudentEmail { get; set; }
        public string InstructorEmail { get; set; }

        public string Code { get; set; } = Guid.NewGuid().ToString();
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
    }
}
