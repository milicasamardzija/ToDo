namespace NovaLite.ToDo.Dto
{
    public class PagedResponse<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int Total { get; set; }
    }
}
