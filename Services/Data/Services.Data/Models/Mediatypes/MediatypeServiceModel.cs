namespace ProcessingTools.Services.Data.Models.Mediatypes
{
    using ProcessingTools.Contracts.Models.Mediatypes;

    internal class MediatypeServiceModel : IMediatype
    {
        public MediatypeServiceModel(string mimetype, string mimesubtype)
        {
            this.Mimetype = mimetype;
            this.Mimesubtype = mimesubtype;
        }

        public MediatypeServiceModel(string mediatype)
        {
            int slashIndex = mediatype.IndexOf('/');
            this.Mimetype = mediatype.Substring(0, slashIndex);
            this.Mimesubtype = mediatype.Substring(slashIndex + 1);
        }

        public string Mimetype { get; set; }

        public string Mimesubtype { get; set; }
    }
}
