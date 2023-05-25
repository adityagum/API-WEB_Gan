using Web_API.Models;

namespace Web_API.Contracts;

public interface IUniversityRepository : IGenericRepository<University>
{
    // JIka menambahkan method dengan konsep Generics
    IEnumerable<University> GetByName(string name);

    // Kelompok 2
    University CreateWithValidate(University university);
}
