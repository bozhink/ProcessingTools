﻿namespace ProcessingTools.Journals.Data.Entity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;
    using ProcessingTools.Contracts.Data.Journals.Models;

    public class Institution : Addressable, IInstitution
    {
        public Institution()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedInstitutionName)]
        public string AbbreviatedName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfInstitutionName)]
        public string Name { get; set; }
    }
}
