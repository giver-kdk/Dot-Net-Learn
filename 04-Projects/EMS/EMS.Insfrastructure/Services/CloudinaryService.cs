using EMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace EMS.Insfrastructure.Services
{
    public class CloudinaryService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService()
        {
            String cloudinary_name = Environment.GetEnvironmentVariable("CLOUDINARY_NAME");
            String cloudinary_api_key = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            String cloudinary_api_secret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");

            _cloudinary = new Cloudinary(new Account(
                cloudinary_name,
                cloudinary_api_key,
                cloudinary_api_secret));
        }

        public async Task<string> UploadProfilePictureAsync(IFormFile file, int userId)
        {
            if (file == null || file.Length == 0){
                Console.WriteLine("************** File is Empty **************");
                return null;
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = $"ems/profile_pictures/{userId}",
                Transformation = new Transformation()
                    .Width(200).Height(200).Gravity("face").Crop("thumb")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            Console.WriteLine("************** Img URL: " + uploadResult);

            return uploadResult.SecureUrl.ToString();
        }

        public async Task DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
