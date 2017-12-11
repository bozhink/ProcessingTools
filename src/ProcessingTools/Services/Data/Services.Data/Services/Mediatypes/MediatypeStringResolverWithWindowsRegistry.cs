namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System.Threading.Tasks;
    using Microsoft.Win32;
    using ProcessingTools.Contracts.Services.Data.Mediatypes;

    public class MediatypeStringResolverWithWindowsRegistry : IMediatypeStringResolver
    {
        public Task<string> ResolveAsync(string fileExtension)
        {
            var registryKey = Registry.ClassesRoot.OpenSubKey(fileExtension);
            object value = registryKey?.GetValue("Content Type");
            string mediatype = value?.ToString();

            return Task.FromResult(mediatype);
        }
    }
}
