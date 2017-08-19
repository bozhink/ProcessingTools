namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Documents;
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;

    public class Publisher : AddressableEntity, IEntityWithPreJoinedFields, IPublisherEntity
    {
        private ICollection<Journal> journals;

        public Publisher()
        {
            this.Id = Guid.NewGuid();
            this.journals = new HashSet<Journal>();
        }

        public Publisher(IPublisherEntity entity)
            : this()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Id = entity.Id;
            this.Name = entity.Name;
            this.AbbreviatedName = entity.AbbreviatedName;
            this.CreatedByUser = entity.CreatedByUser;
            this.ModifiedByUser = entity.ModifiedByUser;
            this.DateCreated = entity.DateCreated;
            this.DateModified = entity.DateModified;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }

        public virtual ICollection<Journal> Journals
        {
            get
            {
                return this.journals;
            }

            set
            {
                this.journals = value;
            }
        }

        [NotMapped]
        public IEnumerable<string> PreJoinFieldNames => new string[]
        {
            nameof(this.Addresses)
        };
    }
}
