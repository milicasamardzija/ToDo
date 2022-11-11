using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Dto
{
    public class AssignmentResponse
    {
        public int Number { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Reminder { get; set; }
        public bool IsExpired { get; set; } = false;
        public Attachment Attachment { get; set; }
    }
}
