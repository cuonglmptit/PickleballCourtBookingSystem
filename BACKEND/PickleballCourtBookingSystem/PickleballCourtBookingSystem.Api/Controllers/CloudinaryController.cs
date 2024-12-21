using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
namespace PickleballCourtBookingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CloudinaryController : ControllerBase
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryController(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    [HttpPost("upload")]
    public IActionResult UploadImageByImageUrl(string imageUrl)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(imageUrl),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true
        };

        var uploadResult = _cloudinary.Upload(uploadParams);

        return Ok(uploadResult);
    }
    
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            // Convert file to stream
            await using var stream = file.OpenReadStream();

            // Set up upload parameters
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };

            // Upload to Cloudinary
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            // Return the secure URL of the uploaded image
            return Ok(new { Url = uploadResult.SecureUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
