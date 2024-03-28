using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PaymentMethodsConfigurations : IEntityTypeConfiguration<PaymentMethods>
    {
        public void Configure(EntityTypeBuilder<PaymentMethods> builder)
        {
            builder.HasIndex(x => x.Name);
            builder.HasData(
                new PaymentMethods
                {
                    Id = 1, Name = "Perfect Money", Charge = 1, WalletId = "U35960081", Minimum = 10,
                    LogoUrl = "https://res.cloudinary.com/dbhqoj86r/image/upload/v1634932548/pngwing.com_xnyhvg.png",
                    Created = DateTime.UtcNow
                },
                new PaymentMethods
                {
                    Id = 2, Name = "USDT (ERC -20)", Charge = 20,
                    WalletId = "0x93EC394634568D8d32493cF23142C8f7F98391ef",
                    Minimum = 10,
                    LogoUrl =
                        "https://res.cloudinary.com/dbhqoj86r/image/upload/c_pad,h_480,w_960/v1634932790/tether-usdt-logo_bxl3dk.png",
                    Created = DateTime.UtcNow
                },
                new PaymentMethods
                {
                    Id = 3, Name = "USDT (TRC -20)", Charge = 1, WalletId = "TSieWboSBuEvbm9WjZ5dXfZySXFxA3mjuX",
                    Minimum = 10,
                    LogoUrl =
                        "https://res.cloudinary.com/dbhqoj86r/image/upload/c_pad,h_480,w_960/v1634932790/tether-usdt-logo_bxl3dk.png",
                    Created = DateTime.UtcNow
                },
                new PaymentMethods
                {
                    Id = 4, Name = "Other Methods", Charge = 0, Minimum = 10,
                    LogoUrl =
                        "https://res.cloudinary.com/dbhqoj86r/image/upload/c_pad,h_480,w_960/v1634933345/s_u2npdm.png",
                    Created = DateTime.UtcNow
                }
            );
        }
    }
}