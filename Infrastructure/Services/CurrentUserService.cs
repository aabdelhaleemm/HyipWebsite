using System;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor accessor)
        {
            if (accessor.HttpContext != null)
                UserId = Convert.ToInt32(accessor.HttpContext.User.FindFirst("Id")?.Value);
        }

        public int UserId { get; }
    }
}