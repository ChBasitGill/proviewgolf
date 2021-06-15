using System;
using System.Linq;
using AutoMapper;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Dbo.Models;
using ProViewGolf.Core.Helpers;
using Profile = ProViewGolf.Core.Dbo.Entities.Profile;

namespace ProViewGolf.Core.Services
{
    public class UserService
    {
        private readonly IProGolfContext _dbo;
        private readonly IMapper _mapper;

        public UserService(IProGolfContext context, IMapper mapper)
        {
            _dbo = context;
            _mapper = mapper;
        }

        public LoginModel Authenticate(PasswordModel model, out string msg)
        {
            msg = null;

            try
            {
                var user = _dbo.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                if (user == null)
                {
                    msg = "username or password didn't match";
                    return null;
                }

                if (!user.AccountVerified)
                {
                    msg = "please confirm your email";
                    return null;
                }

                var result = _mapper.Map<LoginModel>(user);
                if (user.Role == Role.Student)
                {
                    var student = _dbo.Students.Find(user.UserId);
                    result.ProViewHcp = student.ProViewHcp;
                    result.ProViewLevel = student.ProViewLevel;
                }

                return result;
            }
            catch (Exception e)
            {
                msg = e.Message;
                return null;
            }
        }

        public bool ConfirmEmail(string token, out string msg)
        {
            try
            {
                if (_dbo.Users.Any(x => x.VerificationToken == token))
                {
                    var user = _dbo.Users.FirstOrDefault(x => x.VerificationToken == token);
                    if (user?.VerificationTokenExpiry >= DateTime.UtcNow)
                    {
                        user.AccountVerified = true;
                        _dbo.Users.Update(user);

                        msg = "Email Confirmed! Thanks";
                        return _dbo.SaveChanges() > 0;
                    }

                    msg = "Token Expired";
                    return false;
                }

                msg = "Invalid Token";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool SignUp(User user, out string msg)
        {
            try
            {
                if (_dbo.Users.Any(x => x.Email == user.Email))
                {
                    msg = "user already exists with this email";
                    return false;
                }

                msg = "user added successfully";
                user.VerificationToken = Guid.NewGuid().ToString();
                user.VerificationTokenExpiry = DateTime.UtcNow.AddDays(3);

                if (user.Role == Role.Student)
                    _dbo.Students.Add(_mapper.Map<Student>(user));
                else
                    _dbo.Pros.Add(_mapper.Map<Pro>(user));

                var verificationLink = @"https://proviewgolf-web.azurewebsites.net/api/Auth/ConfirmEmail/" + user.VerificationToken;

                var body = @"<b>Dear " + user.Profile.FirstName + " " + user.Profile.LastName + ",</b><br>" +
                           "<p>Please click on the following link or open the link in your browser to confirm your email.</p><a href=" +
                           verificationLink + ">" + verificationLink + "</a><br><b>" +
                           "This link will expire in 3 days.</b>";

                EmailSender.SendEmail(user.Profile.FirstName, user.Email, "Email Confirmation - ProView Golf", body);

                return _dbo.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool RecoverPassword(string email, out string msg)
        {
            try
            {
                var user = _dbo.Users.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    user.Password = PasswordGenerator.GeneratePassword(true, true, true, true, 7);
                    _dbo.Users.Update(user);
                    var body = @"<b>Hi " + user.Profile.FirstName + ",</b><br>" +
                               "We have created your new password, <br> " +
                               "<b>" + user.Password + "<b><br>" +
                               "please use above password to login to your account, you can change this password after successful login";

                    EmailSender.SendEmail(user.Profile.FirstName, user.Email, "Password Recovery", body);
                    msg = "An email with new password has been sent to registered email address";
                    return _dbo.SaveChanges() > 0;
                }

                msg = "user account not found";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool ChangePassword(PasswordModel model, out string msg)
        {
            try
            {
                var user = _dbo.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.OldPassword);

                if (user != null)
                {
                    user.Password = model.NewPassword;
                    _dbo.Users.Update(user);
                    msg = "Password changed successfully";
                    return _dbo.SaveChanges() > 0;
                }

                msg = "User wasn't authenticated ";
                return false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        public bool UpdateProfile(ProfileModel profile, long userId, out string msg)
        {
            msg = null;
            var user = _dbo.Users.Find(userId);
            if (user == null) return false;

            user.Profile = profile;
            if (profile.ChangePassword)
            {
                if (string.IsNullOrEmpty(profile.NewPassword) || profile.OldPassword != user.Password)
                {
                    msg = "old password didn't match";
                    return false;
                }

                user.Password = profile.NewPassword;
            }

            msg = "profile successfully update";
            _dbo.Users.Update(user);
            return _dbo.SaveChanges() > 0;
        }

        public Profile Profile(long userId)
        {
            var user = _dbo.Users.Find(userId);
            return user?.Profile;
        }

        public bool Invitation(string code, int accept, out string msg)
        {
            msg = "Invalid Invitation";

            try
            {
                var invitation = _dbo.Invitations.FirstOrDefault(x => x.Code == code);
                if (invitation == null) return false;

                if (accept == 0)
                {
                    msg = "Invitation Rejected";
                    _dbo.Invitations.Remove(invitation);
                    return _dbo.SaveChanges() > 0;
                }

                var student = _dbo.Students.FirstOrDefault(x => x.Email == invitation.StudentEmail);
                var pro = _dbo.Pros.FirstOrDefault(x => x.Email == invitation.InstructorEmail);

                msg = "Invitation Accepted";
                if (student == null || pro == null) return true;

                student.ProRefId = pro.UserId;
                _dbo.Students.Update(student);
                _dbo.Invitations.Remove(invitation);
                return _dbo.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
    }
}