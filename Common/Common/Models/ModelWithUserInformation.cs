namespace ProcessingTools.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class ModelWithUserInformation : IModelWithUserInformation
    {
        public ModelWithUserInformation()
        {
            this.DateModified = DateTime.UtcNow;
            this.DateCreated = this.DateModified;
        }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.MaximalLengthOfUserIdentifier)]
        public string CreatedByUser { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.MaximalLengthOfUserIdentifier)]
        public string ModifiedByUser { get; set; }
    }
}
