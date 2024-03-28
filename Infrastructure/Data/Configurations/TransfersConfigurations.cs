using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TransfersConfigurations : IEntityTypeConfiguration<Transfers>
    {
        public void Configure(EntityTypeBuilder<Transfers> builder)
        {
            builder.HasOne(x => x.Sender)
                .WithMany(x => x.SentTransfers)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Recipient)
                .WithMany(x => x.ReceivedTransfers)
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.SenderId);
            builder.HasIndex(x => x.RecipientId);
        }
    }
}