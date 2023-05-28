using ASP_DotNet_Web_api.Models.Domain;
using ASP_DotNet_Web_api.Models.DTO;
using ASP_DotNet_Web_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP_DotNet_Web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        //Post : /api/Images/Upload

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                //convert DTO to domainModel
                var imageDomainModel = new Image()
                {
                    File = request.File,
                    FileExtention = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription,

                };



                //User Repository  to Upload Image

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtentions.Contains(Path.GetExtension(request.File.FileName)))  //using path we are getting extention
            {
                ModelState.AddModelError("file", "Unsupoorted Image Format");
            }

            if (request.File.Length > 10485760)  //10mb = to bytes
            {
                ModelState.AddModelError("file", "file size more then 10MB . please Upload a smaller size file less then 10mb");
            }
        }
    }
}
