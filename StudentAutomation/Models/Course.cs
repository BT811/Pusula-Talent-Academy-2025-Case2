namespace StudentAutomation.Models
{
    public enum CourseStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

     public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TeacherId { get; set; }
        public User? Teacher { get; set; }
        public CourseStatus Status { get; set; } = CourseStatus.NotStarted;

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
