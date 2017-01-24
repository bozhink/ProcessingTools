namespace ProcessingTools.Services.Data.Models.Mediatypes
{
    using System;
    using ProcessingTools.Contracts.Models.Mediatypes;

    internal class FileWithMediatypeServiceModel : IFileWithMediatype
    {
        public string FileName { get; set; }

        public string MediaType { get; set; }
    }
}
