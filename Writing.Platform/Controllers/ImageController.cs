using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Writing.Platform.Data;

namespace Writing.Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly CloudinaryUpload cloudinaryUpload;

        public ImageController(CloudinaryUpload cloudinaryUpload)
        {
            this.cloudinaryUpload = cloudinaryUpload;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var imgUrl = await cloudinaryUpload.UploadAsync(file);
            if (imgUrl == null)
            {

                return Problem("Image upload failed", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imgUrl });
        }
    }
}
