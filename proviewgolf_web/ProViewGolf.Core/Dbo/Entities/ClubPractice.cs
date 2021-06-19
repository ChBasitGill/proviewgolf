using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProViewGolf.Core.Dbo.Entities
{
    public enum ClubGroup
    {
        ShortIron = 1, MediumIron = 2, LongIron = 3, Wood = 4
    }

    public enum Ground
    {
        Grass = 1,
        Mat = 2,
        Turf = 3
    }

    public enum Club
    {
        // IRON TYPES ***********************
        IronHybrid2 = 101,
        IronHybrid3 = 102,
        IronHybrid4 = 103,
        IronHybrid5 = 104,
        Iron6 = 105,
        Iron7 = 106,
        Iron8 = 107,
        Iron9 = 108,
        PitchingWedge = 109,
        GapWedge = 110,
        SandWedge = 111,
        LobWedge = 112,

        // WOOD TYPES ***********************
        Driver = 201,
        Wood3 = 202,
        Wood5 = 203,
        Wood7 = 204,
        Rescue = 205
    }

    [Owned]
    public class ClubRecord
    {
        public float AvgDistance { get; set; } = 0.0f;
        public float ClubHeadSpeed { get; set; } = 0.0f;
        public float SpinRate { get; set; } = 0.0f;
        public float Apex { get; set; } = 0.0f;
        public float BallsAmount { get; set; } = 0.0f;
        public float Rating { get; set; }
    }

    public class ClubPractice
    {
        [Key] public long ClubPracticeId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsWithPro { get; set; } = false;

        public Ground Ground { get; set; } = Ground.Grass;
        public Club Club { get; set; }

        public float AvgDistance { get; set; } = 0.0f;
        public float ClubHeadSpeed { get; set; } = 0.0f;
        public float SpinRate { get; set; } = 0.0f;
        public float Apex { get; set; } = 0.0f;
        public float BallsAmount { get; set; } = 0.0f;
        public float Rating { get; set; }




        //public ClubRecord Record { get; set; } = new ClubRecord();
        //public ClubRecord RecordWithPro { get; set; } = new ClubRecord();

        public long StudentId { get; set; }
        //[ForeignKey("StudentId")] 
        public virtual Student Student { get; protected set; }


        //public float AvgDistance { get; set; } = 0.0f;
        //public float ClubHeadSpeed { get; set; } = 0.0f;
        //public float SpinRate { get; set; } = 0.0f;
        //public float SmashFactor { get; set; } = 0.0f;
        //public float BallsAmount { get; set; } = 0.0f;

        //public float AvgDistanceWithPro { get; set; } = 0.0f;
        //public float ClubHeadSpeedWithPro { get; set; } = 0.0f;
        //public float SpinRateWithPro { get; set; } = 0.0f;
        //public float SmashFactorWithPro { get; set; } = 0.0f;
        //public float BallsAmountWithPro { get; set; } = 0.0f;

        //public float Rating { get; set; }




        //public long? ProRefId { get; set; }
        //[ForeignKey("ProRefId")] public virtual User Pro { get; set; }
    }
}