namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Data.Common.Models.Contracts;

    public abstract class DocumentsAbstractEntity : IEntityWithUserInformation
    {
        public DocumentsAbstractEntity()
        {
            this.DateModified = DateTime.UtcNow;
            this.DateCreated = this.DateModified;
        }

        [Required]
        [MaxLength(128)]
        public string CreatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(128)]
        public string ModifiedByUserId { get; set; }
    }
}