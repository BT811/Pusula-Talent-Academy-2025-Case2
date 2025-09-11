namespace StudentAutomation.Models
{
    public class Grade
    {
        public int Id { get; set; }

        // Notu alan öğrenci (User üzerinden)
        public int UserId { get; set; }
        public User User { get; set; }

        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; }

        public double Score { get; set; }
        public string Description { get; set; }  // "Midterm", "Final"
        public DateTime Date { get; set; } = DateTime.UtcNow; // Notun eklenme tarihi
    }
}
