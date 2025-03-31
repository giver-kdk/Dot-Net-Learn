using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EMS.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadProfilePictureAsync(IFormFile file, int userId);
        Task DeleteImageAsync(string publicId);
    }
}
