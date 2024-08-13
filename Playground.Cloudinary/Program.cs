using Playground.Cloudinary;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddEndpointsApiExplorer().AddSwaggerGen();
services.AddCloudinary();

var app = builder.Build();
app.UseSwagger().UseSwaggerUI();

app.MapGet("", () => "Hello World");

app.MapPost("/upload", (ICloudinaryService cloudinaryService, IFormFile file, string bucket) 
    => cloudinaryService.UploadImageAsync(file, bucket)).DisableAntiforgery();

app.MapPost("/upload-video", (ICloudinaryService cloudinaryService, IFormFile file, string bucket) 
    => cloudinaryService.UploadVideoAsync(file, bucket)).DisableAntiforgery();

app.MapDelete("/delete", (ICloudinaryService cloudinaryService, string publicId) => cloudinaryService.DeleteAsync(publicId));

app.Run();