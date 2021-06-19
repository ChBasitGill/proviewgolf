using Microsoft.EntityFrameworkCore;
using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo
{
    public abstract class BaseProGolfContext : DbContext, IProGolfContext
    {
        public DbSet<ClubPractice> ClubPractices { get; set; }
        public DbSet<ShotPractice> ShotPractices { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Pro> Pros { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }

    public static class DbSetExtensions
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, T entity, dynamic key) where T : class
        {
            if (key > 0)
            {
                dbSet.Update(entity);
                return;
            }

            dbSet.Add(entity);
        }

        public static void Remove<T>(this DbSet<T> dbSet, dynamic key) where T : class
        {
            var e = dbSet.Find(key);
            if (e == null) return;
            dbSet.Remove(dbSet.Find(key));
        }
    }
}