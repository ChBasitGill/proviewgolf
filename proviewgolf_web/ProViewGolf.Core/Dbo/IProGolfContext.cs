using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo
{
    public interface IProGolfContext
    {
        public DbSet<ClubPractice> ClubPractices { get; set; }
        public DbSet<ShotPractice> ShotPractices { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Pro> Pros { get; set; }


        // DbContext Methods **************************************************************************
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}