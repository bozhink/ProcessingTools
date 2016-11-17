namespace ProcessingTools.Bio.Data.Miners.Models.SpecimenCodes
{
    using Contracts.Models.SpecimenCodes;

    internal class SpecimenCodeResponseModel : ISpecimenCode
    {
        private string content;
        private string contentType;
        private int hashCode;

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
                this.hashCode = (this.content + this.contentType).GetHashCode();
            }
        }

        public string ContentType
        {
            get
            {
                return contentType;
            }

            set
            {
                contentType = value;
                this.hashCode = (this.content + this.contentType).GetHashCode();
            }
        }

        public override bool Equals(object obj) => this.GetHashCode() == obj.GetHashCode();

        public override int GetHashCode() => this.hashCode;
    }
}
