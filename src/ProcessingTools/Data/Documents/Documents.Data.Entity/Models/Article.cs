namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Documents;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;
    using ProcessingTools.Models.Abstractions;

    public class Article : ModelWithUserInformation, IEntityWithPreJoinedFields, IArticle
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

        [Required]
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

        [NotMapped]
        public IEnumerable<string> PreJoinFieldNames => new string[]
        {
            nameof(this.Authors),
            nameof(this.Documents)
        };

        [NotMapped]
        IEnumerable<IDocument> IArticle.Documents => this.Documents;

        [NotMapped]
        IEnumerable<IAuthor> IArticle.Authors => this.Authors;
    }
}
