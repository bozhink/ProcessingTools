// <copyright file="SpecimenCodeResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Models.Bio.SpecimenCodes
{
    using ProcessingTools.Data.Miners.Contracts.Models.Bio.SpecimenCodes;

    /// <summary>
    /// Specimen code response model.
    /// </summary>
    public class SpecimenCodeResponseModel : ISpecimenCode
    {
        private string content;
        private string contentType;
        private string url;
        private int hashCode;

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content
        {
            get
            {
                return this.content;
            }

            set
            {
                this.content = value ?? string.Empty;
                this.RecalculateHash();
            }
        }

        /// <summary>
        /// Gets or sets the content-type.
        /// </summary>
        public string ContentType
        {
            get
            {
                return this.contentType;
            }

            set
            {
                this.contentType = value ?? string.Empty;
                this.RecalculateHash();
            }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url
        {
            get
            {
                return this.url;
            }

            set
            {
                this.url = value ?? string.Empty;
                this.RecalculateHash();
            }
        }

        private int HashCode => this.hashCode;

        /// <inheritdoc/>
        public override bool Equals(object obj) => this.GetHashCode() == obj.GetHashCode();

        /// <inheritdoc/>
        public override int GetHashCode() => this.HashCode;

        private void RecalculateHash()
        {
            this.hashCode = (this.content + this.contentType + this.url).GetHashCode();
        }
    }
}
