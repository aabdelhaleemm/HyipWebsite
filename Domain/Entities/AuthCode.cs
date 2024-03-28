using System;
using Domain.Common;

namespace Domain.Entities
{
    public class AuthCode : AuditableEntity
    {
        public AuthCode(int userId)
        {
            Code = GenerateCode();
            ExpireAt = DateTime.UtcNow.AddMinutes(30);
            UserId = userId;
        }

        public int Id { get; set; }
        public int Code { get; private set; }
        public int UserId { get; private set; }
        public DateTime ExpireAt { get; private set; }

        private int GenerateCode()
        {
            var generator = new Random();
            return int.Parse(generator.Next(0, 1000000).ToString("D6"));
        }
    }
}