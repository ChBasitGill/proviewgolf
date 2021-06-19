using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo
{
    public interface IProGolfContext
    {
        DbSet<ClubPractice> ClubPractices { get; set; }
        DbSet<ShotPractice> ShotPractices { get; set; }
        DbSet<Session> Sessions { get; set; }

        DbSet<Skill> Skills { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<Equipment> Equipments { get; set; }
        DbSet<Invitation> Invitations { get; set; }

        DbSet<User> Users { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Pro> Pros { get; set; }
        DbSet<Review> Reviews { get; set; }


        // DbContext Methods **************************************************************************
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}