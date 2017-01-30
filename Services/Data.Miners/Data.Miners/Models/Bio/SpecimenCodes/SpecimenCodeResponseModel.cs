namespace ProcessingTools.Data.Miners.Models.Bio.SpecimenCodes
{
    using Contracts.Models.Bio.SpecimenCodes;

    internal class SpecimenCodeResponseModel : ISpecimenCode
    {
        private string content;
        private string contentType;
        private string url;
        private int hashCode;

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

        public override bool Equals(object obj) => this.GetHashCode() == obj.GetHashCode();

        public override int GetHashCode() => this.hashCode;

        private void RecalculateHash()
        {
            this.hashCode = (this.content + this.contentType + this.url).GetHashCode();
        }
    }
}
