namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class Article : DocumentsAbstractEntity
    {
        private ICollection<Document> documents;
        private ICollection<Author> authors;

        public Article()
        {
            this.Id = Guid.NewGuid();
            this.documents = new HashSet<Document>();
            this.authors = new HashSet<Author>();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfArticleTitle)]
        public string Title { get; set; }

        public DateTime? DateReceived { get; set; }

        public DateTime? DateAccepted { get; set; }

        public DateTime? DatePublished { get; set; }

        public int? Volume { get; set; }

        public int? Issue { get; set; }

        public int? FirstPage { get; set; }

        public int? LastPage { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfArticleELocationId)]
        public string ELocationId { get; set; }

        public virtual Guid JournalId { get; set; }

        public virtual Journal Journal { get; set; }

        public virtual ICollection<Document> Documents
        {
            get
            {
                return this.documents;
            }

            set
            {
                this.documents = value;
            }
        }

        public virtual ICollection<Author> Authors
        {
            get
            {
                return this.authors;
            }

            set
            {
                this.authors = value;
            }
        }
    }
}