using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProViewGolf.Core.Dbo;

namespace ProViewGolf.DataLayer
{

    public class ProGolfContext : BaseProGolfContext
    {
        private readonly IConfiguration _configuration;
      
        public ProGolfContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("Default"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
