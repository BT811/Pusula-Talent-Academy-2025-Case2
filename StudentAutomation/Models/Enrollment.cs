namespace StudentAutomation.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }  

        public int CourseId { get; set; }
        public Course Course { get; set; }
        
        // Bir kaydın birden fazla notu olabilir (Vize, Final vb.)
        public ICollection<Grade> Grades { get; set; }
    }
}