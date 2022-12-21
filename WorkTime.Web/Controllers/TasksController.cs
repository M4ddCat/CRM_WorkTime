using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;
using System.Text.Json;

namespace WorkTime.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly WorkTimeContext _context;

        public TasksController(WorkTimeContext context)
        {
            _context = new WorkTimeContext();
        }

        // GET: Tasks
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Index()
        {
            var workTimeContext = _context.WorkTasks.Include(w => w.TaskStatus).Include(w => w.Project);
            //Where(t => t.PerformerId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            return View(await workTimeContext.ToListAsync());
        }

        public async Task<IActionResult> MyTasks(string id)
        {
            var workTimeContext = _context.WorkTasks.Where(t => t.PerformerId == id).Include(w => w.TaskStatus).Include(w => w.Project);
            //Where(t => t.PerformerId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            return View(await workTimeContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.WorkTasks == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTasks
                .Include(w => w.Issuer)
                .Include(w => w.Performer)
                .Include(w => w.Project)
                .Include(w => w.TaskStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }


        // GET: Tasks/Create
        [Authorize(Roles = "Administrator,Manager")]
        public IActionResult Create()
        {
            ViewData["PerformerId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["TaskStatusId"] = new SelectList(_context.WorkTaskStatuses, "Id", "Name");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Create(WorkTask workTask)
        {
            workTask.IssuerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            workTask.TaskStatusId = 1;
            workTask.ProjectId = workTask.ProjectId == "0" ? null : workTask.ProjectId;
            _context.Add(workTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Tasks/Edit/5
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.WorkTasks == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }
            var users = _context.AspNetUserInformations.Select(u => new
            {
                Id = u.UserId,
                Name = $"{u.Name} {u.Surname}"
            });
            ViewData["IssuerId"] = new SelectList(users, "Id", "Name", workTask.IssuerId);
            ViewData["PerformerId"] = new SelectList(users, "Id", "Name", workTask.PerformerId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", workTask.ProjectId);
            ViewData["TaskStatusId"] = new SelectList(_context.WorkTaskStatuses, "Id", "Name", workTask.TaskStatusId);
            return View(workTask);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, WorkTask workTask)
        {
            if (id != workTask.Id)
            {
                return NotFound();
            }

            try
            {
                workTask.ProjectId = workTask.ProjectId == "null" ? null : workTask.ProjectId;
                _context.Update(workTask);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTaskExists(workTask.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // POST: Tasks/Delete/5
        [Authorize(Roles = "Administrator,Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.WorkTasks == null)
            {
                return Problem("Entity set 'WorkTimeContext.WorkTasks'  is null.");
            }
            var workTask = await _context.WorkTasks.FindAsync(id);
            if (workTask != null)
            {
                _context.WorkTasks.Remove(workTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GetCommentaries(string id)
        {
            if (!_context.WorkTasks.Any(a => a.Id == id))
            {
                return "";
            }
            var commentaries = _context.TaskCommentaries.Where(t => t.TaskId == id);
            var usersId = commentaries.Select(c => c.UserId);
            var users = _context.AspNetUserInformations.Where(t => usersId.Contains(t.UserId))
                .Select(u => new { UserId = u.UserId, Name = $"{u.Name} {u.Surname}" });
            return JsonSerializer.Serialize(new { data = commentaries.Select(c => new 
                { users.FirstOrDefault(u => u.UserId == c.UserId).Name, c.Text }
            ) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCommentary(string taskId, string text)
        {
            if (!_context.WorkTasks.Any(a => a.Id == taskId) || String.IsNullOrWhiteSpace(text))
            {
                return BadRequest();
            }
            _context.TaskCommentaries.Add(
                new TaskCommentary()
                {
                    TaskId = taskId,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Text = text
                });
            _context.SaveChanges();
            return Ok();
        }

        private bool WorkTaskExists(string id)
        {
            return _context.WorkTasks.Any(e => e.Id == id);
        }
    }
}
