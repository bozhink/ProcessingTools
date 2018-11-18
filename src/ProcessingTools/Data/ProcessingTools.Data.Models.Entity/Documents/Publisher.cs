namespace ProcessingTools.Data.Models.Entity.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Documents;
    using ProcessingTools.Models.Contracts.Documents;

    public class Publisher : AddressableEntity, IPublisher
    {
        private ICollection<Journal> journals;

        public Publisher()
        {
            this.Id = Guid.NewGuid();
            this.journals = new HashSet<Journal>();
        }

        public Publisher(IPublisher entity)
            : this()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.Id = entity.Id;
            this.Name = entity.Name;
            this.AbbreviatedName = entity.AbbreviatedName;
            this.CreatedBy = entity.CreatedBy;
            this.ModifiedBy = entity.ModifiedBy;
            this.CreatedOn = entity.CreatedOn;
            this.ModifiedOn = entity.ModifiedOn;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
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
    }
}
