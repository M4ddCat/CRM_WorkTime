using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using WorkTime.Data;

namespace WorkTime.Web.Controllers
{
    [Authorize]
    public class TaskCommentariesController : Controller
    {
        private readonly WorkTimeContext _context;

        public TaskCommentariesController()
        {
            _context = new WorkTimeContext();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetCommentaries(string id)
        {
            var commentaries = _context.TaskCommentaries.Where(t => t.TaskId == id);
            var usersId = commentaries.Select(c => c.UserId);
            var users = _context.AspNetUserInformations.Where(t => usersId.Contains(t.UserId));
            return JsonSerializer.Serialize(new { data = _context.Projects.Select(p => new { p.Id, p.Name }) });
        }

    }
}
