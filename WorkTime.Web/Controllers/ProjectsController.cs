using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;
using Microsoft.CodeAnalysis;

namespace WorkTime.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly WorkTimeContext _context;

        public ProjectsController(WorkTimeContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
              return View(await _context.Projects.ToListAsync());
        }

        //GET: AddUserInProject
        public async Task<IActionResult> AddUserInProject(string id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var projectUsers = _context.UserProjects.Where(p => p.ProjectId == id)
                .Select(p => p.UserId).AsEnumerable();
            if (projectUsers == null)
            {
                throw new ArgumentNullException(nameof(projectUsers));
            }
            var users = _context.AspNetUserInformations
                .Where(u => !projectUsers.Contains(u.UserId)).AsEnumerable();
            if (users == null)
            {
                return NotFound();
            }
            var typeEmp = _context.TypeOfEmployments.AsEnumerable();

            ViewBag.Project = await _context.Projects.FindAsync(id);
            ViewBag.Users = users;
            ViewBag.EmpType = typeEmp;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserInProject(UserProject userProject)
        {
            await _context.UserProjects.AddAsync(new UserProject()
            {
                UserId = userProject.UserId,
                ProjectId = userProject.ProjectId,
                HourlyWage = userProject.HourlyWage,
                EmpTypeId = userProject.EmpTypeId
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UsersInProject(string id)
        {
            ViewBag.Project = await _context.Projects.FindAsync(id);
            return View(await _context.UserProjects.Where(u => u.ProjectId == id).ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Bonus")] Models.Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Bonus")] Models.Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'WorkTimeContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(string id)
        {
          return _context.Projects.Any(e => e.Id == id);
        }
    }
}
