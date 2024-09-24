using Moto.Application.Storage;

namespace Moto.Infraestructure.Storage;

/// <summary>
/// Implements a local file storage service for handling file uploads.
/// </summary>
public sealed class LocalFileStorageService : IFileStorageService
{
    /// <summary>
    /// Asynchronously uploads a file to the local storage as a base64 encoded string.
    /// </summary>
    /// <param name="fileName">The name of the file (without extension) to be saved.</param>
    /// <param name="extension">The file extension (e.g., .png, .jpg) for the uploaded file.</param>
    /// <param name="base64">The base64 encoded string representation of the file.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the local path of the uploaded file.</returns>
    public async Task<string> UploadAsync(string fileName, string extension, string base64)
    {
        var fileBytes = Convert.FromBase64String(base64);

        var path = Path.Combine(Directory.GetCurrentDirectory(), "images");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, $"{fileName}{extension}");

        using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            await fs.WriteAsync(fileBytes, 0, fileBytes.Length);
        }

        return path;
    }
}