using Client.Models;
using Client.Repositories.Interface;

namespace Client.Repositories.Data
{
    public class EducationRepository : GeneralRepository<Education, Guid>, IEducationRepository
    {
        public EducationRepository(string request = "Education/") : base(request)
        {
        }
    }
}
