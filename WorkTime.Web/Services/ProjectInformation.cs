using WorkTime.Data;
using WorkTime.Models;

namespace WorkTime.Web.Services
{
    public class ProjectInformation
    {
        private WorkTimeContext _context;
        public ProjectInformation(WorkTimeContext context)
        {
            _context = context;
        }
        public string GetName(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return "";
            }
            return _context.Projects.FirstOrDefault(u => u.Id == id).Name;
        }

        public string GetContract(string userProjectId)
        {
            if (String.IsNullOrWhiteSpace(userProjectId))
            {
                throw (new InvalidOperationException());
            }
            string? contractId = _context.Contracts.Where(c => c.UserProjectId == userProjectId).FirstOrDefault()?.Id;
            return contractId == null ? "Без договора" : contractId;
        }

        public string GetEmployeeName(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                throw new InvalidOperationException();
            }
            string? name = _context.TypeOfEmployments.Find(id)?.Name;
            return name == null ? "Имя не найдено" : name;
        }
    }
}
