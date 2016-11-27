namespace ProcessingTools.Mediatypes.Data.Internal.Repositories
{
    using Abstractions.Repositories;
    using Microsoft.Win32;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;

    public class MediatypesRepositoryWindowsRegistry : AbstractMediatypesRepository, IMediatypesRepository
    {
        protected override string GetMediatype(string fileExtension)
        {
            string extension = $".{fileExtension.ToLower()}";
            var registryKey = Registry.ClassesRoot.OpenSubKey(extension);
            object value = registryKey?.GetValue("Content Type");
            string mediatype = value?.ToString();
            return mediatype;
        }
    }
}
