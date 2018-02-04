namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts;

    public class StateSynonym : Synonym, IDataModel
    {
        public virtual int StateId { get; set; }

        public virtual State State { get; set; }
    }
}
