using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Dbo.Models;
using Profile = AutoMapper.Profile;

namespace ProViewGolf.Core.Platform
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClubRecordModel, ClubPractice>().ReverseMap();
            CreateMap<ShotRecordModel, ShotPractice>().ReverseMap();

            CreateMap<User, LoginModel>();
            CreateMap<Review, ReviewModel>();
            CreateMap<ReviewModel, Review>();

            CreateMap<User, Student>();
            CreateMap<User, Pro>();
        }
    }
}