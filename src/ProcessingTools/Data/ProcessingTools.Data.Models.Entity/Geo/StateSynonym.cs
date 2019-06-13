namespace ProcessingTools.Data.Models.Entity.Geo
{
    public class StateSynonym : Synonym
    {
        public virtual int StateId { get; set; }

        public virtual State State { get; set; }
    }
}
