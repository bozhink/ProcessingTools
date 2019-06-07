namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    public class StateSynonym : Synonym, IDataModel
    {
        public virtual int StateId { get; set; }

        public virtual State State { get; set; }
    }
}
