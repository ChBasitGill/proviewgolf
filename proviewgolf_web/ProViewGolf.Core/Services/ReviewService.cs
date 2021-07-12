using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using System.Globalization;

namespace ProViewGolf.Core.Services
{
    public class ReviewService
    {
        private readonly IProGolfContext _dbo;

        public ReviewService(IProGolfContext context)
        {
            _dbo = context;
        }

        public bool AddOrUpdate(Review model, out string msg)
        {
            try
            {
                var entity = _dbo.Reviews.AsNoTracking().FirstOrDefault(x =>
                 x.StudentRefId == model.StudentRefId && x.ProRefId == model.ProRefId);

                model.ReviewId = entity?.ReviewId ?? 0;
                _dbo.Reviews.AddOrUpdate(model, model.ReviewId);

                msg = "Review updated successfully";
                _dbo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public Review Reviews(long studentId, long proid)
        {
            // its working here
            return _dbo.Reviews.FirstOrDefault(x => x.StudentRefId == studentId && x.ProRefId == proid);
        }
        public decimal ReviewAverage(long studentId, long proid)
        {
            // its working here
            var review = _dbo.Reviews.FirstOrDefault(x => x.StudentRefId == studentId && x.ProRefId == proid);
            if(review == null)
            {
                return 0;
            }
            var sum =review.Alignment +
                review.BackspinControl +
                review.BallPosition +
                review.Blocking +
                review.Bowing +
                review.Concentration +
                review.ControlBall +
                review.CourseManagement +
                review.Dipping +
                review.Etiquette +
                review.FinishPosition +
                review.Fitness +
                review.Flexibility +
                review.Folding +
                review.FollowThrough +
                review.GolfRules +
                review.Grip +
                review.GripPressure +
                review.HandPosition +
                review.HeadMovement +
                review.LawBallFight +
                review.Lifting +
                review.Looping +
                review.MentalStrength +
                review.Overswinging +
                review.PaceOfGame +
                review.PlayingPunch +
                review.PuttingTechnique +
                review.Realease +
                review.ReverseWeight +
                review.Stance +
                review.SwippingTheBall +
                review.TakeAway +
                review.WeightTransfer;
            return Decimal.Divide(sum,24);
        }
    }
}