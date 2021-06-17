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
        public int WarmupTime { get; set; }
        public int DriverPeaces { get; set; }
        public int IronPeaces { get; set; }
        public int ChipPeaces { get; set; }
        public int SandPeaces { get; set; }
        public int PuttPeaces { get; set; }
        public string GolfCourse { get; set; }
        public double ExactHcp { get; set; }
        public int PlayingHcp { get; set; }
        public bool Nervous { get; set; }
        public double FlightPartnersRating { get; set; }

        public double DriversRating { get; set; }
        public int DriversLeft { get; set; }
        public int DriversCenter { get; set; }
        public int DriversRight { get; set; }

        public double IronsRating { get; set; }
        public int IronsLeft { get; set; }
        public int IronsCenter { get; set; }
        public int IronsRight { get; set; }

        public double WoodsRating { get; set; }
        public int WoodsLeft { get; set; }
        public int WoodsCenter { get; set; }
        public int WoodsRight { get; set; }

        public double ShortIronGameRating { get; set; }
        public double BunkerShortsRating { get; set; }
        public int PuttingStrokes { get; set; }
        public double GreenSpeedRating { get; set; }

        public int StableFordPoints { get; set; }
        public int Strokes { get; set; }
        public double NewHcp { get; set; }
        public bool Walking { get; set; }
        public int DistanceWalked { get; set; }
        public int GameDuration { get; set; }
        public Holes Holes { get; set; }

        public long StudentId { get; set; }
        [ForeignKey("StudentId")] public virtual Student Student { get; protected set; }
    }
public class GamesAverages
    {
        public double WarmupTime { get; set; }
        public double DriverPeaces { get; set; }
        public double IronPeaces { get; set; }
        public double ChipPeaces { get; set; }
        public double SandPeaces { get; set; }
        public double PuttPeaces { get; set; }
        public double ExactHcp { get; set; }
        public double PlayingHcp { get; set; }
        public double FlightPartnersRating { get; set; }

        public double DriversRating { get; set; }
        public double DriversLeft { get; set; }
        public double DriversCenter { get; set; }
        public double DriversRight { get; set; }

        public double IronsRating { get; set; }
        public double IronsLeft { get; set; }
        public double IronsCenter { get; set; }
        public double IronsRight { get; set; }

        public double WoodsRating { get; set; }
        public double WoodsLeft { get; set; }
        public double WoodsCenter { get; set; }
        public double WoodsRight { get; set; }

        public double ShortIronGameRating { get; set; }
        public double BunkerShortsRating { get; set; }
        public double PuttingStrokes { get; set; }
        public double GreenSpeedRating { get; set; }

        public double StableFordPoints { get; set; }
        public double Strokes { get; set; }
        public double NewHcp { get; set; }
        public double DistanceWalked { get; set; }

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