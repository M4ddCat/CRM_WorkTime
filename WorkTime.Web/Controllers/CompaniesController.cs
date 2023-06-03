using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;

namespace WorkTime.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly WorkTimeContext _context;

        public CompaniesController(WorkTimeContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var workTimeContext = _context.Companies.Include(c => c.BankInfo);
            return View(await _context.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.BankInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            ViewData["BankInfoId"] = new SelectList(_context.BankInformation, "Id", "Id");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string companyPlace, string directorFullName, 
            string bankAccount, string bankName, string corInv, string bik, string bankLocation)
        {
            BankInformation bankinfo = new BankInformation()
            {
                BankAccount = bankAccount,
                BankLocation = bankLocation,
                Bik = bik,
                BankName = bankName,
                CorInv = corInv
            };
            Company comp = new Company()
            {
                Name = name,
                BankInfo = bankinfo,
                BankInfoId = bankinfo.Id,
                CompanyPlace = companyPlace,
                DirectorFullName = directorFullName
            };
            await _context.AddAsync(bankinfo);
            await _context.AddAsync(comp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            
            var bankInfo = await _context.BankInformation.FindAsync(company.BankInfoId);
            if (bankInfo == null)
            {
                return NotFound();
            }

            ViewData["BankAccount"] = bankInfo.BankAccount;
            ViewData["BankName"] = bankInfo.BankName;
            ViewData["CorInv"] = bankInfo.CorInv;
            ViewData["Bik"] = bankInfo.Bik;
            ViewData["BankLocation"] = bankInfo.BankLocation;

            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Company company, 
            BankInformation bankInfo)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(company);
                    BankInformation bankInformation = await _context.BankInformation.FindAsync(bankInfo.Id);
                    bankInformation.CorInv = bankInfo.CorInv;
                    bankInformation.BankName = bankInfo.BankName;
                bankInformation.BankLocation = bankInfo.BankLocation;
                bankInformation.BankAccount = bankInfo.BankAccount;
                bankInformation.Bik = bankInfo.Bik;

                    _context.Update(bankInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["BankInfoId"] = new SelectList(_context.BankInformation, "Id", "Id", company.BankInfoId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.BankInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'WorkTimeContext.Company'  is null.");
            }
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(string id)
        {
          return _context.Companies.Any(e => e.Id == id);
        }
    }
}
