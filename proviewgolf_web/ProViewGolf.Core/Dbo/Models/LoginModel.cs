using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo.Models
{
    public class LoginModel
    {
        public long UserId { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public Profile Profile { get; set; }

        public int ProViewHcp { get; set; }
        public int ProViewLevel { get; set; }

        public string Token { get; set; }
    }
}
