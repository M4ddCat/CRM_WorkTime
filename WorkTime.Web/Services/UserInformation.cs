using WorkTime.Data;

namespace WorkTime.Web.Services
{
    public class UserInformation
    {
        private WorkTimeContext _context;
        private IHttpContextAccessor _httpContext;
        public UserInformation(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
            _context = new WorkTimeContext();
        }

        public string GetName()
        {
            string userName = _httpContext.HttpContext?.User.Identity.Name;
            string userId = _context.AspNetUsers.FirstOrDefault(u => u.UserName == userName)?.Id;
            var userInfo = _context.AspNetUserInformations.FirstOrDefault(u => u.UserId == userId);
            return $"{userInfo.Name} {userInfo.Surname}";
        }

        public string GetName(string id)
        {
            var userInfo = _context.AspNetUserInformations.FirstOrDefault(u => u.UserId == id);
            return $"{userInfo.Name} {userInfo.Surname}";
        }

        public string GetMyId()
        {
            string userName = _httpContext.HttpContext?.User.Identity.Name;
            return _context.AspNetUsers.FirstOrDefault(u => u.UserName == userName)?.Id;
        }
    }
}
