using System;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InvestmentsConfigurations : IEntityTypeConfiguration<Investments>
    {
        public void Configure(EntityTypeBuilder<Investments> builder)
        {
            builder.Ignore(x => x.DomainEvents);
            builder.HasOne(x => x.User)
                .WithMany(x => x.Investments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(x => x.Status)
                .HasConversion(
                    status => status.ToString(),
                    s => (InvestmentsStatus)Enum.Parse(typeof(InvestmentsStatus), s))
                .HasDefaultValue(InvestmentsStatus.Running);
            builder.Property(x => x.Plan)
                .HasConversion(
                    plan => plan.ToString(),
                    s => (InvestmentsPlan)Enum.Parse(typeof(InvestmentsPlan), s)
                );
            builder.HasIndex(x => x.UserId);
        }
    }
}