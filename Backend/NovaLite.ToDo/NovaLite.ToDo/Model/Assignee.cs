namespace NovaLite.ToDo.Model
{
    public class Assignee
    {
        private List<Assignment> assignments = new List<Assignment>();
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public IReadOnlyCollection<Assignment> Assignments => assignments;
        public void AddAssignment(Assignment assignment)
        {
            assignment.Number = GenerateID();
            assignments.Add(assignment);
        }
        private int GenerateID()
        {
            return Assignments.Count > 0 ? Assignments.Max(assignment => assignment.Number) + 1 : 1;
        }
    }
}
