namespace Moto.Application.File;

public interface IFileExtensionChecker
{
    (bool, string) Validate(string base64, params string[] extensions);
}
