using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProViewGolf.Core.Dbo.Entities
{
    public enum GameType
    {
        Tournament = 1,
        PlayRounds = 2
    }

    public enum Holes
    {
        H9 = 1,
        H18 = 2
    }

    public class Game
    {
        [Key] public long GameId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public GameType GameType { get; set; }
        public int WarmupTime { get; set; }
        public int DriverPeaces { get; set; }
        public int IronPeaces { get; set; }
        public int ChipPeaces { get; set; }
        public int SandPeaces { get; set; }
        public int PuttPeaces { get; set; }
        public string GolfCourse { get; set; }
        public float ExactHcp { get; set; }
        public int PlayingHcp { get; set; }
        public bool Nervous { get; set; }
        public float FlightPartnersRating { get; set; }

        public float DriversRating { get; set; }
        public int DriversLeft { get; set; }
        public int DriversCenter { get; set; }
        public int DriversRight { get; set; }

        public float IronsRating { get; set; }
        public int IronsLeft { get; set; }
        public int IronsCenter { get; set; }
        public int IronsRight { get; set; }

        public float WoodsRating { get; set; }
        public int WoodsLeft { get; set; }
        public int WoodsCenter { get; set; }
        public int WoodsRight { get; set; }

        public float ShortIronGameRating { get; set; }
        public float BunkerShortsRating { get; set; }
        public int PuttingStrokes { get; set; }
        public float GreenSpeedRating { get; set; }

        public int StableFordPoints { get; set; }
        public int Strokes { get; set; }
        public float NewHcp { get; set; }
        public bool Walking { get; set; }
        public int DistanceWalked { get; set; }
        public int GameDuration { get; set; }
        public Holes Holes { get; set; }

        public long StudentId { get; set; }
        [ForeignKey("StudentId")] public virtual Student Student { get; protected set; }
    }
}