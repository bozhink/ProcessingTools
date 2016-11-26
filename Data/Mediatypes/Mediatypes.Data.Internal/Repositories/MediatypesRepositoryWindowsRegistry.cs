namespace ProcessingTools.Mediatypes.Data.Internal.Repositories
{
    using System.Threading.Tasks;
    using Abstractions.Repositories;
    using Microsoft.Win32;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;

    public class MediatypesRepositoryWindowsRegistry : AbstractMediatypesRepository, IMediatypesRepository
    {
        protected override Task<string> GetMediatype(string fileExtension)
        {
            string extension = $".{fileExtension.ToLower()}";
            var registryKey = Registry.ClassesRoot.OpenSubKey(extension);
            object value = registryKey?.GetValue("Content Type");

            string mediatype = value?.ToString();

            return Task.FromResult(mediatype);
        }
    }
}
