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

namespace WorkTime.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly WorkTimeContext db;

        public TasksController(WorkTimeContext context)
        {
            db = new WorkTimeContext();
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var workTimeContext = db.WorkTasks.Where(t => t.PerformerId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await workTimeContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || db.WorkTasks == null)
            {
                return NotFound();
            }

            var workTask = await db.WorkTasks
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
        public IActionResult Create()
        {
            ViewData["IssuerId"] = new SelectList(db.AspNetUsers, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(db.Projects, "Id", "Name");
            ViewData["TaskStatusId"] = new SelectList(db.WorkTaskStatuses, "Id", "Name");
            return View();
        }

        //Get: Tasks/CreateStatus
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateStatus()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskName,TaskText,PerformerId,CountOfHours,TaskStatusId,IssuerId")] WorkTask workTask)
        {
            workTask.Id = $"{Guid.NewGuid()}";
            workTask.PerformerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            db.Add(workTask);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //ViewData["IssuerId"] = new SelectList(db.AspNetUsers, "Id", "Id", workTask.IssuerId);
            //ViewData["PerformerId"] = new SelectList(db.AspNetUsers, "Id", "Id", workTask.PerformerId);
            //ViewData["ProjectId"] = new SelectList(db.Projects, "Id", "Id", workTask.ProjectId);
            //ViewData["TaskStatusId"] = new SelectList(db.WorkTaskStatuses, "Id", "Name", workTask.TaskStatusId);
            //return View(workTask);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || db.WorkTasks == null)
            {
                return NotFound();
            }

            var workTask = await db.WorkTasks.FindAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }
            ViewData["IssuerId"] = new SelectList(db.AspNetUsers, "Id", "Id", workTask.IssuerId);
            ViewData["PerformerId"] = new SelectList(db.AspNetUsers, "Id", "Id", workTask.PerformerId);
            ViewData["ProjectId"] = new SelectList(db.Projects, "Id", "Id", workTask.ProjectId);
            ViewData["TaskStatusId"] = new SelectList(db.WorkTaskStatuses, "Id", "Name", workTask.TaskStatusId);
            return View(workTask);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,TaskName,TaskText,ProjectId,PerformerId,CountOfHours,TaskStatusId,DateOfCompletion,InvoiceId,IssuerId")] WorkTask workTask)
        {
            if (id != workTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(workTask);
                    await db.SaveChangesAsync();
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
            ViewData["IssuerId"] = new SelectList(db.AspNetUsers, "Id", "Id", workTask.IssuerId);
            ViewData["PerformerId"] = new SelectList(db.AspNetUsers, "Id", "Id", workTask.PerformerId);
            ViewData["ProjectId"] = new SelectList(db.Projects, "Id", "Id", workTask.ProjectId);
            ViewData["TaskStatusId"] = new SelectList(db.WorkTaskStatuses, "Id", "Name", workTask.TaskStatusId);
            return View(workTask);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || db.WorkTasks == null)
            {
                return NotFound();
            }

            var workTask = await db.WorkTasks
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

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (db.WorkTasks == null)
            {
                return Problem("Entity set 'WorkTimeContext.WorkTasks'  is null.");
            }
            var workTask = await db.WorkTasks.FindAsync(id);
            if (workTask != null)
            {
                db.WorkTasks.Remove(workTask);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTaskExists(string id)
        {
          return db.WorkTasks.Any(e => e.Id == id);
        }
    }
}
