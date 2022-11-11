using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Configuration
{
    public class AssigneeConfiguration : IEntityTypeConfiguration<Assignee>
    {
        public void Configure(EntityTypeBuilder<Assignee> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasMany(a => a.Assignments)
                   .WithOne()
                   .HasForeignKey($"{nameof(Assignee)}Id")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
