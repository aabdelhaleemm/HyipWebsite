using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.Reference)
                .WithMany(x => x.ReferenceUsers)
                .HasForeignKey(x => x.ReferenceId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}