namespace ProcessingTools.Web.Documents.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using ProcessingTools.Constants;

    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [MaxLength(ValidationConstants.MaximalLengthOfUserIdentifier)]
        public override string Id { get; set; }
    }
}
