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
            var users = _context.AspNetUserInformations.Where(t => usersId.Contains(t.UserId))
                .Select(u => new { UserId = u.UserId, Name = $"{u.Name} {u.Surname}" });
            return JsonSerializer.Serialize(new { data = commentaries.Select(c => new { users.FirstOrDefault(u => u.UserId == c.UserId).Name, c.Text }) });
        }

    }
}
