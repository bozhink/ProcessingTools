namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts;

    public class MunicipalitySynonym : Synonym, IDataModel
    {
        public virtual int MunicipalityId { get; set; }

        public virtual Municipality Municipality { get; set; }
    }
}
