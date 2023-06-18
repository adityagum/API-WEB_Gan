namespace Client.Models
{
    public class Education
    {
        public Guid guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public Guid UniversityGuid { get; set; }
    }
}
