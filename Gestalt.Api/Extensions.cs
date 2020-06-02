using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Gestalt.Api
{
    public static class Extensions
    {
        public static int GetCurrentUserId(this ControllerBase controller)
        {
            if (controller.User.Identity is ClaimsIdentity claimsIdentity)
            {
                var identifierClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                return int.Parse(identifierClaim.Value);
            }
            throw new System.Exception(Constants.NotAuthorizedExceptionMessage);
        }
    }
}
