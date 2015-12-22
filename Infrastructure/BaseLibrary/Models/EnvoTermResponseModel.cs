namespace ProcessingTools.BaseLibrary.Models
{
    public class EnvoTermResponseModel
    {
        private string envoId;

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

        public string Content { get; set; }

        public string Uri
        {
            get
            {
                return "http://purl.obolibrary.org/obo/" + this.envoId;
            }
        }
    }
}
