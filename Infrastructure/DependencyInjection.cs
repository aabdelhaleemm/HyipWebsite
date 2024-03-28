using System;
using System.Text;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, opt) =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("db"));
                opt.UseInternalServiceProvider(serviceProvider);
            });

            services.AddHttpContextAccessor();

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddSingleton<IJwtService, JwtService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IInvestmentsProfitService, InvestmentsProfitService>();
            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<ISendGridEmailService, SendGridEmailService>();
            services.Configure<SendGridEmailOptions>(options =>
            {
                options.ApiKey = configuration["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = configuration["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = configuration["ExternalProviders:SendGrid:SenderName"];
            });
            services.Configure<CloudinaryOptions>(options =>
            {
                options.ApiKey = configuration["ExternalProviders:Cloudinary:ApiKey"];
                options.Cloud = configuration["ExternalProviders:Cloudinary:Cloud"];
                options.ApiSecret = configuration["ExternalProviders:Cloudinary:ApiSecret"];
            });
            services.AddIdentityCore<User>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromMinutes(30));

            services.AddIdentityCore<Admin>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["jwtkey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}