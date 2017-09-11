namespace ProcessingTools.Services.Data.Models.Mediatypes
{
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Models.Mediatypes;

    internal class MediatypeServiceModel : IMediatype
    {
        private string mimetype;
        private string mimesubtype;

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

        public string Mimetype
        {
            get
            {
                return this.mimetype;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.mimetype = ContentTypes.DefaultMimetype;
                }
                else
                {
                    this.mimetype = value;
                }
            }
        }

        public string Mimesubtype
        {
            get
            {
                return this.mimesubtype;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.mimesubtype = ContentTypes.DefaultMimesubtype;
                }
                else
                {
                    this.mimesubtype = value;
                }
            }
        }
    }
}
