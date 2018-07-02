// <copyright file="Mediatype.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Mediatypes
{
    using ProcessingTools.Constants;
    using ProcessingTools.Models.Contracts.Mediatypes;

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
            this.Mimetype = mimetype;
            this.Mimesubtype = mimesubtype;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediatype"/> class.
        /// </summary>
        /// <param name="mediatype">Mediatype.</param>
        public Mediatype(string mediatype)
        {
            int slashIndex = mediatype.IndexOf('/');
            this.Mimetype = mediatype.Substring(0, slashIndex);
            this.Mimesubtype = mediatype.Substring(slashIndex + 1);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
