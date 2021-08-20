using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProViewGolf.Core.Dbo.Entities 
{
    public enum BookingStatus{
        Active = 1,
        Cancelled =2 ,
        Completed =3,
        Pending = 4
    }
    public class Booking
    {
        [Key]
        public long BookingId {get;set;}
        public int Status {get;set;} = (int)BookingStatus.Active;
         public DateTime BookingDate {get;set;}
        public TimeSpan TimeSlot { get; set; }
        public long StudentRefId { get; set; }
        public virtual Student Student { get; set; }
        public long ProRefId { get; set; }
         public virtual Pro Pro { get; set; }
    }
}