using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Domain.Entities.State> State { get; set; }
        DbSet<Domain.Entities.PaymentMethods> PaymentMethods { get; set; }
        DbSet<InvestmentPlans> InvestmentPlans { get; set; }
        DbSet<Transfers> Transfers { get; set; }
        DbSet<Domain.Entities.AuthCode> AuthCode { get; set; }
        DbSet<Domain.Entities.User> Users { get; set; }
        public DbSet<Domain.Entities.Transactions> Transactions { get; set; }
        DbSet<Domain.Entities.Deposits> Deposits { get; set; }
        DbSet<Investments> Investments { get; set; }
        DbSet<InvestmentsProfits> InvestmentsProfits { get; set; }
        public DbSet<Domain.Entities.Wallet> Wallets { get; set; }
        public DbSet<Domain.Entities.Withdraws> Withdraws { get; set; }

        DbSet<Domain.Entities.Admin> Admin { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}