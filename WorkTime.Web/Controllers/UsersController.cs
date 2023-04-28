using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTime.Data;
using WorkTime.Models;
using WorkTime.Web.Views.Users;

namespace WorkTime.Web.Controllers
{
    [Authorize(Roles = "Administrator,Manager")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WorkTimeContext _context;

        public UsersController(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            RoleManager<IdentityRole> roleManager,
            WorkTimeContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return View(await _context.AspNetUsers.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string email, string name, 
            string surname, string? patronymic, 
            double hourlyWage, string contactPhone, string contactEmail, string socialNetworkContact,
            string passportNum, string password, 
            string? bankAccount, string? personalAddress, string? bankName, 
            string? corInv, string? bik, string? bankLocation, string userType)
        {
            var user = CreateUser();
            await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                string userId = await _userManager.GetUserIdAsync(user);
                var _user = _context.AspNetUsers.Find(userId);

                _user.EmailConfirmed = true;

                BankInformation bankinfo = new BankInformation() {
                    BankAccount = bankAccount,
                    BankLocation = bankLocation,
                    Bik = bik,
                    BankName = bankName,
                    CorInv = corInv
                };

                int typeId = _context.UserTypes.Where(u => u.Type == userType).FirstOrDefault().Id;

                await _context.AddAsync(bankinfo);

                await _context.AspNetUserInformations.AddAsync(new AspNetUserInformation()
                {
                    Name = name,
                    Surname = surname,
                    Patronymic = patronymic,
                    HourlyWage = hourlyWage,
                    UserId = userId,
                    PassportNum = passportNum,
                    ContactEmail = contactEmail,
                    ContactPhone = contactPhone,
                    PersonalAddress = personalAddress,
                    SocialNetworkContact = socialNetworkContact,
                    BankInfo = bankinfo,
                    BankInfoId = bankinfo.Id,
                    UserTypeId = typeId
                });

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUsers.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            AspNetUserInformation? userInfo = await _context.AspNetUserInformations.FirstOrDefaultAsync(x => x.UserId == id);

            if (userInfo == null)
            {
                return NotFound();
            }
            BankInformation? bankInfo = await _context.BankInformation.FirstOrDefaultAsync(x => x.Id == userInfo.BankInfoId);
            if (bankInfo == null)
            {
                bankInfo = new BankInformation() { BankAccount = "", BankLocation = "", BankName = "", Bik = "", CorInv = "" };
                userInfo.BankInfoId = bankInfo.Id;
                _context.Add(bankInfo);
                _context.Update(userInfo);
                _context.SaveChanges();
            }
            ViewBag.UserInfo = userInfo;
            ViewBag.BankInfo = bankInfo;

            return View(aspNetUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string email, string name,
            string surname, string? patronymic,
            double hourlyWage, string contactPhone, string contactEmail, string socialNetworkContact,
            string passportNum, string password,
            string? bankAccount, string? personalAddress, string? bankName,
            string? corInv, string? bik, string? bankLocation, string userType)
        {
            AspNetUser? aspNetUser = _context.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            try
            {
                aspNetUser.Email = email;
                AspNetUserInformation? userInfo = _context.AspNetUserInformations.FirstOrDefault(u => u.UserId == id);
                if(userInfo == null) { return NotFound(); }
                userInfo.Name = name;
                userInfo.Surname = surname;
                userInfo.Patronymic = patronymic;
                userInfo.HourlyWage = hourlyWage;
                userInfo.ContactPhone = contactPhone;
                userInfo.ContactEmail = contactEmail;
                userInfo.SocialNetworkContact = socialNetworkContact;
                userInfo.PassportNum = passportNum;

                BankInformation? bankInformation = _context.BankInformation.FirstOrDefault(b => b.Id == userInfo.BankInfoId);
                if (bankInformation != null)
                {
                    bankInformation.BankAccount = bankAccount;
                    bankInformation.BankName = bankName;
                    bankInformation.CorInv = corInv;
                    bankInformation.Bik = bik;
                    bankInformation.BankLocation = bankLocation;
                }

                _context.Update(aspNetUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(aspNetUser.Id))
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

        // POST: Users/Delete/5
        /*
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AspNetUsers == null)
            {
                return Problem("Entity set 'WorkTimeContext.AspNetUsers'  is null.");
            }
            var aspNetUser = await _context.AspNetUsers.FindAsync(id);
            if (aspNetUser != null)
            {
                _context.AspNetUsers.Remove(aspNetUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        public async Task<IActionResult> UserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UserRole(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        private bool AspNetUserExists(string id)
        {
          return _context.AspNetUsers.Any(e => e.Id == id);
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
