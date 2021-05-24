using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ProViewGolf.Platform
{
    public static class ControllerExtenstion
    {
        public static long UserId(this ControllerBase controllerBase)
        {
            try
            {
                return long.Parse(controllerBase.User.Claims.First(i => i.Type == "UserId").Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
