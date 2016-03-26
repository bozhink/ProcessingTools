namespace ProcessingTools.Documents.Data.Common.Models
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

        [MaxLength(128)]
        public string CreatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [MaxLength(128)]
        public string ModifiedByUserId { get; set; }
    }
}