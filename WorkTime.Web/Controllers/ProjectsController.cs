using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;
using Microsoft.CodeAnalysis;
using SelectPdf;
using System.Drawing.Printing;
using System.Runtime.Intrinsics.Arm;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Build.Evaluation;

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
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Projects.ToListAsync());
        }

        //GET: AddUserInProject
        [Authorize(Roles = "Administrator,Manager")]
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
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> RemoveUserFromProject(string id)
        {
            _context.UserProjects.Remove(_context.UserProjects.Find(id));
            await _context.SaveChangesAsync();
            return RedirectToAction($"Index", "Projects");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> AddUserInProject(string userId, 
            string projectId, double hourlyWage, string empTypeId, DateTime? contractDate,
            string contractNumber)
        {
            UserProject userProj = new UserProject()
            {
                UserId = userId,
                ProjectId = projectId,
                HourlyWage = hourlyWage,
                EmpTypeId = empTypeId,
            };
            await _context.UserProjects.AddAsync(userProj);

            DateTime? contrDate = contractDate == null ? DateTime.Now : contractDate;

            Contract contract = new Contract()
            {
                ContractDate = contrDate,
                PerformerPersonId = userId,
                UserProjectId = userProj.Id,
                UserProject = userProj,
                ContractNumber = contractNumber
            };

            Models.Project proj = _context.Projects.Find(projectId);

            if (proj.CustomerCompanyId != null)
            {
                contract.CustomerCompanyId = proj.CustomerCompanyId;
            } 
            else if (proj.CustomerPersonId != null)
            {
                contract.CustomerPersonId = proj.CustomerPersonId;
            }
            await _context.AddAsync(contract);

            await _context.SaveChangesAsync();
            return RedirectToRoute($"CreateUserContract/{contract.Id}");
        }

        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> ProjectDetails(string id)
        {
            ViewBag.Project = await _context.Projects.FindAsync(id);
            ViewBag.Contract = await _context.Contracts.Where(c => c.UserProject.ProjectId == id).ToListAsync();
            return View(await _context.UserProjects.Where(u => u.ProjectId == id).ToListAsync());
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "Administrator,Manager")]
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
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Create()
        {
            ViewBag.CustomerCompany = await _context.Companies.Select(c => new { c.Id, c.Name }).ToListAsync();
            ViewBag.CustomerPerson = await _context.AspNetUserInformations.Select(c => new { c.Id, c.Name }).ToListAsync();
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Create(string name, string customerType, 
            string? companySelect, string? personSelect, double bonus, 
            DateTime startDate, DateTime? endDate)
        {
            Models.Project project = new Models.Project() { Name = name, Bonus = bonus, StartDate = startDate, EndDate = endDate };

            if (customerType == "company")
                project.CustomerCompanyId = companySelect;
            else if (customerType == "person")
                project.CustomerPersonId = personSelect;
            _context.Add(project);

            await _context.SaveChangesAsync();
            return RedirectToAction($"Details/{project.Id}", "Projects");
        }

        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> CreateUserContract(string id)
        {
            Contract? contract = await _context.Contracts.FindAsync(id);
            if (contract == null) throw new ArgumentNullException();

            string? projId = _context.UserProjects.FindAsync(contract.UserProjectId).Result?.ProjectId;
            if (projId == null) throw new ArgumentNullException();

            byte[]? ct = _context.ContractTemplates.FirstOrDefaultAsync(c => c.ProjectId == projId).Result?.TemplateFile;
            if (ct == null) throw new ArgumentNullException();

            string template = Encoding.UTF8.GetString(ct);

            ViewBag.Template = template;

            AspNetUserInformation? userInfo = _context.AspNetUserInformations.Find(contract.PerformerPersonId);
            if (userInfo == null) throw new ArgumentNullException();

            ViewBag.UserInfo = userInfo;

            ViewBag.BankInfo = _context.BankInformation.Find(userInfo.BankInfoId);

            ViewBag.Contract = contract;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> CreateUserContract(string htmlCode, string contractId, 
            string contractNumber)
        {
            Contract contract = _context.Contracts.Find(contractId);

            contract.ContractNumber = contractNumber;
            //contract.ContractDate = contractDate;

            HtmlToPdf converter = new HtmlToPdf();

            PdfDocument doc = converter.ConvertHtmlString($"<div style='padding:3rem!important'>{htmlCode}</div>");
            
            doc.Save("Name.pdf");
            doc.Close();

            return RedirectToAction($"Index", "Projects");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateContractTemplateInProject(string id)
        {
            ViewBag.Project = _context.Projects.Find(id);
            if (ViewBag.Project.CustomerCompanyId == null)
            {
                string uId = ViewBag.Project.CustomerPersonId;
                if (uId == null)
                {
                    throw new ArgumentNullException();
                }
                AspNetUserInformation? userInfo = _context.AspNetUserInformations.FirstOrDefault(u => u.UserId == uId);
                BankInformation? bankInfo = _context.BankInformation.FirstOrDefault(b => b.Id == userInfo.BankInfoId);
                ViewBag.BankInfo = bankInfo;
                ViewBag.UserInfo = userInfo;

            }
            else
            {
                Company? company = _context.Companies.Find(ViewBag.Project.CustomerCompanyId);
                BankInformation? bankInfo = _context.BankInformation.FirstOrDefault(b => b.Id == company.BankInfoId);
                ViewBag.BankInfo = bankInfo;
                ViewBag.Company = company;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateContractTemplateInProject(string projectId, string htmlCode)
        {
            byte[] template = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(htmlCode));
            ContractTemplate contractTemplate = new ContractTemplate() 
            { 
                ProjectId =  projectId, 
                TemplateFile = template
            };
            _context.AddAsync(contractTemplate);
            return RedirectToAction($"Details/{projectId}", "Projects");
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Administrator,Manager")]
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
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> Edit(string id, Models.Project project)
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

        // POST: Projects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Manager")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'WorkTimeContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.UserProjects.RemoveRange(_context.UserProjects.Where(p => p.ProjectId == id));
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
