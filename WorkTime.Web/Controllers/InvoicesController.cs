using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;

namespace WorkTime.Web.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly WorkTimeContext _context;

        public InvoicesController(WorkTimeContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var workTimeContext = _context.Invoices.Include(i => i.PaymentState).Include(i => i.Project).Include(i => i.User);
            return View(await workTimeContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.PaymentState)
                .Include(i => i.Project)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["PaymentStateId"] = new SelectList(_context.PaymentStates, "Id", "Name");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Task<IActionResult>
        public string Create(string userId, string projectId, string[] tasksId, double bonus, double hourlyWage)
        {
            if(!_context.AspNetUsers.Any(u => u.Id == userId))
            {
                return "0";
            }
            double countHours = 0;
            foreach (string id in tasksId)
            {
                countHours += _context.WorkTasks.Find(id).CountOfHours;
            }
            double sumByHoursWithBonus = hourlyWage * countHours + bonus;
            int percents = 6;
            if (projectId != "0")
            {
                string up = _context.UserProjects.Where(u => u.UserId == userId && u.ProjectId == projectId).FirstOrDefault().EmpTypeId;
                percents = _context.TypeOfEmployments.Find(up).Tax;
            }
            Invoice invoice = new Invoice() {
                UserId = userId,
                ProjectId = projectId == "0" ? null : projectId,
                Bonus = bonus,
                HourlyWage = hourlyWage,
                HoursWorked = countHours,
                Date = DateTime.Now,
                PaymentStateId = 1,
                SumByHours = hourlyWage * countHours,
                RemWdebtAndBonus = sumByHoursWithBonus,
                Debt = 0,
                Issued = 0,
                Remainder = sumByHoursWithBonus,
                SumWithTax = Math.Round(sumByHoursWithBonus + (sumByHoursWithBonus * percents / (100 - percents)), 2)
            };
            _context.Add(invoice);

            foreach (string id in tasksId)
            {
                WorkTask task = _context.WorkTasks.Find(id);
                task.InvoiceId = invoice.Id;
                _context.Entry(task).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return "1";
        }

        public async Task<IActionResult> UploadFile(string id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            ViewBag.InvoiceId = _context.Invoices.FirstOrDefault(m => m.Id == id).Id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(string invoiceId, IFormFile file, string name)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
                _context.Add(new InvoiceFile() 
            { 
                InvoiceId = invoiceId,
                File = fileBytes,
                Name = name
            });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["PaymentStateId"] = new SelectList(_context.PaymentStates, "Id", "Name", invoice.PaymentStateId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", invoice.ProjectId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", invoice.UserId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,ProjectId,PaymentStateId,Date,HoursWorked,HourlyWage,SumByHours,Bonus,Total,Issued,Remainder,Debt,RemWdebtAndBonus,SumWithTax")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
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
            ViewData["PaymentStateId"] = new SelectList(_context.PaymentStates, "Id", "Name", invoice.PaymentStateId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", invoice.ProjectId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", invoice.UserId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.PaymentState)
                .Include(i => i.Project)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'WorkTimeContext.Invoices'  is null.");
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(string id)
        {
          return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
