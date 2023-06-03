using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WorkTime.Data;

namespace WorkTime.Web.Controllers
{
    [Authorize]
    public class GetValueController : Controller
    {
        private readonly WorkTimeContext _context;

        public GetValueController()
        {
            _context = new WorkTimeContext();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetProjects()
        {
            return JsonSerializer.Serialize(new { data = _context.Projects.Select(p => new { p.Id, p.Name })});
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
                json = JsonSerializer.Serialize(new { data = users.Select(u => new { u.UserId, u.Name, u.Surname }), 
                    bonus = _context.Projects.Find(projectId).Bonus });
            }
            else
            {
                var users = _context.AspNetUserInformations.AsEnumerable();
                json = JsonSerializer.Serialize(new { data = users.Select(u => new { u.UserId, u.Name, u.Surname }), bonus = 0 });
            }
            return json;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetUserAtributes(string userId, string projectId)
        {
            string json = "";
            if (!String.IsNullOrWhiteSpace(userId) && (projectId != "0" || !String.IsNullOrWhiteSpace(projectId)))
            {
                if (projectId == "0")
                {
                    json = JsonSerializer.Serialize(new { HourlyWage = _context.AspNetUserInformations.FirstOrDefault(a => a.UserId == userId).HourlyWage });
                }
                else
                    json = JsonSerializer.Serialize(_context.UserProjects.Where(u => u.UserId == userId 
                    && u.ProjectId == projectId).FirstOrDefault());
            }
            return json;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetUserTasks(string userId, string projectId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateFrom == null && dateTo == null)
            {
                return JsonSerializer.Serialize(new { data = 
                     _context.WorkTasks.Where(t => t.PerformerId == userId &&
            (projectId == "0" ? t.ProjectId == null : t.ProjectId == projectId) && t.InvoiceId == null)
                .Select(t => new { t.Id, t.CountOfHours, t.PerformerId, t.TaskName, t.TaskStatusId }).AsEnumerable()
            });
            }
            if (dateFrom != null && dateTo == null)
            {
                return JsonSerializer.Serialize(new
                {
                    data =
                     _context.WorkTasks.Where(t => 
                        t.PerformerId == userId &&
                        (projectId == "0" ? t.ProjectId == null : t.ProjectId == projectId) && 
                        t.InvoiceId == null &&
                        t.DateOfCompletion >= dateFrom)
                .Select(t => new { t.Id, t.CountOfHours, t.PerformerId, t.TaskName, t.TaskStatusId }).AsEnumerable()
                });
            }
            if (dateFrom != null && dateTo != null)
            {
                return JsonSerializer.Serialize(new
                {
                    data =
                     _context.WorkTasks.Where(t => 
                        t.PerformerId == userId &&
                        (projectId == "0" ? t.ProjectId == null : t.ProjectId == projectId) && 
                        t.InvoiceId == null &&
                        t.DateOfCompletion >= dateFrom && t.DateOfCompletion <= dateTo)
                .Select(t => new { t.Id, t.CountOfHours, t.PerformerId, t.TaskName, t.TaskStatusId }).AsEnumerable()
                });
            }

            return JsonSerializer.Serialize(":(");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetContractTemplate(string empTypeId)
        {
            string? template = _context.ContractTemplates.FirstOrDefault(ct => ct.Id == empTypeId)?.Template;
            return JsonSerializer.Serialize(new { data = template == null ? " " : template });
        }
    }
}
