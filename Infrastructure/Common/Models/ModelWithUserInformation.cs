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

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.MaximalLengthOfUserIdentifier)]
        [Display(Name = "Created By User")]
        public string CreatedByUser { get; set; }

        [Required]
        [MaxLength(ModelValidationConstants.MaximalLengthOfUserIdentifier)]
        [Display(Name = "Modified By User")]
        public string ModifiedByUser { get; set; }
    }
}
