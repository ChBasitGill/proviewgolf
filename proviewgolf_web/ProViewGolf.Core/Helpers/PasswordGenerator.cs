using System;

namespace ProViewGolf.Core.Helpers
{
    public class PasswordGenerator
    {
        private const string LowerCase = "abcdefghijklmnopqursuvwxyz";
        private const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numbers = "123456789";
        private const string Specials = @"!@£$%^&*()#€";


        public static string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial,
            int passwordSize)
        {
            var password = new char[passwordSize];
            var charSet = "";
            var random = new Random();
            int counter;
            if (useLowercase) charSet += LowerCase;

            if (useUppercase) charSet += UpperCase;

            if (useNumbers) charSet += Numbers;

            if (useSpecial) charSet += Specials;

            for (counter = 0; counter < passwordSize; counter++)
                password[counter] = charSet[random.Next(charSet.Length - 1)];

            return string.Join(null, password);
        }
    }
}