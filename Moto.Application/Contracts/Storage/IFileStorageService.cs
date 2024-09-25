namespace Moto.Application.Contracts.Storage;

/// <summary>
/// Defines the contract for a file storage service that handles file uploads.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Uploads a file to the storage service.
    /// </summary>
    /// <param name="fileName">The name of the file to be uploaded.</param>
    /// <param name="extension">The file extension (e.g., .png, .jpg) to validate the file type.</param>
    /// <param name="base64">The base64 encoded string representation of the file.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the URL or path of the uploaded file.</returns>
    Task<string> UploadAsync(string fileName, string extension, string base64);
}
