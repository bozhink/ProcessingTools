namespace ProcessingTools.Models.Abstractions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Models.Contracts;

    public abstract class ModelWithUserInformation : ICreated, IModified
    {
        protected ModelWithUserInformation()
        {
            this.ModifiedOn = DateTime.UtcNow;
            this.CreatedOn = this.ModifiedOn;
        }

        [Display(Name = "Date Created")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime ModifiedOn { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        [Display(Name = "Created By User")]
        public string CreatedBy { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        [Display(Name = "Modified By User")]
        public string ModifiedBy { get; set; }
    }
}
