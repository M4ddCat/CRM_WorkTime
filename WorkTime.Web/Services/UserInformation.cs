using Newtonsoft.Json.Linq;
using System.Globalization;
using WorkTime.Data;
using WorkTime.Models;

namespace WorkTime.Web.Services
{
    public class UserInformation
    {
        private WorkTimeContext _context;
        private IHttpContextAccessor _httpContext;
        public UserInformation(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
            _context = new WorkTimeContext();
        }

        public string GetUserName()
        {
            AspNetUserInformation? userInfo = _context.AspNetUserInformations.FirstOrDefault(u => u.UserId == GetUId());
            if (userInfo == null) throw new ArgumentNullException();
            return $"{userInfo.Name} {userInfo.Surname}";
        }

        public bool CurUserIsSignedIn()
        {
            string? uName = _httpContext.HttpContext?.User.Identity?.Name;
            if (uName == null) return false;
            return true;
        }

        public string GetUserName(string id)
        {
            AspNetUserInformation? userInfo = _context.AspNetUserInformations.FirstOrDefault(u => u.UserId == id);
            if (userInfo == null) throw new ArgumentNullException();
            return $"{userInfo.Name} {userInfo.Surname}";
        }

        public string GetUserId()
        {
            return GetUId();
        }

        public int GetUserCountOfTasks()
        {
            return _context.WorkTasks.Where(t => (t.TaskStatusId == 1 || t.TaskStatusId == 2) && t.PerformerId == GetUId()).Count();
        }

        public string GetUserMounthlyMoney()
        {
            string uId = GetUId();
            DateTime now = DateTime.Now;
            var mas = _context.WorkTasks
                .Where(t => t.DateOfCompletion.Value.Month == now.Month
                && t.DateOfCompletion.Value.Year == now.Year
                && t.PerformerId == uId
                && (t.TaskStatusId == 3 || t.TaskStatusId == 4))
                .Select(t => t.CountOfHours).ToArray();
            if (mas.Length == 0)
            {
                return "0";
            }
            double countOfHours = 0;
            for (int i = 0; i != mas.Length; i++)
            {
                countOfHours += mas[i];
            }
            double? hourlyWage = _context.AspNetUserInformations.FirstOrDefault(u => u.UserId == uId)?.HourlyWage;
            if (hourlyWage == null)
                throw new ArgumentNullException();
            double money = (double)(countOfHours * hourlyWage);
            return money.ToString("C", CultureInfo.CreateSpecificCulture("ru-RU"));
        }

        public MonthTasks[] GetUserTasksByMonths()
        {
            string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            DateTime date = DateTime.Now;
            int nowMonth = date.Month - 1;
            int nowYear = date.Year;
            string uId = GetUId();
            MonthTasks[] monthTasks = new MonthTasks[6];
            for (int i = 0; i < 6; i++)
            {
                int count = _context.WorkTasks.Where(t => t.DateOfCompletion.Value.Year == nowYear
                && t.DateOfCompletion.Value.Month == nowMonth + 1 
                && (t.TaskStatusId == 3 || t.TaskStatusId == 4)
                && t.PerformerId == uId).Count();
                monthTasks[i] = new MonthTasks(months[nowMonth], count);

                if (nowMonth == 0)
                {
                    nowMonth = 12;
                    nowYear--;
                }
                nowMonth--;
            }
            return monthTasks;
        }

        public Invoice GetUserLastInvoce()
        {
            Invoice? uInv = _context.Invoices.Where(i => i.UserId == GetUId())
                .OrderByDescending(i => i.Date).FirstOrDefault();
            if (uInv == null)
            {
                uInv = new Invoice() { Bonus = 0, Date = null, Debt = 0, HourlyWage = 0, HoursWorked = 0, Id = "0"
                , Issued = 0, PaymentStateId = 0, ProjectId = null, SumByHours = 0, Remainder = 0, Total = 0, RemWdebtAndBonus =0
                , SumWithTax = 0, UserId = "0"};
            }
            uInv.SumWithTax = Math.Round(uInv.SumWithTax, 0);
            return uInv;
        }

        private string GetUId()
        {
            string? uName = _httpContext.HttpContext?.User.Identity?.Name;
            if (uName == null)
                throw new ArgumentNullException();
            string? uId = _context.AspNetUsers.FirstOrDefault(u => u.UserName == uName)?.Id;
            if (uId == null)
                throw new ArgumentNullException();
            return uId;
        }

        public class MonthTasks
        {
            public string month;
            public int tasks;
            public MonthTasks(string _month, int _tasks)
            {
                month = _month; tasks = _tasks;
            }
        }
    }
}
