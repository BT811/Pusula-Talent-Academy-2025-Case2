namespace StudentAutomation.Models
{
    public class Message
    {
        public int Id { get; set; }

        // Gönderen (Teacher veya Student)
        public int SenderId { get; set; }
        public User Sender { get; set; }

        // Alıcı (Teacher veya Student)
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        // Opsiyonel: Mesajın hangi kursa ait olduğu
        public int? CourseId { get; set; }
        public Course Course { get; set; }

        public string Content { get; set; }      // Mesaj içeriği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
