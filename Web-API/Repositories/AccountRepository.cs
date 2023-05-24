using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Accounts;
using Web_API.ViewModels.Login;

namespace Web_API.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    private readonly IEmployeeRepository _employeeRepository;
    public AccountRepository(BookingManagementDbContext context, IEmployeeRepository employeeRepository) : base(context)
    {
        _employeeRepository = employeeRepository;
    }

    public AccountEmpVM Login(LoginVM loginVM)
    {
        var account = GetAll();
        var employee = _employeeRepository.GetAll();
        var query = from emp in employee
                    join acc in account
                    on emp.Guid equals acc.Guid
                    where emp.Email == loginVM.Email
                    select new AccountEmpVM
                    {
                        Email = emp.Email,
                        Password = acc.Password

                    };
        return query.FirstOrDefault();
    }
}
