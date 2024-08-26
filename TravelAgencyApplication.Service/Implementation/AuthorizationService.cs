using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Service.Interface;
namespace TravelAgencyApplication.Service.Implementation
{
    public class AuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public AuthorizationService(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public bool IsUserAuthorized(out TAUser currentUser)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            currentUser = null;

            if (userId == null)
            {
                return false;
            }

            currentUser = _userService.GetDetailsForTAUser(userId);

            if (currentUser == null || currentUser.UserRole != UserRole.ADMIN)
            {
                return false;
            }

            return true;
        }
    }
}
