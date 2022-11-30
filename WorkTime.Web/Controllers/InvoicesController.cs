using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WorkTime.Data;
using WorkTime.Models;

namespace WorkTime.Web.Controllers
{
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProjectId,PaymentStateId,Date,HoursWorked,HourlyWage,SumByHours,Bonus,Total,Issued,Remainder,Debt,RemWdebtAndBonus,SumWithTax")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentStateId"] = new SelectList(_context.PaymentStates, "Id", "Name", invoice.PaymentStateId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", invoice.ProjectId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", invoice.UserId);
            return View(invoice);
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
