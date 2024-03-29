﻿using System.Security.Claims;

namespace Runner.Helpers
{
    public static class ClaimsPrincipleExtensions
    {

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
