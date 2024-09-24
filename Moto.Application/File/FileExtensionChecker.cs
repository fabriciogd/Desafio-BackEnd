namespace Moto.Application.File
{
    public class FileExtensionChecker : IFileExtensionChecker
    {
        public (bool, string) Validate(string base64, params string[] extensions)
        {
            if (string.IsNullOrEmpty(base64))
                return (false, string.Empty);

            var extension = GetFileExtension(base64);
            return (extensions.Contains(extension), extension);
        }

        private string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            return data.ToUpper() switch
            {
                "IVBOR" => "png",
                "/9J/4" => "jpg",
                "AAAAF" => "mp4",
                "JVBER" => "pdf",
                "AAABA" => "ico",
                "UMFYI" => "rar",
                "E1XYD" => "rtf",
                "U1PKC" => "txt",
                "MQOWM" or "77U/M" => "srt",
                _ => string.Empty,
            };
        }
    }
}
