using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System.Net;

namespace Writing.Platform.Data
{
    public class CloudinaryUpload
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        public CloudinaryUpload(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                 configuration.GetSection("CloudinaryKey")["CloudName"],
                 configuration.GetSection("CloudinaryKey")["ApiKey"],
                 configuration.GetSection("CloudinaryKey")["ApiSecret"]
             );
        }
        public async Task<string?> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var uploadResult = await client.UploadAsync(uploadParams);
            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}
