namespace ProcessingTools.BaseLibrary.Special
{
    using ProcessingTools.DocumentProvider;

    public abstract class SpecialFactory : TaxPubDocument
    {
        public SpecialFactory(string xml)
            : base(xml)
        {
        }
    }
}