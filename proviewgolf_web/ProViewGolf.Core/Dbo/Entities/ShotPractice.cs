using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProViewGolf.Core.Dbo.Entities
{
    public enum ShotGroup
    {
        PuttingRun = 1,
        Bunker = 2,
        PitchChip = 3,
        OnCourse = 4
    }

    public enum ShotCategory
    {
        Putt = 10,
        ChipRun = 20,
        GreenSide = 30,
        Fairway = 40,
        Pitch = 50,
        Chip = 60,
        PlayHole = 70,
        Scrambling = 80
    }

    public struct Shot
    {
        public struct Putt
        {
            public static int M1 = 11;
            public static int M2 = 12;
            public static int M3 = 13;
            public static int M5 = 14;
            public static int M10 = 15;
            public static int M15 = 16;
            public static int M20 = 17;
            public static int M25 = 18;
        }

        public struct ChipRun
        {
            public static int SandWedge = 21;
            public static int GapWedge = 22;
            public static int PitchingWedge = 23;
            public static int Iron9 = 24;
            public static int Iron8 = 25;
            public static int Iron7 = 26;
        }

        public struct GreenSide
        {
            public static int LongHigh = 31;
            public static int ShortHigh = 32;
            public static int LongFlat = 33;
            public static int ShortFlat = 34;
            public static int FriedEggLie = 35;
            public static int DownHillLie = 36;
        }

        public struct Fairway
        {
            public static int RecoveryShot = 41;
            public static int CleaningLip = 42;
            public static int LongStraight = 43;
            public static int CutShot = 44;
            public static int SkimmerToGreen = 45;
        }

        public struct Pitch
        {
            public static int M5 = 51;
            public static int M10 = 52;
            public static int M15 = 53;
            public static int M20 = 54;
            public static int M25 = 55;
            public static int M30 = 56;
            public static int M50 = 57;
            public static int M75 = 58;
        }

        public struct Chip
        {
            public static int M5 = 61;
            public static int M10 = 62;
            public static int M15 = 63;
            public static int M20 = 64;
            public static int M25 = 65;
            public static int M30 = 66;
            public static int M40 = 67;
            public static int M50 = 68;
        }

        public struct PlayHole
        {
            public static int TeeShot = 71;
            public static int ApproachShot = 72;
            public static int FairwayBunker = 73;
            public static int GreenSideBunker = 74;
            public static int PuttingShot = 75;
            public static int PuttingLong = 76;
        }

        public struct Scrambling
        {
            public static int RoughShot = 81;
            public static int PunchShot = 82;
            public static int ChipRunShot = 83;
            public static int RecoveryShot = 84;
            public static int SideHillLay = 85;
            public static int UpDownHillLay = 86;
            public static int ChipShot = 87;
            public static int PitchShot = 88;
        }
    }

    public class ShotPractice
    {
        [Key] public long ShotPracticeId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public bool IsWithPro { get; set; } = false;

        public ShotCategory ShotCategory { get; set; }
        public int ShotType { get; set; } // like Shot.Putt.M1

        public int Shots { get; set; } = 0;
        public int GoodShots { get; set; } = 0;

        //public int ShotsWithPro { get; set; } = 0;
        //public int GoodShotsWithPro { get; set; } = 0;

        public float Rating { get; set; }

        public long StudentId { get; set; }
        [ForeignKey("StudentId")] public virtual Student Student { get; protected set; }


        //public int Putt { get; set; } = 0;
        //public int PuttWithPro { get; set; } = 0;

        //public int Holed { get; set; } = 0;
        //public int HoledWithPro { get; set; } = 0;

        //public float Rating { get; set; }

        //public long UserRefId { get; set; }
        //[ForeignKey("UserRefId")] public virtual User User { get; set; }

        //public long? ProRefId { get; set; }
        //[ForeignKey("ProRefId")] public virtual User Pro { get; set; }
    }
}