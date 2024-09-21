using Moto.Application.Storage;

namespace Moto.Infraestructure.Storage;

public sealed class LocalFileStorageService : IFileStorageService
{
    public async Task<string> UploadAsync(string fileName, byte[] fileBytes)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

        await File.WriteAllBytesAsync(path, fileBytes);

        return path;
    }
}
