using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Json;
using WorkTime.Data;

namespace WorkTime.Web.Controllers
{
    public class GetValueController : Controller
    {
        private readonly WorkTimeContext _context;

        public GetValueController()
        {
            _context = new WorkTimeContext();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetUsersInProject(string projectId)
        {
            string json = "";
            if (projectId != "0")
            {
                var projectUsers = _context.UserProjects.Where(p => p.ProjectId == projectId)
                .Select(p => p.UserId).AsEnumerable();
                var users = _context.AspNetUserInformations
                    .Where(u => projectUsers.Contains(u.UserId)).AsEnumerable();
                json = JsonConvert.SerializeObject(new { data = users.Select(u => new { u.UserId, u.Name, u.Surname }) });
            }
            else
            {
                var users = _context.AspNetUserInformations.AsEnumerable();
                json = JsonConvert.SerializeObject(new { data = users.Select(u => new { u.UserId, u.Name, u.Surname }) });
            }
            return json;
        }
    }
}
