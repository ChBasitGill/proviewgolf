using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ProViewGolf.Core.Dbo.Entities
{
    public enum DistanceUnit
    {
        Meters = 1,
        Yards = 2
    }

    public enum SpeedUnit
    {
        Kmh = 1,
        Mph = 2
    }

    public enum Role
    {
        Student = 1,
        Pro = 2,
    }


    [Owned]
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public int Hcp { get; set; }
        public string HealthLimitation { get; set; }

        public DistanceUnit DistanceUnit { get; set; }
        public SpeedUnit SpeedUnit { get; set; }
    }

    public class Student : User
    {
        //public long StudentId { get; set; }

        public int ProViewHcp { get; set; } = 0;
        public int ProViewLevel { get; set; } = 0;

        public long ProRefId { get; set; }
       // [ForeignKey("ProRefId")] 
        public virtual Pro Pro { get; set; }

        //public long UserRefId { get; set; }
        //[ForeignKey("UserRefId")] public virtual User User { get; set; }
    }

    public class Pro : User
    {
        //public long ProId { get; set; }
        //public Profile Profile { get; set; } = new Profile();
        public List<Student> Students { get; set; } = new List<Student>();

        //public long UserRefId { get; set; }
        //[ForeignKey("UserRefId")] public virtual User User { get; set; }
    }

    public class User
    {
        [Key] public long UserId { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Profile Profile { get; set; } = new Profile();

        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool Gender { get; set; }
        //public int Age { get; set; }
        //public int HcpNo { get; set; }
        //public int ProViewHcp { get; set; } = 0;
        //public UserType UserType { get; set; }
        //public DistanceUnit DistanceUnit { get; set; }
        //public SpeedUnit SpeedUnit { get; set; }
        //public string HealthLimitation { get; set; }

        [JsonIgnore] public bool AccountVerified { get; set; } = false;
        [JsonIgnore] public string VerificationToken { get; set; }
        [JsonIgnore] public DateTime VerificationTokenExpiry { get; set; }
    }
}