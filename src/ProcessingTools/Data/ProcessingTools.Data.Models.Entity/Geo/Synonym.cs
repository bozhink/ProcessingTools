﻿namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    public abstract class Synonym : BaseModel, ISynonym, IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoName)]
        public string Name { get; set; }

        public int? LanguageCode { get; set; }
    }
}