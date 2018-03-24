﻿namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts;

    public class ProvinceSynonym : Synonym, IDataModel
    {
        public virtual int ProvinceId { get; set; }

        public virtual Province Province { get; set; }
    }
}
