namespace NovaLite.ToDo.Model
{
    public class Assignment 
    {
        public int Number { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Step> Steps { get; set; } = new List<Step>();
        public DateTime Reminder { get; set; }
        public bool IsExpired { get; set; } = false;
        public Attachment? Attachment { get; set; }

    }
}
