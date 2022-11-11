namespace NovaLite.ToDo.Model
{
    public class Step
    {
        public int Number { get; set; }
        public string Subject { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
}
