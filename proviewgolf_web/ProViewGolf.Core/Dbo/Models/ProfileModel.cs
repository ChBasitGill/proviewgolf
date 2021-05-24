using ProViewGolf.Core.Dbo.Entities;

namespace ProViewGolf.Core.Dbo.Models
{
    public class ProfileModel : Profile
    {
        public bool ChangePassword { get; set; } = false;
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class PasswordModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
