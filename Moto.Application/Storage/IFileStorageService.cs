namespace Moto.Application.Storage;

public interface IFileStorageService
{
    Task<string> UploadAsync(string fileName, byte[] file);
}
