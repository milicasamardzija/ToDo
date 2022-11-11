using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaLite.ToDo.Model;

namespace NovaLite.ToDo.Configuration
{
    public class StepConfiguration : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.HasKey(nameof(Step.Number),
                $"{nameof(Assignment)}{nameof(Assignment.Number)}",
                $"{nameof(Assignee)}{nameof(Assignee.Id)}");

            builder.Property(a => a.Number)
                   .ValueGeneratedOnAdd();
            builder.Property(a => a.Subject).HasMaxLength(255);
        }
    }
}
