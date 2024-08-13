using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Playground.Cloudinary;
using Cloudinary = CloudinaryDotNet.Cloudinary;

public interface ICloudinaryService
{
    Task<ImageUploadResult?> UploadImageAsync(IFormFile file, string bucket);
    
    Task<VideoUploadResult?> UploadVideoAsync(IFormFile file, string bucket);

    Task<DeletionResult> DeleteAsync(string publicId);
}

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;
    
    public CloudinaryService(IConfiguration configuration)
    {
        var url = configuration.GetSection("CloudinaryURL").Value ?? throw new NullReferenceException();
        
        _cloudinary = new Cloudinary(url)
        {
            Api =
            {
                Secure = true
            }
        };
    }

    public async Task<ImageUploadResult?> UploadImageAsync(IFormFile file, string bucket)
    {
        await using var stream = file.OpenReadStream();
        
        var parameters = new ImageUploadParams
        {
            File = new FileDescription(file.Name, stream),
            Folder = bucket,
            PublicId = Guid.NewGuid().ToString()
        };

        var result = await _cloudinary.UploadAsync(parameters);

        return result;
    }

    public async Task<VideoUploadResult?> UploadVideoAsync(IFormFile file, string bucket)
    {
        await using var stream = file.OpenReadStream();

        var parameters = new VideoUploadParams
        {
            File = new FileDescription(file.Name, stream),
            Folder = bucket,
            PublicId = Guid.NewGuid().ToString()
        };

        var result = await _cloudinary.UploadAsync(parameters);

        return result;
    }

    public async Task<DeletionResult> DeleteAsync(string publicId)
    {
        var parameters = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(parameters);
        
        return result;
    }
}