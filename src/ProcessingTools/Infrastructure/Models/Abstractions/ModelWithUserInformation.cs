namespace ProcessingTools.Models.Abstractions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants;
    using ProcessingTools.Models.Contracts;

    public abstract class ModelWithUserInformation : IModelWithUserInformation
    {
        protected ModelWithUserInformation()
        {
            this.DateModified = DateTime.UtcNow;
            this.DateCreated = this.DateModified;
        }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        [Display(Name = "Created By User")]
        public string CreatedByUser { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        [Display(Name = "Modified By User")]
        public string ModifiedByUser { get; set; }
    }
}
