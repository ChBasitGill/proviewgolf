using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;

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
        public decimal WarmupTime { get; set; }
        public decimal DriverPeaces { get; set; }
        public decimal IronPeaces { get; set; }
        public decimal ChipPeaces { get; set; }
        public decimal SandPeaces { get; set; }
        public decimal PuttPeaces { get; set; }
        public string GolfCourse { get; set; }
        public decimal ExactHcp { get; set; }
        public decimal PlayingHcp { get; set; }
        public bool Nervous { get; set; }
        public decimal FlightPartnersRating { get; set; }

        public decimal DriversRating { get; set; }
        public decimal DriversLeft { get; set; }
        public decimal DriversCenter { get; set; }
        public decimal DriversRight { get; set; }

        public decimal IronsRating { get; set; }
        public decimal IronsLeft { get; set; }
        public decimal IronsCenter { get; set; }
        public decimal IronsRight { get; set; }

        public decimal WoodsRating { get; set; }
        public decimal WoodsLeft { get; set; }
        public decimal WoodsCenter { get; set; }
        public decimal WoodsRight { get; set; }

        public decimal ShortIronGameRating { get; set; }
        public decimal BunkerShortsRating { get; set; }
        public decimal PuttingStrokes { get; set; }
        public decimal GreenSpeedRating { get; set; }

        public decimal StableForPoints { get; set; }
        public decimal Strokes { get; set; }
        public decimal NewHcp { get; set; }
        public bool Walking { get; set; }
        public decimal DistanceWalked { get; set; }
        public decimal GameDuration { get; set; }
        public Holes Holes { get; set; }

        public long StudentId { get; set; }
      //  [ForeignKey("StudentId")]
         public virtual Student Student { get; protected set; }
    }
public class GamesAverages
    {
        public decimal WarmupTime { get; set; }
        public decimal DriverPeaces { get; set; }
        public decimal IronPeaces { get; set; }
        public decimal ChipPeaces { get; set; }
        public decimal SandPeaces { get; set; }
        public decimal PuttPeaces { get; set; }
        public decimal ExactHcp { get; set; }
        public decimal PlayingHcp { get; set; }
        public decimal FlightPartnersRating { get; set; }

        public decimal DriversRating { get; set; }
        public decimal DriversLeft { get; set; }
        public decimal DriversCenter { get; set; }
        public decimal DriversRight { get; set; }

        public decimal IronsRating { get; set; }
        public decimal IronsLeft { get; set; }
        public decimal IronsCenter { get; set; }
        public decimal IronsRight { get; set; }

        public decimal WoodsRating { get; set; }
        public decimal WoodsLeft { get; set; }
        public decimal WoodsCenter { get; set; }
        public decimal WoodsRight { get; set; }

        public decimal ShortIronGameRating { get; set; }
        public decimal BunkerShortsRating { get; set; }
        public decimal PuttingStrokes { get; set; }
        public decimal GreenSpeedRating { get; set; }

        public decimal StableForPoints { get; set; }
        public decimal Strokes { get; set; }
        public decimal NewHcp { get; set; }
        public decimal DistanceWalked { get; set; }

    }


public class GamesGrouping{
        public string Text {get;set;}
        public GamesAverages Average {get;set;}
    }
        public class GameDto  
        {
            public GameType GameType {get;set;}
            public Game Game {get;set;}
            public GamesAverages Averages {get;set;}

            public IEnumerable<GamesGrouping> MonthlyGrouping {get;set;}
            public IEnumerable<GamesGrouping> YearlyGrouping {get;set;}
            public GamesGrouping WeeklyGrouping {get;set;}
            
        }

}