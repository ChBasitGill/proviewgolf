using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProViewGolf.Core.Dbo.Entities
{
    public class Session
    {
        [Key] public long SessionId { get; set; }
        public DateTime Start { get; set; } = DateTime.UtcNow;
        public DateTime End { get; set; } = DateTime.UtcNow;

        public long StudentRefId { get; set; }
        [ForeignKey("StudentRefId")] public virtual Student Student { get; protected set; }

        public long ProRefId { get; set; }
        [ForeignKey("ProRefId")] public virtual Pro Pro { get; protected set; }
    }
}