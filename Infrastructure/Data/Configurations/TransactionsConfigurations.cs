using System;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TransactionsConfigurations : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Deposit)
                .WithOne(x => x.Transaction)
                .HasForeignKey<Transactions>(x => x.DepositId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Withdraw)
                .WithOne(x => x.Transaction)
                .HasForeignKey<Transactions>(x => x.WithdrawId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Investment)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.InvestmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.InvestmentsProfit)
                .WithOne(x => x.Transaction)
                .HasForeignKey<Transactions>(x => x.InvestmentProfitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.SenderTransfers)
                .WithOne(x => x.SenderTransaction)
                .HasForeignKey<Transactions>(x => x.SenderTransferId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RecipientTransfers)
                .WithOne(x => x.RecipientTransaction)
                .HasForeignKey<Transactions>(x => x.RecipientTransferId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Status)
                .HasConversion(
                    status => status.ToString(),
                    s => (Status)Enum.Parse(typeof(Status), s));
            builder.Property(x => x.Type)
                .HasConversion(
                    status => status.ToString(),
                    s => (TransactionsTypes)Enum.Parse(typeof(TransactionsTypes), s));
        }
    }
}