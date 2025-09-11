using StudentAutomation.Models;

namespace StudentAutomation.Dtos
{

    public class CreateCourseDto
    {
        public string Name { get; set; }
        public int? TeacherId { get; set; }
        public CourseStatus Status { get; set; } = CourseStatus.NotStarted;
    }

    // Ders listelerinde gösterilecek temel bilgileri içerir.
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; } // Öğretmen ID yerine Adı Soyadı
        public string Status { get; set; }
    }

    // Bir dersin detaylarını gösterir (öğrenciler vb.).
    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public string Status { get; set; }
        public List<string> EnrolledStudents { get; set; }
    }
    public class CourseUpdateDto
    {
        public string? Name { get; set; }
        public int? TeacherId { get; set; }
        public CourseStatus? Status { get; set; }
    }

    public class EnrollmentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<GradeDto> Grades { get; set; }
    }

    public class GradeDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}