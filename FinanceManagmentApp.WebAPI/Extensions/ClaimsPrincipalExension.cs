﻿using System.Security.Claims;

namespace FinanceManagmentApp.WebAPI.Extensions
{
    internal static class ClaimsPrincipalExension
    {
        public static Guid GetUserIdFromJwt(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (claim is null)
            {
                throw new InvalidDataException("User id was not found in data from JWT token.");
            }

            return Guid.Parse(claim.Value);
        }
    }
}
