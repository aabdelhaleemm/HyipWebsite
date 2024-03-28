using System;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class WithdrawsConfigurations : IEntityTypeConfiguration<Withdraws>
    {
        public void Configure(EntityTypeBuilder<Withdraws> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.Withdraws)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.Status)
                .HasConversion(
                    status => status.ToString(),
                    s => (Status)Enum.Parse(typeof(Status), s))
                .HasDefaultValue(Status.Pending);
            builder.HasIndex(x => x.UserId);
        }
    }
}