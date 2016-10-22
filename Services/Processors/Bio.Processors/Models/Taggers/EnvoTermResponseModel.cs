namespace ProcessingTools.Bio.Processors.Models.Taggers
{
    internal class EnvoTermResponseModel
    {
        private string envoId;

        public string Content { get; set; }

        public string EntityId { get; set; }

        public string EnvoId
        {
            get
            {
                return this.envoId;
            }

            set
            {
                this.envoId = "ENVO_" + value?.Substring(5);
            }
        }
    }
}
