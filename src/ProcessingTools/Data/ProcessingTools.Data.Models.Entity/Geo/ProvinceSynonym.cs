namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    public class ProvinceSynonym : Synonym, IDataModel
    {
        public virtual int ProvinceId { get; set; }

        public virtual Province Province { get; set; }
    }
}
