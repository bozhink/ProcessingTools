namespace ProcessingTools.MediaType.Services.Data.Services
{
    using Contracts;
    using Factories;

    using Microsoft.Win32;

    public class MediaTypeDataServiceWindowsRegistry : MediaTypeDataServiceFactory, IMediaTypeDataService
    {
        protected override string ResolveMediaType(string fileExtension)
        {
            string extension = $".{fileExtension.ToLower()}";
            var registryKey = Registry.ClassesRoot.OpenSubKey(extension);
            object value = registryKey?.GetValue("Content Type");

            string mediaType = null;
            if (value != null)
            {
                mediaType = value.ToString();
            }

            return mediaType;
        }
    }
}