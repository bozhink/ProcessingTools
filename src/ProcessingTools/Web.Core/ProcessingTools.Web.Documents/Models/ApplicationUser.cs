namespace ProcessingTools.Web.Documents.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using ProcessingTools.Constants;

    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public override string Id { get; set; }
    }
}
