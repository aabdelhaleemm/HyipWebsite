using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class FileUploadService : IFileUploadService
    {
        public FileUploadService(IOptions<CloudinaryOptions> options)
        {
            Options = options.Value;
        }

        private CloudinaryOptions Options { get; }

        public async Task<Uri> UploadImage(IFormFile file)
        {
            var account = new Account(Options.Cloud, Options.ApiKey, Options.ApiSecret);
            var cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription("deposit", file.OpenReadStream()),
                Type = "authenticated",
                PublicId = Guid.NewGuid().ToString()
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url;
        }
    }
}