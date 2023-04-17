using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;
using System.IO.Compression;

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
        [Authorize(Roles = "Administrator,Manager,Bookkeeper")]
        public async Task<IActionResult> Index()
        {
            var workTimeContext = _context.Invoices.Include(i => i.PaymentState)
                .Include(i => i.Project)
                .Include(i => i.User.AspNetUserInformations);
            return View(await workTimeContext.ToListAsync());
        }

        public async Task<IActionResult> MyInvoices(string id)
        {
            var workTimeContext = _context.Invoices.Where(i => i.UserId == id)
                .Include(i => i.PaymentState)
                .Include(i => i.Project)
                .Include(i => i.User);
            return View(await workTimeContext.ToListAsync());
        }

        [Authorize(Roles = "Administrator,Manager,Bookkeeper")]
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
        [Authorize(Roles = "Administrator,Manager,Bookkeeper")]
        public IActionResult Create()
        {
            ViewData["PaymentStateId"] = new SelectList(_context.PaymentStates, "Id", "Name");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", "Без проекта");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Task<IActionResult>
        [Authorize(Roles = "Administrator,Manager,Bookkeeper")]
        public string Create(string userId, string projectId, string[] tasksId, double bonus, double hourlyWage)
        {
            if (!_context.AspNetUsers.Any(u => u.Id == userId))
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
            Invoice invoice = new Invoice()
            {
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

        public async Task<IActionResult> InvoicePay(string id)
        {
            if (id == null || _context.Invoices == null || !(await _context.Invoices.AnyAsync(i => i.Id == id)))
            {
                return NotFound();
            }
            var inv = await _context.Invoices.FindAsync(id);
            var user = await _context.AspNetUserInformations.Where(u => u.UserId == inv.UserId).FirstOrDefaultAsync();
            ViewBag.UserName = $"{user.Name} {user.Surname} {user.Patronymic}";
            ViewBag.InvoiceId = id;
            ViewBag.ToPay = _context.Invoices.FindAsync(id).Result.SumWithTax;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InvoicePay(string invoiceId, double issued, IFormFile uploadedFile)
        {
            if (invoiceId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                uploadedFile.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            _context.Add(new InvoiceFile()
            {
                InvoiceId = invoiceId,
                File = fileBytes,
                Name = uploadedFile.FileName
            });
            var inv = await _context.Invoices.FindAsync(invoiceId);
            inv.Issued = issued;
            inv.Remainder = inv.SumWithTax - issued;
            inv.PaymentStateId = 2;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> InvoiceConfirm(string id)
        {
            if (id == null || _context.Invoices == null || !(await _context.Invoices.AnyAsync(i => i.Id == id)))
            {
                return NotFound();
            }

            ViewBag.InvoiceId = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InvoiceConfirm(string invoiceId, IFormFile uploadedFile)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                uploadedFile.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            _context.Add(new InvoiceFile()
            {
                InvoiceId = invoiceId,
                File = fileBytes,
                Name = uploadedFile.FileName
            });
            var inv = await _context.Invoices.FindAsync(invoiceId);
            inv.PaymentStateId = 3;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UploadFile(string id)
        {
            if (id == null || _context.Invoices == null || !(await  _context.Invoices.AnyAsync(i => i.Id == id)))
            {
                return NotFound();
            }

            ViewBag.InvoiceId = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(string invoiceId, IFormFile uploadedFile)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                uploadedFile.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            _context.Add(new InvoiceFile()
            {
                InvoiceId = invoiceId,
                File = fileBytes,
                Name = uploadedFile.FileName
            });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator,Manager,Bookkeeper")]
        public async Task<FileResult> GetInvoiceFiles(string id)
        {
            List<InvoiceFile> files = _context.InvoiceFiles.Where(i => i.InvoiceId == id).ToList();

            if (!Directory.Exists($"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}/Temp/"))
            {
                Directory.CreateDirectory($"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}/Temp/");
            }

            string path = $"{Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory)}/Temp/{id}";

            Directory.CreateDirectory(path);

            foreach (InvoiceFile file in files)
            {
                System.IO.File.WriteAllBytes($"{path}/{file.Name}", file.File);
            }

            ZipFile.CreateFromDirectory(path, $"{path}.zip");

            byte[] archive = System.IO.File.ReadAllBytes($"{path}.zip");

            Directory.Delete(path, true);
            System.IO.File.Delete($"{path}.zip");

            return File(archive, "application/zip");
        }

        // GET: Invoices/Edit/5
        [Authorize(Roles = "Administrator,Manager,Bookkeeper")]
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
            var users = _context.AspNetUserInformations.Select(u => new
            {
                Id = u.UserId,
                Name = $"{u.Name} {u.Surname}"
            });
            ViewData["PaymentStateId"] = new SelectList(_context.PaymentStates, "Id", "Name", invoice.PaymentStateId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", invoice.ProjectId);
            ViewData["UserId"] = new SelectList(users, "Id", "Name", invoice.UserId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager,Bookkeeper")]
        public async Task<IActionResult> Edit(string id, Invoice invoice)
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
            var users = _context.AspNetUserInformations.Select(u => new
            {
                Id = u.UserId,
                Name = $"{u.Name} {u.Surname}"
            });
            ViewData["PaymentStateId"] = new SelectList(_context.PaymentStates, "Id", "Name", invoice.PaymentStateId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", invoice.ProjectId);
            ViewData["UserId"] = new SelectList(users, "Id", "Name", invoice.UserId);
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager,Bookkeeper")]
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
