using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using WorkTime.Data;
using WorkTime.Models;

using Microsoft.AspNetCore.WebUtilities;
using System.Xml.Linq;
using System.Security.Claims;

namespace WorkTime.Web.Areas.Identity.Pages.Account
{
    public class AddUserDataModel : PageModel
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly WorkTimeContext db;

        public AddUserDataModel(WorkTimeContext context)
        {
            db = new WorkTimeContext();
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var userInfo = new AspNetUserInformation();
                userInfo.Name = Request.Form["Name"];
                userInfo.Surname = Request.Form["Surname"];
                userInfo.Patronymic = Request.Form["Patronymic"];
                userInfo.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                db.AspNetUserInformations.Add(userInfo);

                var userId = userInfo.UserId;
                var user = User;
                UserManager<IdentityUser> userManager =;
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }
            return Page();
        }
    }
}
