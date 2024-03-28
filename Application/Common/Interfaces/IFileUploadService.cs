using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileUploadService
    {
        Task<Uri> UploadImage(IFormFile file);
    }
}