using Moto.Application.Storage;

namespace Moto.Infraestructure.Storage;

public sealed class LocalFileStorageService : IFileStorageService
{
    public async Task<string> UploadAsync(string fileName, byte[] fileBytes)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "images");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path = Path.Combine(path, fileName);

        // Criar ou abrir o arquivo para escrita
        using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            // Escrever os bytes no arquivo
            await fs.WriteAsync(fileBytes, 0, fileBytes.Length);
        }

        return path;
    }
}
