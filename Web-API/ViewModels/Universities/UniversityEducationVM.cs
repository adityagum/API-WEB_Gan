using Web_API.ViewModels.Educations;

public class UniversityEducationVM
{
    public Guid? Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public IEnumerable<EducationVM> Educations { get; set; }
}