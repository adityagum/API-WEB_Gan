using Web_API.Models;

namespace Web_API.ViewModels.Educations;

public class EducationVM
{
    public Guid Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float GPA { get; set; }
    public Guid UniversityGuid { get; set; }

    /*public static EducationVM ToVM(Education education)
    {
        return new EducationVM
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            GPA = education.Gpa,
            UniversityGuid = education.UniversityGuid
        };
    }*/
}
