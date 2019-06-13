namespace ProcessingTools.Data.Models.Entity.Geo
{
    public class ProvinceSynonym : Synonym
    {
        public virtual int ProvinceId { get; set; }

        public virtual Province Province { get; set; }
    }
}
