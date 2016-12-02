namespace ProcessingTools.Data.Miners.Models.SpecimenCodes
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
                return this.content;
            }

            set
            {
                this.content = value;
                this.hashCode = (this.content + this.contentType).GetHashCode();
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
                this.contentType = value;
                this.hashCode = (this.content + this.contentType).GetHashCode();
            }
        }

        public override bool Equals(object obj) => this.GetHashCode() == obj.GetHashCode();

        public override int GetHashCode() => this.hashCode;
    }
}
