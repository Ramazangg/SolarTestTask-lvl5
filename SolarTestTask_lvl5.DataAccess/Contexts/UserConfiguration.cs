using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarTestTask_lvl5.Domain;

namespace SolarTestTask_lvl5.DataAccess.Contexts
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u=>u.email).HasMaxLength(64);
            builder.Property(u => u.FIO).HasMaxLength(64);
            builder.Property(u => u.BirthDate).HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));
        }
    }
}
