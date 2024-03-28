using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InvestmentsProfitsConfigurations : IEntityTypeConfiguration<InvestmentsProfits>
    {
        public void Configure(EntityTypeBuilder<InvestmentsProfits> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.InvestmentsProfits)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Investment)
                .WithMany(x => x.InvestmentsProfits)
                .HasForeignKey(x => x.InvestmentsId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.InvestmentsId);
        }
    }
}