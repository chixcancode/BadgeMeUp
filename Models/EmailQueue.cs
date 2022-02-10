#nullable disable

namespace BadgeMeUp.Models
{
    public class EmailQueue
    {
        public int Id { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Sent { get; set; } = false;
    }
}
