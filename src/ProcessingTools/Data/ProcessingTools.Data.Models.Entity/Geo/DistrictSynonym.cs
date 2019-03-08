﻿namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Models.Contracts;

    public class DistrictSynonym : Synonym, IDataModel
    {
        public virtual int DistrictId { get; set; }

        public virtual District District { get; set; }
    }
}