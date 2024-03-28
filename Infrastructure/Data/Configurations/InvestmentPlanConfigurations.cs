using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InvestmentPlanConfigurations : IEntityTypeConfiguration<InvestmentPlans>
    {
        public void Configure(EntityTypeBuilder<InvestmentPlans> builder)
        {
            builder.HasIndex(x => x.Name)
                .IsUnique();
            builder.Property(x => x.Name)
                .HasConversion(
                    plan => plan.ToString(),
                    s => (InvestmentsPlan)Enum.Parse(typeof(InvestmentsPlan), s)
                );
            builder.HasData(new List<InvestmentPlans>
            {
                new(InvestmentsPlan.IslamicStarterPlan, 5, 7.5, 5)
                    { Id = 1, Created = DateTime.UtcNow },
                new(InvestmentsPlan.IslamicSilverPlan, 7.5, 10, 7.5)
                    { Id = 2, Created = DateTime.UtcNow },
                new(InvestmentsPlan.IslamicGoldenPlan, 10, 12.5, 10)
                    { Id = 3, Created = DateTime.UtcNow },
                new(InvestmentsPlan.IslamicDiamondPlan, 12.5, 15, 12.5)
                    { Id = 4, Created = DateTime.UtcNow },
                new(InvestmentsPlan.IslamicPearlPlan, 15, 17.5, 15)
                    { Id = 5, Created = DateTime.UtcNow },
                new(InvestmentsPlan.IslamicPlatinumPlan, 17.5, 20, 17.5)
                    { Id = 6, Created = DateTime.UtcNow },
                new(InvestmentsPlan.IslamicMindsTradePlan, 0, 1000, 0)
                    { Id = 7, Created = DateTime.UtcNow },
            });
        }
    }
}