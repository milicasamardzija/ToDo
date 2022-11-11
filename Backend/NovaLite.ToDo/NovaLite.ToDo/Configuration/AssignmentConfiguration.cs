using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Configuration
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(nameof(Assignment.Number), $"{nameof(Assignee)}Id");
            builder.Property(a => a.Subject).HasMaxLength(255);
            builder.Property(a => a.Description);
            builder.Property(a => a.Number)
                   .ValueGeneratedNever();
            builder.HasMany(a => a.Steps)
                   .WithOne()
                   .HasForeignKey($"{nameof(Assignment)}{nameof(Assignment.Number)}", $"{nameof(Assignee)}{nameof(Assignee.Id)}")
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Attachment)
                   .WithMany();
        }
    }
}
