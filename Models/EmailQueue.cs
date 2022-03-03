#nullable disable

namespace BadgeMeUp.Models;

public class EmailQueue
{
    public string Body { get; set; }

    public int Id { get; set; }

    public bool Sent { get; set; } = false;

    public string Subject { get; set; }

    public string ToEmail { get; set; }
}