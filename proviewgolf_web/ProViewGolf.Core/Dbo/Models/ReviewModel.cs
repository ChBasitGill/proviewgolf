using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo.Models
{
public class ReviewModel
    {
        public long ReviewId {get;set;}
        public decimal Grip{ get; set; }
        public decimal Alignment{ get; set; }
        public decimal Flexibility{ get; set; }
        public decimal Fitness{ get; set; }
        public decimal BallPosition{ get; set; }
        public decimal HandPosition{ get; set; }
        public decimal Stance{ get; set; }
        public decimal TakeAway{ get; set; }
        public decimal HeadMovement{ get; set; }
        public decimal GripPressure{ get; set; }
        public decimal FollowThrough{ get; set; }
        public decimal Realease{ get; set; }
        public decimal Folding{ get; set; }
        public decimal FinishPosition{ get; set; }
        public decimal PuttingTechnique{ get; set; }
        public decimal Dipping{ get; set; }
        public decimal WeightTransfer{ get; set; }
        public decimal Concentration{ get; set; }
        public decimal MentalStrength{ get; set; }
        public decimal Etiquette{ get; set; }
        public decimal GolfRules{ get; set; }
        public decimal CourseManagement{ get; set; }
        public decimal PaceOfGame{ get; set; }
        public decimal ControlBall{ get; set; }
        public decimal PlayingPunch{ get; set; }
        public decimal BackspinControl{ get; set; }
        public decimal SwippingTheBall{ get; set; }
        public decimal Overswinging{ get; set; }
        public decimal Looping{ get; set; }
        public decimal ReverseWeight{ get; set; }
        public decimal Bowing{ get; set; }
        public decimal Blocking{ get; set; }
        public decimal Lifting{ get; set; }
        public decimal LawBallFight{ get; set; }
          public long StudentRefId { get; set; }
        public long ProRefId { get; set; }
    }
}