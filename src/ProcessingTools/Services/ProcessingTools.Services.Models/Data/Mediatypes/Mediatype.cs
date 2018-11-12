// <copyright file="Mediatype.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Mediatypes
{
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    /// <summary>
    /// Mediatype service model.
    /// </summary>
    public class Mediatype : IMediatype
    {
        private string mimetype;
        private string mimesubtype;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediatype"/> class.
        /// </summary>
        /// <param name="mimetype">Mime type.</param>
        /// <param name="mimesubtype">Mime sub-type.</param>
        public Mediatype(string mimetype, string mimesubtype)
        {
            this.MimeType = mimetype;
            this.MimeSubtype = mimesubtype;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediatype"/> class.
        /// </summary>
        /// <param name="mediatype">Mediatype.</param>
        public Mediatype(string mediatype)
        {
            int slashIndex = mediatype.IndexOf('/');
            this.MimeType = mediatype.Substring(0, slashIndex);
            this.MimeSubtype = mediatype.Substring(slashIndex + 1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediatype"/> class.
        /// </summary>
        public Mediatype()
        {
            this.MimeType = ContentTypes.DefaultMimeType;
            this.MimeSubtype = ContentTypes.DefaultMimeSubtype;
        }

        /// <inheritdoc/>
        public string MimeType
        {
            get
            {
                return this.mimetype;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.mimetype = ContentTypes.DefaultMimeType;
                }
                else
                {
                    this.mimetype = value;
                }
            }
        }

        /// <inheritdoc/>
        public string MimeSubtype
        {
            get
            {
                return this.mimesubtype;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.mimesubtype = ContentTypes.DefaultMimeSubtype;
                }
                else
                {
                    this.mimesubtype = value;
                }
            }
        }
    }
}
