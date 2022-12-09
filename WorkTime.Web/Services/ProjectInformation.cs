using WorkTime.Data;

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
            return _context.Projects.FirstOrDefault(u => u.Id == id).Name;
        }
    }
}
