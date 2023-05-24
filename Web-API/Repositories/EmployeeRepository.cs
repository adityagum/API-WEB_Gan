using System;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;

namespace Web_API.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }

        public IEnumerable<Employee> GetByEmail(string email)
        {
            return _context.Set<Employee>().Where(e => e.Email == email);
        }

        /*IEnumerable<Employee> IEmployeeRepository.GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Where(e => e.Guid == guid);
        }*/

        /* public IEnumerable<Employee> GetByEmail(string email)
         {
             return _context.Set<Employee>().Where(u => u.Email == email);
         }

         public IEnumerable<Employee> GetByGuidAcc(Guid accId)
         {
             return _context.Set<Employee>().Where(u => u.Guid == accId);
         }*/

        /*Employee IEmployeeRepository.GetByEmail(string email)
        {
            throw new NotImplementedException();
        }*/
    }
}
