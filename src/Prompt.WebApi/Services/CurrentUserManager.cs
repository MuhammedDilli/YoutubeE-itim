using Prompt.Persistence.Services;

namespace Prompt.WebApi.Services
{
    public class CurrentUserManager : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? UserId => 123456;

        public long? GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;

            return userId is null ? null : long.Parse(userId);
        }
    }
}
