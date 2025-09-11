namespace StudentAutomation.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        // Katılan öğrenci
        public int UserId { get; set; }
        public User User { get; set; }  // User rolü Student olmalı

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime Date { get; set; }
        public bool IsPresent { get; set; } // true = geldi, false = gelmedi
    }
}
