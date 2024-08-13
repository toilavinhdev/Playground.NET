namespace Playground.Cloudinary;

public static class CloudinaryExtensions
{
    public static void AddCloudinary(this IServiceCollection services)
    {
        services.AddSingleton<ICloudinaryService, CloudinaryService>();
    }
}