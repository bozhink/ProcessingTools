﻿namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    public class MunicipalitySynonym : Synonym, IDataModel
    {
        public virtual int MunicipalityId { get; set; }

        public virtual Municipality Municipality { get; set; }
    }
}
