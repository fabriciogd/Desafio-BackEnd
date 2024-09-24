namespace Moto.Application.File
{
    public class FileExtensionChecker : IFileExtensionChecker
    {
        public (bool, string) Validate(string base64, params string[] extensions)
        {
            byte[] imageBytes = Convert.FromBase64String(base64);

            foreach (var entry in SignatureToExtensionMap)
            {
                byte[] signature = entry.Key;

                if (imageBytes.Length >= signature.Length)
                {
                    bool isMatch = true;
                    for (int i = 0; i < signature.Length; i++)
                    {
                        if (imageBytes[i] != signature[i])
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        return (true, entry.Value);
                    }
                }
            }

            return (false, string.Empty);
        }


        private static readonly Dictionary<byte[], string> SignatureToExtensionMap = new Dictionary<byte[], string>
        {
            //{ new byte[] { 0xFF, 0xD8, 0xFF }, ".jpg" }, // JPEG
            { new byte[] { 0x89, 0x50, 0x4E, 0x47 }, ".png" }, // PNG
            //{ new byte[] { 0x47, 0x49, 0x46, 0x38 }, ".gif" }, // GIF
            { new byte[] { 0x42, 0x4D }, ".bmp" }, // BMP
            //{ new byte[] { 0x49, 0x20, 0x49, 0x43 }, ".tiff" }, // TIFF
            //{ new byte[] { 0x52, 0x49, 0x46, 0x46 }, ".webp" }, // WEBP
        };
    }
}
