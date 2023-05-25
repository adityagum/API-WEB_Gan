using System;
using Web_API.Contexts;
using Web_API.Contracts;
using Web_API.Models;
using Web_API.ViewModels.Employees;

namespace Web_API.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
   /* private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;*/

    /*public EmployeeRepository(BookingManagementDbContext context, 
        IEducationRepository educationRepository,
        IUniversityRepository universityRepository ) : base(context) 
    {
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }*/

    public EmployeeRepository(BookingManagementDbContext context) : base(context)
    {
    }


    public IEnumerable<Employee> GetByEmail(string email)
    {
        return _context.Set<Employee>().Where(e => e.Email == email);
    }


    // Kel 1
    public IEnumerable<MasterEmployeeVM> GetAllMasterEmployee()
    {
        var employees = GetAll();
        var educations = _context.Educations.ToList();
        var universities = _context.Universities.ToList();

        var employeeEducations = new List<MasterEmployeeVM>();

        foreach (var employee in employees)
        {
            var education = educations.FirstOrDefault(e => e.Guid == employee?.Guid);
            var university = universities.FirstOrDefault(u => u.Guid == education?.UniversityGuid);

            if (education != null && university != null)
            {
                var employeeEducation = new MasterEmployeeVM
                {
                    Guid = employee.Guid,
                    NIK = employee.Nik,
                    FullName = employee.FirstName + " " + employee.LastName,
                    BirthDate = employee.BirthDate,
                    Gender = employee.Gender.ToString(),
                    HiringDate = employee.HiringDate,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Major = education.Major,
                    Degree = education.Degree,
                    GPA = education.Gpa,
                    UniversityName = university.Name
                };

                employeeEducations.Add(employeeEducation);
            }
        }

        return employeeEducations;
    }


    MasterEmployeeVM? IEmployeeRepository.GetMasterEmployeeByGuid(Guid guid)
    {
        var employee = GetByGuid(guid);
        var educations = _context.Educations.Find(guid);
        var universities = _context.Universities.Find(educations.UniversityGuid);

        var data = new MasterEmployeeVM
        {
            Guid = employee.Guid,
            NIK = employee.Nik,
            FullName = employee.FirstName + " " + employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender.ToString(),
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Major = educations.Major,
            Degree = educations.Degree,
            GPA = educations.Gpa,
            UniversityName = universities.Name
        };

        return data;
    }
    // End Kel 1


    // Kel 2
    public int CreateWithValidate(Employee employee)
    {
        try
        {
            bool ExistsByEmail = _context.Employees.Any(e => e.Email == employee.Email);
            if (ExistsByEmail)
            {
                return 1;
            }

            bool ExistsByPhoneNumber = _context.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber);
            if (ExistsByPhoneNumber)
            {
                return 2;
            }

            Create(employee);
            return 3;

        }
        catch
        {
            return 0;
        }
    }
    // End Kelompok 2

    // Kelompok 5 dan 6
    public Guid? FindGuidByEmail(string email)
    {
        try
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
            if (employee == null)
            {
                return null;
            }
            return employee.Guid;
        }
        catch
        {
            return null;
        }   

    }

}
