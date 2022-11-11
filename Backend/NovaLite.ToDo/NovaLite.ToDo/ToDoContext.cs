using Microsoft.EntityFrameworkCore;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo
{
    public class ToDoContext : DbContext
    {
        public DbSet<Assignee> Assignee { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<Step> Step { get; set; }
        public DbSet<Attachment> Attachment { get; set; }

        private readonly string _currentUserEmail = null;

        public ToDoContext(DbContextOptions<ToDoContext> options, IHttpContextAccessor accessor) : base(options)
        {
            var email = accessor.HttpContext?.User.Claims.First(c => c.Type == "preferred_username");
            _currentUserEmail = email?.Value;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.Entity<Assignee>().HasQueryFilter(x => x.Email == _currentUserEmail);
        }
    }
}
