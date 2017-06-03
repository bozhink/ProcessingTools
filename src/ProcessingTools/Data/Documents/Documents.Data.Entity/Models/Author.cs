namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ProcessingTools.Constants.Data.Documents;
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;
    using ProcessingTools.Models.Abstractions;

    public class Author : ModelWithUserInformation, IEntityWithPreJoinedFields, IAuthorEntity
    {
        private ICollection<Affiliation> affiliations;
        private ICollection<Article> articles;

        public Author()
        {
            this.Id = Guid.NewGuid();
            this.affiliations = new HashSet<Affiliation>();
            this.articles = new HashSet<Article>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAuthorSurname)]
        public string Surname { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAuthorGivenNames)]
        public string GivenNames { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAuthorPrefix)]
        public string Prefix { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAuthorSuffix)]
        public string Suffix { get; set; }

        public virtual ICollection<Affiliation> Affiliations
        {
            get
            {
                return this.affiliations;
            }

            set
            {
                this.affiliations = value;
            }
        }

        public virtual ICollection<Article> Articles
        {
            get
            {
                return this.articles;
            }

            set
            {
                this.articles = value;
            }
        }

        [NotMapped]
        public IEnumerable<string> PreJoinFieldNames => new string[]
        {
            nameof(this.Affiliations),
            nameof(this.Articles)
        };

        [NotMapped]
        ICollection<IAffiliationEntity> IAuthorEntity.Affiliations => this.Affiliations.ToList<IAffiliationEntity>();

        [NotMapped]
        ICollection<IArticleEntity> IAuthorEntity.Articles => this.Articles.ToList<IArticleEntity>();
    }
}
