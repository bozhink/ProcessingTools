namespace ProcessingTools.Services.Data.Services.Mediatypes
{
    using System.Threading.Tasks;
    using Contracts.Mediatypes;
    using Microsoft.Win32;

    public class MediatypeStringResolverWithWindowsRegistry : IMediatypeStringResolver
    {
        public Task<string> Resolve(string fileExtension)
        {
            var registryKey = Registry.ClassesRoot.OpenSubKey(fileExtension);
            object value = registryKey?.GetValue("Content Type");
            string mediatype = value?.ToString();

            return Task.FromResult(mediatype);
        }
    }
}
