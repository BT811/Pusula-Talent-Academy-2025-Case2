namespace StudentAutomation.Frontend.Models
{


    public class CourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string Status { get; set; } = "NotStarted";  // string yapıldı
    }

    public class CreateCourseDto
    {
        public string Name { get; set; }
        public int? TeacherId { get; set; }
    }

    public class CourseUpdateDto
    {
        public string? Name { get; set; }
        public int? TeacherId { get; set; }
        public int? Status { get; set; }
    }
    public enum CourseStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
}
